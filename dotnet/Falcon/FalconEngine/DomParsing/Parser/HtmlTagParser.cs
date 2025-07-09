using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    //TODO HTMLTAGPARSER CLEAN TAGS ADD TEST AND STEP IN CODE
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
                _tag.Content = GetContent(html);
                _tag.Attributes = GetAttributsHtml();
            }
            catch (Exception ex)
            {
                string message = string.Format($"Une erreur a eu lieu lors du parsing de {html}");
                throw new HtmlParsingException(ErrorTypeParsing.html, message);
            }
            return _tag;
        }

        private string GetContent(string html)
        {
            return html.Replace(_identifyTag.TagStart, string.Empty).Replace(_identifyTag.TagEnd, string.Empty);
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