using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class DoctypeParser : ITagParser
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


        public bool IsValid(TagModel tag)
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
            const string TAGXHTM1STRICT = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">";
            const string TAGXHTM1TRANSITIONAL = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
            const string TAGXHTM1FRAMESET = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Frameset//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd\">";
            bool isGoodTagModern = false;
            bool isGoodTagHtml401 = false;
            if (tagStart.ToLower() == TAGMODERN)
                isGoodTagModern = true;
            if (tagStart == TAGHTM401STRICT || tagStart == TAGHTM401TRANSITIONAL || tagStart == TAGHTM401FRAMESET || tagStart == TAGXHTM1STRICT ||
                tagStart == TAGXHTM1TRANSITIONAL || tagStart == TAGXHTM1FRAMESET)
                isGoodTagHtml401 = true;
            return isGoodTagModern || isGoodTagHtml401;
        }

        public string CleanHtml(TagModel tag, string html)
        {
            throw new NotImplementedException();
        }
    }
}