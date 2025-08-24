using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class OptionParser : TagParser, ITagParser
    {
        private IIdentifyTag _identifyTag;
        public OptionParser(IIdentifyTag identifyTag, IAttributeTagManager attributeTagManager)
                                                : base(attributeTagManager, NameTagEnum.option)
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
            return tagsEnd && attributesAreOk;
        }
    }
}