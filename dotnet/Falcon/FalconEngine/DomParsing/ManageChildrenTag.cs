using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.DomParsing.Parser.Attribute;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public class ManageChildrenTag : IManageChildrenTag
    {
        private IDeleteUselessSpace _deleteUselessSpace;
        private IIdentifyTag _identifyTag;
        private IIdentifyStartTagEndTag _identitfyStartEndTag;
        private IDeterminateContent _determinateContent;
        private IAttributeTagManager _attributeTagManager;
        private IList<ITagParser> _tagParsers;
        private Dictionary<TagModel, string> _htmlByParents;
        private Dictionary<TagModel, List<TagModel>> _childrenWithParents;
        private List<TagModel> _parents;
        private TagModel _parent;

        public ManageChildrenTag(IDeleteUselessSpace deleteUselessSpace, IIdentifyTag identifyTag,
            IIdentifyStartTagEndTag identifyStartTagEndTag, IDeterminateContent determinateContent,
            IAttributeTagManager attributeTagManager)
        {
            _identifyTag = identifyTag;
            _deleteUselessSpace = deleteUselessSpace;
            _identitfyStartEndTag = identifyStartTagEndTag;
            _determinateContent = determinateContent;
            _attributeTagManager = attributeTagManager;
            _parents = new List<TagModel>();
            _childrenWithParents = new Dictionary<TagModel, List<TagModel>>();
            _htmlByParents = new Dictionary<TagModel, string>();
        }

        public List<TagModel> Identify(TagModel parent, string html)
        {
            _parents.Add(parent);
            _htmlByParents.Add(parent, html);
            try
            {
                SearchChildren(parent);
            }
            catch (NoStartTagException ex)
            {
                return GetChildren();
            }
            catch (Exception ex)
            {
                throw new DeterminateChildrenException(ErrorTypeParsing.children, $"Error parsing for the children of  {html}");
            }
            return GetChildren();
        }

        //TODO subdivise this method
        private void SearchChildren(TagModel parent)
        {
            var initiateParser = new InitiateParser(_deleteUselessSpace, _identifyTag, _identitfyStartEndTag, _determinateContent, this, _attributeTagManager);
            _attributeTagManager.SetAttributes();
            string html = _htmlByParents[parent];
            _tagParsers = initiateParser.GetTagParsers(html);
            if (_tagParsers != null && _tagParsers.Count > 0)
            {
                foreach (var parser in _tagParsers)
                {
                    html = RemoveUselessHtml(html);
                    var childTag = parser.Parse(html);
                    string htmlToParse = ChildTagHtml(childTag);
                    _parent = _parents.Last();
                    if (!_childrenWithParents.ContainsKey(_parent))
                        _childrenWithParents[_parent] = new List<TagModel>();
                    _childrenWithParents[_parent].Add(childTag);
                    html = html.Replace(htmlToParse, string.Empty);
                }
            }
        }

        private string ChildTagHtml(TagModel childTag)
        {
            if (string.IsNullOrEmpty(childTag.TagEnd))
                return childTag.TagStart;
            else if (string.IsNullOrEmpty(childTag.Content))
                return string.Concat(childTag.TagStart, childTag.TagEnd);
            else
                return string.Concat(childTag.TagStart, childTag.Content, childTag.TagEnd);
        }

        private string RemoveUselessHtml(string html)
        {
            string htmlCleaned = string.Empty;
            bool isBeginTag = false;
            for (int i = 0; i < html.Length; i++)
            {
                char caracter = html[i];
                if (caracter == '<')
                    isBeginTag = true;
                if (isBeginTag)
                    htmlCleaned += caracter;
            }
            return htmlCleaned;
        }

        private List<TagModel> GetChildren()
        {
            var parent = _parents.Last();
            _parents.RemoveAt(_parents.Count - 1);
            if (_childrenWithParents.ContainsKey(parent))
                return _childrenWithParents[parent];
            else
                return null;
        }

        public bool ValidateChildren()
        {
            bool areValid = true;
            foreach (var tagParser in _tagParsers)
            {
                bool isValid = tagParser.IsValid();
                if (!isValid)
                {
                    areValid = false;
                    break;
                }
            }
            return areValid;
        }
    }
}