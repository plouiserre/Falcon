using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class InputParser : TagParser, ITagParser
    {
        private IIdentifyTag _identifyTag;

        public InputParser(IIdentifyTag identifyTag, IAttributeTagManager attributeTagManager) : base(attributeTagManager, NameTagEnum.input)
        {
            _identifyTag = identifyTag;
        }

        public override TagModel Parse(string html)
        {
            _tag = _identifyTag.Analyze(html);
            return _tag;
        }
    }
}