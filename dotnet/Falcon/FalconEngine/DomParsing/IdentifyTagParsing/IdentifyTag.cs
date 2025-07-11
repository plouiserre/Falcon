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

        public IdentifyTag(IDeleteUselessSpace deleteUselessSpace, IAttributeTagParser attributeTagParser, IIdentifyTagName identifyTagName)
        {
            _deleteUselessSpace = deleteUselessSpace;
            _attributeTagParser = attributeTagParser;
            _identifyTagName = identifyTagName;
        }

        public TagModel Analyze(string html)
        {
            _html = html;
            _attributes = null;
            FindTagStart();
            FindTagEnd();
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

        private void FindTagStart()
        {
            int position = 0;
            for (int i = 0; i < _html?.Length; i++)
            {
                char caracter = _html[i];
                if (caracter == '>')
                {
                    position = i;
                    break;
                }
            }
            _tagStart = _html?.Substring(0, position + 1);
            _tagStart = _deleteUselessSpace.PurgeUselessCaractersAroundTag(_tagStart);
        }

        private void FindTagEnd()
        {
            string cleanTagStart = _tagStart.Replace("<", string.Empty).Replace(">", string.Empty);
            string baseTag = cleanTagStart.Split(" ")[0];
            string tagEndCandidate = string.Concat("</", baseTag, ">");
            _tagEnd = _html.Contains(tagEndCandidate) ? tagEndCandidate : null;
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