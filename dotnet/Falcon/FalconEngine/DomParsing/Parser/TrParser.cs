using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class TrParser : TagParser, ITagParser
    {
        private IdentifyTag _identifyTag;
        private IManageChildrenTag _manageChildrenTag;

        public TrParser(IdentifyTag identifyTag, IManageChildrenTag manageChildrenTag, IAttributeTagManager attributeTagManager) :
                         base(attributeTagManager, NameTagEnum.tr)
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
            return tagsEnd && attributesAreOk;
        }
    }
}