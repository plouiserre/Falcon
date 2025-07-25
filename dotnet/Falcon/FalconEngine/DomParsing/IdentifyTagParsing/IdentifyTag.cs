using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.Parser;
using FalconEngine.DomParsing.Parser.Attribute;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public class IdentifyTag : IIdentifyTag
    {
        private string? _html;
        private string? _tagStart { get; set; }
        private string? _tagEnd { get; set; }
        private string? _content { get; set; }
        private TagFamilyEnum _tagFamily;
        private NameTagEnum _nameTag { get; set; }
        private List<AttributeModel> _attributes;
        private IDeleteUselessSpace _deleteUselessSpace;
        private IAttributeTagParser _attributeTagParser;
        private IIdentifyTagName _identifyTagName;
        private IIdentifyStartTagEndTag _identifyStartTagEndTag;
        private IDeterminateContent _determinateContent;

        public IdentifyTag(IDeleteUselessSpace deleteUselessSpace, IAttributeTagParser attributeTagParser, IIdentifyTagName identifyTagName,
                            IIdentifyStartTagEndTag identifyStartTagEndTag, IDeterminateContent determinateContent)
        {
            _deleteUselessSpace = deleteUselessSpace;
            _attributeTagParser = attributeTagParser;
            _identifyTagName = identifyTagName;
            _identifyStartTagEndTag = identifyStartTagEndTag;
            _determinateContent = determinateContent;
        }

        public TagModel Analyze(string html)
        {
            _html = html;
            _attributes = null;
            CleanHtml();
            FindTagStartEnd();
            FindAttributes();
            IdentifyTagName();
            FindTagFamily();
            FindContent();
            return new TagModel()
            {
                TagStart = _tagStart,
                TagEnd = _tagEnd,
                Attributes = _attributes,
                NameTag = _nameTag,
                TagFamily = _tagFamily,
                Content = _content
            };
        }

        private void CleanHtml()
        {
            var htmlCleaned = _deleteUselessSpace.PurgeUselessCaractersAroundTag(_html);
            _html = htmlCleaned;
        }

        private void FindTagStartEnd()
        {
            _identifyStartTagEndTag.DetermineStartEndTags(_html);
            _tagStart = _identifyStartTagEndTag.StartTag;
            _tagEnd = _identifyStartTagEndTag.EndTag;
        }

        private void FindAttributes()
        {
            bool isAttributeHere = _attributeTagParser.IsAttributePresent(_tagStart);
            if (isAttributeHere)
                _attributes = _attributeTagParser.Parse(_tagStart);
        }

        private void IdentifyTagName()
        {
            _nameTag = _identifyTagName.FindTagName(_tagStart);
        }

        private void FindTagFamily()
        {
            _tagFamily = !string.IsNullOrEmpty(_tagEnd) ? TagFamilyEnum.WithEnd : TagFamilyEnum.NoEnd;
        }

        private void FindContent()
        {
            _content = _determinateContent.FindContent(_html, _tagStart, _tagEnd);
        }
    }
}