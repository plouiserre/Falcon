using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class ScriptParser : TagParser, ITagParser
    {
        private IIdentifyTag _identifyTag;

        public ScriptParser(IIdentifyTag identifyTag, IAttributeTagManager attributeTagManager) :
                    base(attributeTagManager, NameTagEnum.script)
        {
            _identifyTag = identifyTag;
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
            bool isReferrerPolicyRuleValid = IsValidReffererPolicyAttribute();
            return tagsEnd && attributesAreOk && isReferrerPolicyRuleValid;
        }

        private bool IsValidReffererPolicyAttribute()
        {
            var valuesAutorized = new string[] {"no-referrer", "no-referrer-when-downgrade", "origin", "", "origin-when-cross-origin",
            "same-origin", "strict-origin", "strict-origin-when-cross-origin", "unsafe-url" };
            if (!_tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.referrerpolicy.ToString()))
                return true;
            else
            {
                var attribute = _tag.Attributes.First(o => o.FamilyAttribute == FamilyAttributeEnum.referrerpolicy.ToString());
                if (valuesAutorized.Contains(attribute.Value))
                    return true;
                else
                    return false;
            }
        }
    }
}