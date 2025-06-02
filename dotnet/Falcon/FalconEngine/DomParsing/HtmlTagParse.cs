using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public class HtmlTagParse : ITagParsing
    {

        private TagModel _tag;

        public TagModel Parse(string html)
        {
            string content = html.Replace("<html>", string.Empty);
            content = content.Replace("</html>", string.Empty);
            content = CleanText(content);
            _tag = new TagModel()
            {
                Content = content,
                NameTag = NameTagEnum.html,
                TagFamily = TagFamilyEnum.WithEnd
            };
            return _tag;
        }

        public string CleanText(string html)
        {
            string textCleaned = html;
            textCleaned = textCleaned.Replace("\n", string.Empty);
            textCleaned = textCleaned.Replace("\t", string.Empty);
            textCleaned = textCleaned.Trim();
            return textCleaned;
        }
    }
}