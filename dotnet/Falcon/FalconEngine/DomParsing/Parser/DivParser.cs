using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class DivParser : TagParser, ITagParser
    {
        private IdentifyTag _identifyTag;

        public DivParser(IAttributeTagManager attributeTagManager, IdentifyTag identifyTag) :
                    base(attributeTagManager, NameTagEnum.div)
        {
            _identifyTag = identifyTag;
        }

        public override bool IsValid()
        {
            bool tagEnd = !string.IsNullOrEmpty(_tag.TagEnd);
            bool tagsAreOk = AreAttributesAreAutorized();
            return tagEnd && tagsAreOk;
        }

        public override TagModel Parse(string html)
        {
            _tag = _identifyTag.Analyze(html);
            return _tag;
        }
    }
}