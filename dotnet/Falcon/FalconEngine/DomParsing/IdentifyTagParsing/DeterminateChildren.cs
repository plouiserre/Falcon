using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public class DeterminateChildren : IDeterminateChildren
    {
        private IDeleteUselessSpace _deleteUselessSpace;
        private IIdentifyTag _identifyTag;
        private IIdentifyStartTagEndTag _identitfyStartEndTag;
        private IAttributeTagParser _attributeTagParser;
        private IDeterminateContent _determinateContent;

        public DeterminateChildren(IDeleteUselessSpace deleteUselessSpace, IIdentifyTag identifyTag,
            IIdentifyStartTagEndTag identifyStartTagEndTag, IAttributeTagParser attributeTagParser,
            IDeterminateContent determinateContent)
        {
            _identifyTag = identifyTag;
            _deleteUselessSpace = deleteUselessSpace;
            _identitfyStartEndTag = identifyStartTagEndTag;
            _attributeTagParser = attributeTagParser;
            _determinateContent = determinateContent;
        }

        public List<TagModel> Find(string html)
        {
            var initiateParser = new InitiateParser(_deleteUselessSpace, _identifyTag, _identitfyStartEndTag, _attributeTagParser, _determinateContent, this);
            var children = new List<TagModel>();
            var parsers = initiateParser.GetTagParsers(html);
            foreach (var parser in parsers)
            {
                var tagChild = parser.Parse(html);
                children.Add(tagChild);
                string tagToRemove = CalculateAllTagAnalyze(tagChild);
                html = html.Replace(tagToRemove, string.Empty);
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