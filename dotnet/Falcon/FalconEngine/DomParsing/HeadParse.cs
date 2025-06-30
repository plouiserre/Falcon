using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public class HeadParse : ITagParsing
    {

        private string _html;
        private string _tagStart;
        private string _tagEnd;

        public HeadParse()
        {
            _tagStart = "<head>";
            _tagEnd = "</head>";
        }

        public string CleanHtml(TagModel tag, string html)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(TagModel tag)
        {
            return tag.TagStart == _tagStart && tag.TagEnd == _tagEnd;
        }

        public TagModel Parse(string html)
        {
            try
            {
                _html = html;
                string content = GetContent();
                return new TagModel()
                {
                    Content = content,
                    NameTag = NameTagEnum.head,
                    TagFamily = TagFamilyEnum.WithEnd,
                    TagEnd = _tagEnd,
                    TagStart = _tagStart
                };
            }
            catch (Exception ex)
            {
                string message = $"Une erreur a eu lieu lors du parsing de {html}";
                throw new HeadParsingException(ErrorType.head, message);
            }
        }

        //TODO check présence des tags start and end
        private string GetContent()
        {
            int count = 0;
            for (int i = 0; i < _html.Length; i++)
            {
                string word = _html.Substring(i, _tagEnd.Length);
                if (word == _tagEnd)
                {
                    count = i;
                    break;
                }
            }
            string contentNotClean = _html.Substring(0, count);
            string content = contentNotClean.Replace(_tagStart, string.Empty);
            return content;
            //faire une exception si on parse mal
        }
    }
}