using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class LinkParser : TagParser, ITagParser
    {
        private IIdentifyTag _identifyTag;

        public LinkParser(IIdentifyTag identifyTag, IAttributeTagManager attributeTagManager) :
                    base(attributeTagManager, NameTagEnum.link)
        {
            _identifyTag = identifyTag;
            _attributeTagManager = attributeTagManager;
        }

        public bool IsValid()
        {
            return base.IsValid();
        }

        public override TagModel Parse(string html)
        {
            _tag = _identifyTag.Analyze(html);
            return _tag;
        }
    }
}