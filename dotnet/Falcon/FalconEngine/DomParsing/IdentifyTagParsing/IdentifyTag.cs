using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public class IdentifyTag : IIdentifyTag
    {
        private string? _html;
        private string? _tagStart { get; set; }
        private string? _tagEnd { get; set; }
        private TagFamilyEnum _tagFamily;
        private NameTagEnum _nameTag { get; set; }
        private List<AttributeModel> _attributes;
        private IDeleteUselessSpace _deleteUselessSpace;
        private IAttributeTagParser _attributeTagParser;
        private IIdentifyTagName _identifyTagName;
        private IIdentifyStartTagEndTag _identifyStartTagEndTag;

        public IdentifyTag(IDeleteUselessSpace deleteUselessSpace, IAttributeTagParser attributeTagParser, IIdentifyTagName identifyTagName,
                            IIdentifyStartTagEndTag identifyStartTagEndTag)
        {
            _deleteUselessSpace = deleteUselessSpace;
            _attributeTagParser = attributeTagParser;
            _identifyTagName = identifyTagName;
            _identifyStartTagEndTag = identifyStartTagEndTag;
        }

        public TagModel Analyze(string html)
        {
            _html = html;
            _attributes = null;
            FindTagStartEnd();
            FindAttributes();
            IdentifyTagName();
            FindTagFamily();
            return new TagModel()
            {
                TagStart = _tagStart,
                TagEnd = _tagEnd,
                Attributes = _attributes,
                NameTag = _nameTag,
                TagFamily = _tagFamily
            };
        }

        private void FindTagStartEnd()
        {
            var htmlCleaned = _deleteUselessSpace.PurgeUselessCaractersAroundTag(_html);
            _identifyStartTagEndTag.DetermineStartEndTags(htmlCleaned);
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
    }
}