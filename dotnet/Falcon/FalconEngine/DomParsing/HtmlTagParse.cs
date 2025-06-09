using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public class HtmlTagParse : ITagParsing
    {

        private TagModel _tag;

        public TagModel Parse(string html)
        {
            _tag = new TagModel()
            {
                NameTag = NameTagEnum.html,
                TagFamily = TagFamilyEnum.WithEnd,
                TagStart = "<html lang=\"en\" dir=\"auto\">",
                TagEnd = "</html>"
            };
            _tag.Content = CleanText(html);
            _tag.Attributes = GetAttributsHtml();
            return _tag;
        }

        public string CleanText(string html)
        {
            var deleteUselessSpace = new DeleteUselessSpace(_tag);
            string contentCleaned = deleteUselessSpace.CleanContent(html);
            return contentCleaned;
        }

        public List<AttributeModel> GetAttributsHtml()
        {
            var discoverAttributs = new DiscoverAttributs();
            var attributs = discoverAttributs.Find(_tag.TagStart);
            return attributs;
        }
    }
}