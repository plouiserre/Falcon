using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class HeadParser : ITagParser
    {

        private string _html;
        private string _tagStart;
        private string _tagEnd;
        private IDeleteUselessSpace _deleteUselessSpace;
        private IIdentifyTag _identifyTag;
        private IIdentifyStartTagEndTag _identitfyStartEndTag;
        private IAttributeTagParser _attributeTagParser;
        private IDeterminateContent _determinateContent;

        public HeadParser(IDeleteUselessSpace deleteUselessSpace, IIdentifyTag identifyTag,
            IIdentifyStartTagEndTag identifyStartTagEndTag, IAttributeTagParser attributeTagParser,
            IDeterminateContent determinateContent)
        {
            _identifyTag = identifyTag;
            _deleteUselessSpace = deleteUselessSpace;
            _identitfyStartEndTag = identifyStartTagEndTag;
            _attributeTagParser = attributeTagParser;
            _determinateContent = determinateContent;
        }

        public bool IsValid(TagModel tag)
        {
            return tag.TagStart == _tagStart && tag.TagEnd == _tagEnd;
        }

        public TagModel Parse(string html)
        {
            try
            {
                _html = html;
                _html = CleanHtml();
                var tag = _identifyTag.Analyze(_html);
                _tagStart = tag.TagStart;
                _tagEnd = tag.TagEnd;
                tag.Children = DeterminateChildren(tag.Content);
                return tag;
            }
            catch (Exception ex)
            {
                string message = $"Une erreur a eu lieu lors du parsing de {html}";
                throw new HeadParsingException(ErrorTypeParsing.head, message);
            }
        }


        private string CleanHtml()
        {
            return _deleteUselessSpace.PurgeUselessCaractersAroundTag(_html);
        }

        //TODO check présence des tags start and end
        private string GetContent()
        {
            int count = 0;
            for (int i = 0; i < _html.Length; i++)
            {
                string word = _html.Substring(i, _tagEnd.Length);
                if (word == _tagEnd)
                {
                    count = i;
                    break;
                }
            }
            string contentNotClean = _html.Substring(0, count);
            string content = contentNotClean.Replace(_tagStart, string.Empty);
            return content;
            //faire une exception si on parse mal
        }

        //TODO add good exceptions
        private List<TagModel> DeterminateChildren(string content)
        {
            var initiateParser = new InitiateParser(_deleteUselessSpace, _identifyTag, _identitfyStartEndTag, _attributeTagParser, _determinateContent);
            var children = new List<TagModel>();
            var parsers = initiateParser.GetTagParsers(content);
            foreach (var parser in parsers)
            {
                var tagChild = parser.Parse(content);
                children.Add(tagChild);
                string tagToRemove = CalculateAllTagAnalyze(tagChild);
                content = content.Replace(tagToRemove, string.Empty);
            }
            return children;
        }

        private string CalculateAllTagAnalyze(TagModel tag)
        {
            string allTag = tag.TagStart;
            if (!string.IsNullOrEmpty(tag.Content))
                allTag += tag.Content;
            if (!string.IsNullOrEmpty(tag.TagEnd))
                allTag += tag.TagEnd;
            return allTag;
        }

    }
}