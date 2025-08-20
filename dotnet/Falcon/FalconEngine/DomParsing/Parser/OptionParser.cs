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
            var tag = _identifyTag.Analyze(html);
            return tag;
        }
    }
}