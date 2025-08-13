using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class BodyParser : TagParser, ITagParser
    {
        private IIdentifyTag _identifyTag;
        private IManageChildrenTag _manageChildrenTag;
        public BodyParser(IIdentifyTag identifyTag, IManageChildrenTag manageChildrenTag, IAttributeTagManager attributeTagManager) :
                base(attributeTagManager, NameTagEnum.body)
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
            bool tagEnd = !string.IsNullOrEmpty(_tag.TagEnd);
            bool tagsAreOk = AreAttributesAreAutorized();
            bool areChildrenValid = _manageChildrenTag.ValidateChildren();
            return tagEnd && tagsAreOk && areChildrenValid;
        }
    }
}