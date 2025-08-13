using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class AParser : TagParser, ITagParser
    {
        private IIdentifyTag _identifyTag;
        private IDeleteUselessSpace _deleteUselessSpace;

        public AParser(IIdentifyTag identifyTag, IAttributeTagManager attributeTagManager, IDeleteUselessSpace deleteUselessSpace) : base(attributeTagManager, NameTagEnum.a)
        {
            _identifyTag = identifyTag;
            _deleteUselessSpace = deleteUselessSpace;
        }

        public override bool IsValid()
        {
            bool tagEnd = !string.IsNullOrEmpty(_tag.TagEnd);
            bool tagsAreOk = AreAttributesAreAutorized();
            return tagEnd && tagsAreOk;
        }

        public override TagModel Parse(string html)
        {
            string cleanHtml = _deleteUselessSpace.PurgeUselessCaractersAroundTag(html);
            _tag = _identifyTag.Analyze(cleanHtml);
            return _tag;
        }
    }
}