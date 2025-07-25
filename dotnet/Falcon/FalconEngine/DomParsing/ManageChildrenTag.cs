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
        private IAttributeTagParser _attributeTagParser;
        private IDeterminateContent _determinateContent;
        private IExtractHtmlRemaining _extractHtmlRemaining;
        private IAttributeTagManager _attributeTagManager;
        private IList<ITagParser> _tagParsers;
        private string _html;
        private List<TagModel> _children;

        public ManageChildrenTag(IDeleteUselessSpace deleteUselessSpace, IIdentifyTag identifyTag,
            IIdentifyStartTagEndTag identifyStartTagEndTag, IAttributeTagParser attributeTagParser,
            IDeterminateContent determinateContent, IExtractHtmlRemaining extractHtmlRemaining,
            IAttributeTagManager attributeTagManager)
        {
            _identifyTag = identifyTag;
            _deleteUselessSpace = deleteUselessSpace;
            _identitfyStartEndTag = identifyStartTagEndTag;
            _attributeTagParser = attributeTagParser;
            _determinateContent = determinateContent;
            _extractHtmlRemaining = extractHtmlRemaining;
            _attributeTagManager = attributeTagManager;
        }

        public List<TagModel> Identify(string html)
        {
            _html = html;
            _children = new List<TagModel>();
            try
            {
                SearchChildren();
            }
            catch (NoStartTagException ex)
            {
                return _children;
            }
            catch (Exception ex)
            {
                throw new DeterminateChildrenException(ErrorTypeParsing.children, $"Error parsing for the children of  {html}");
            }
            return _children;
        }

        private void SearchChildren()
        {
            var initiateParser = new InitiateParser(_deleteUselessSpace, _identifyTag, _identitfyStartEndTag, _determinateContent, this, _attributeTagManager);
            _attributeTagManager.SetAttributes();
            _tagParsers = initiateParser.GetTagParsers(_html);
            foreach (var parser in _tagParsers)
            {
                var tagChild = parser.Parse(_html);
                _children.Add(tagChild);
                _html = _extractHtmlRemaining.Extract(tagChild, _html, ExtractionMode.ASide);
            }
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