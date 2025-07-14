using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    //TODO HTMLTAGPARSER CLEAN TAGS ADD TEST AND STEP IN CODE
    public class HtmlTagParser : ITagParser
    {

        private TagModel _tag;
        private IIdentifyTag _identifyTag;
        private IDeterminateContent _determinateContent;

        public HtmlTagParser(IIdentifyTag identifyTag, IDeterminateContent determinateContent)
        {
            _identifyTag = identifyTag;
            _determinateContent = determinateContent;
        }

        public TagModel Parse(string html)
        {
            try
            {
                _tag = _identifyTag.Analyze(html);
                _tag.Content = _determinateContent.FindContent(html, _tag.TagStart, _tag.TagEnd);
                if (string.IsNullOrEmpty(_tag.TagEnd))
                    throw new HtmlParsingException(ErrorTypeParsing.html, $"Une erreur a eu lieu lors du parsing de {html}");
            }
            catch (Exception ex)
            {
                string message = string.Format($"Une erreur a eu lieu lors du parsing de {html}");
                throw new HtmlParsingException(ErrorTypeParsing.html, message);
            }
            return _tag;
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