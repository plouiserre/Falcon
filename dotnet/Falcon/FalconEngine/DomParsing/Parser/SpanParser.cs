using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class SpanParser : TagParser, ITagParser
    {
        private IIdentifyTag _identifyTag;
        private IManageChildrenTag _manageChildrenTag;

        public SpanParser(IIdentifyTag identifyTag, IAttributeTagManager attributeTagManager,
                IManageChildrenTag manageChildrenTag, NameTagEnum nameTag) : base(attributeTagManager, nameTag)
        {
            _identifyTag = identifyTag;
            _manageChildrenTag = manageChildrenTag;
        }

        public override bool IsValid()
        {
            bool noTagEnd = !string.IsNullOrEmpty(_tag.TagEnd);
            bool tagsAreOk = AreAttributesAreAutorized();
            return noTagEnd && tagsAreOk;
        }

        public override TagModel Parse(string html)
        {
            _tag = _identifyTag.Analyze(html);
            _tag.Children = _manageChildrenTag.Identify(_tag.Content);
            return _tag;
        }
    }
}