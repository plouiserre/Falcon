using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class TBodyParser : TagParser, ITagParser
    {
        private IIdentifyTag _identifyTag;
        private IManageChildrenTag _manageChildrenTag;

        public TBodyParser(IIdentifyTag identifyTag, IManageChildrenTag manageChildrenTag, IAttributeTagManager attributeTagManager) : base(attributeTagManager, NameTagEnum.tbody)
        {
            _identifyTag = identifyTag;
            _manageChildrenTag = manageChildrenTag;
        }

        public override TagModel Parse(string html)
        {
            _tag = _identifyTag.Analyze(html);
            _tag.Children = _manageChildrenTag.Identify(_tag, _tag.Content);
            return _tag;
        }

        public override bool IsValid()
        {
            bool tagsEnd = !string.IsNullOrEmpty(_tag.TagEnd);
            bool attributesAreOk = AreAttributesAreAutorized();
            bool childrenAreValid = _manageChildrenTag.ValidateChildren(_tag);
            return tagsEnd && attributesAreOk && childrenAreValid;
        }
    }
}