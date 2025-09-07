using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser.List
{
    //TODO when ol will be developed you must updated validation with this rules https://developer.mozilla.org/fr/docs/Web/HTML/Reference/Elements/li
    public class LiParser : TagParser, ITagParser
    {
        private IIdentifyTag _identifyTag;
        private IManageChildrenTag _manageChildrenTag;

        public LiParser(IIdentifyTag identifyTag, IManageChildrenTag manageChildrenTag, IAttributeTagManager attributeTagManager) :
                    base(attributeTagManager, NameTagEnum.li)
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
            bool areChildrenValid = _manageChildrenTag.ValidateChildren(_tag);
            return tagEnd && tagsAreOk && areChildrenValid;
        }
    }
}