using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public class DoctypeParse : ITagParsing
    {
        public TagModel Parse(string html)
        {
            var identifyTag = new IdentifyTag();
            identifyTag.Analyze(html);
            var tag = new TagModel()
            {
                TagStart = identifyTag.TagStart,
                TagEnd = identifyTag.TagEnd,
                Attributes = null,
                Content = string.Empty,
                NameTag = NameTagEnum.doctype,
                TagFamily = TagFamilyEnum.NoEnd
            };

            return tag;
        }
    }
}