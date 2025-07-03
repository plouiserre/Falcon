using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class HtmlTagParser : ITagParser
    {

        private TagModel _tag;
        private IdentifyTag _identifyTag;

        public HtmlTagParser()
        {
            _identifyTag = new IdentifyTag();
        }

        public TagModel Parse(string html)
        {
            try
            {
                _identifyTag.Analyze(html);
                _tag = new TagModel()
                {
                    NameTag = NameTagEnum.html,
                    TagFamily = TagFamilyEnum.WithEnd,
                    TagStart = _identifyTag.TagStart,
                    TagEnd = _identifyTag.TagEnd
                };
                _tag.Content = CleanText(html);
                _tag.Attributes = GetAttributsHtml();
            }
            catch (Exception ex)
            {
                string message = string.Format($"Une erreur a eu lieu lors du parsing de {html}");
                throw new HtmlParsingException(ErrorTypeParsing.html, message);
            }
            return _tag;
        }

        private string CleanText(string html)
        {
            var deleteUselessSpace = new DeleteUselessSpace(_tag);
            string contentCleaned = deleteUselessSpace.CleanContent(html);
            return contentCleaned;
        }

        private List<AttributeModel> GetAttributsHtml()
        {
            var discoverAttributs = new DiscoverAttributs();
            var attributs = discoverAttributs.Find(_tag.TagStart);
            return attributs;
        }

        public bool IsValid(TagModel tag)
        {
            if (tag.TagEnd == "</html>" && tag.TagStart.Contains("html"))
                return true;
            else
                return false;
        }

        public string CleanHtml(TagModel tag, string html)
        {
            throw new NotImplementedException();
        }

        public List<TagModel> DeterminateChildren(string html)
        {
            throw new NotImplementedException();
        }
    }
}