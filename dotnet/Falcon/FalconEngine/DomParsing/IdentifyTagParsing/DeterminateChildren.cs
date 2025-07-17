using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.CustomException;
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
        private IExtractHtmlRemaining _extractHtmlRemaining;
        private IAttributeTagManager _attributeTagManager;

        public DeterminateChildren(IDeleteUselessSpace deleteUselessSpace, IIdentifyTag identifyTag,
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

        public List<TagModel> Find(string html)
        {
            var children = new List<TagModel>();
            try
            {
                var initiateParser = new InitiateParser(_deleteUselessSpace, _identifyTag, _identitfyStartEndTag, _attributeTagParser, _determinateContent, this, _extractHtmlRemaining, _attributeTagManager);
                var parsers = initiateParser.GetTagParsers(html);
                foreach (var parser in parsers)
                {
                    var tagChild = parser.Parse(html);
                    children.Add(tagChild);
                    html = _extractHtmlRemaining.Extract(tagChild, html, ExtractionMode.ASide);
                }
            }
            catch (Exception ex)
            {
                throw new DeterminateChildrenException(ErrorTypeParsing.children, $"Error parsing for the children of  {html}");
            }
            return children;
        }
    }
}