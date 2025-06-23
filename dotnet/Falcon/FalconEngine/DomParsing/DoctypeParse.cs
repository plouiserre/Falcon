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
            tag.IsValid = IsValid(tag);
            return tag;
        }

        private bool IsValid(TagModel tag)
        {
            bool isTagStartGoodFormatting = false;
            bool isNoTagEnd = false;
            bool isNoContent = false;
            bool isGoodNameTag = false;
            isTagStartGoodFormatting = IsGoodTagStart(tag.TagStart);
            if (tag.TagEnd == null)
                isNoTagEnd = true;
            if (string.IsNullOrEmpty(tag.Content))
                isNoContent = true;
            if (tag.NameTag == NameTagEnum.doctype)
                isGoodNameTag = true;
            return isGoodNameTag && isNoContent && isNoTagEnd && isTagStartGoodFormatting;
        }

        private bool IsGoodTagStart(string tagStart)
        {
            const string TAGMODERN = "<!doctype html>";
            const string TAGHTM401STRICT = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">";
            const string TAGHTM401TRANSITIONAL = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">";
            const string TAGHTM401FRAMESET = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Frameset//EN\" \"http://www.w3.org/TR/html4/frameset.dtd\">";
            bool isGoodTagModern = false;
            bool isGoodTagHtml401 = false;
            if (tagStart.ToLower() == TAGMODERN)
                isGoodTagModern = true;
            if (tagStart == TAGHTM401STRICT || tagStart == TAGHTM401TRANSITIONAL || tagStart == TAGHTM401FRAMESET)
                isGoodTagHtml401 = true;
            return isGoodTagModern || isGoodTagHtml401;
        }
    }
}