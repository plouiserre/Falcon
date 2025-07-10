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
        private IIdentifyTag _identifyTag;

        public HtmlTagParser(IIdentifyTag identifyTag)
        {
            _identifyTag = identifyTag;
        }

        public TagModel Parse(string html)
        {
            try
            {
                _tag = _identifyTag.Analyze(html);
                _tag.NameTag = NameTagEnum.html;
                _tag.TagFamily = TagFamilyEnum.WithEnd;
                _tag.Content = GetContent(html);
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
            return html.Replace(_tag.TagStart, string.Empty).Replace(_tag.TagEnd, string.Empty);
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