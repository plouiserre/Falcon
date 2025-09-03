using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class ThParser : TagParser, ITagParser
    {
        private IIdentifyTag _identifyTag;

        public ThParser(IIdentifyTag identifyTag, IAttributeTagManager attributeTagManager) : base(attributeTagManager, NameTagEnum.th)
        {
            _identifyTag = identifyTag;
            _attributeTagManager = attributeTagManager;
        }

        public override TagModel Parse(string html)
        {
            _tag = _identifyTag.Analyze(html);
            return _tag;
        }

        public override bool IsValid()
        {
            bool tagsEnd = !string.IsNullOrEmpty(_tag.TagEnd);
            bool attributesAreOk = AreAttributesAreAutorized();
            bool isValidScopeValue = ValidScopeValue();
            return tagsEnd && attributesAreOk && isValidScopeValue;
        }

        private bool ValidScopeValue()
        {
            var attribute = _tag.Attributes.FirstOrDefault(o => o.FamilyAttribute == FamilyAttributeEnum.scope.ToString());
            var validValue = new string[] { "row", "col", "rowgroup", "colgroup" };
            if (attribute == null)
                return true;
            else
            {
                if (validValue.Contains(attribute.Value))
                    return true;
                else
                    return false;
            }
        }
    }
}