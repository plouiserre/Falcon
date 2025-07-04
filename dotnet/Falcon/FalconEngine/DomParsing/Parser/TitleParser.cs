using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class TitleParser : ITagParser
    {
        private string _html;
        private string _startTag;
        private string _endTag;
        private string _content;

        public string CleanHtml(TagModel tag, string html)
        {
            throw new NotImplementedException();
        }

        public List<TagModel> DeterminateChildren(string html)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(TagModel tag)
        {
            throw new NotImplementedException();
        }

        public TagModel Parse(string html)
        {
            _html = html;
            GetTagStart();
            GetTagEnd();
            GetContent();
            return new TagModel()
            {
                NameTag = NameTagEnum.title,
                Content = _content,
                TagFamily = TagFamilyEnum.WithEnd,
                TagStart = _startTag,
                TagEnd = _endTag
            };
        }

        //TODO externalize in one place
        private void GetTagStart()
        {
            int startIndex = 0;
            int endIndex = 0;
            for (int i = 0; i < _html.Length; i++)
            {
                char caracter = _html[i];
                if (caracter == '<')
                    startIndex = i;
                else if (caracter == '>')
                {
                    endIndex = i;
                    break;
                }
            }
            _startTag = _html.Substring(startIndex, endIndex - startIndex + 1);
        }

        private void GetTagEnd()
        {
            _endTag = _startTag.Replace("<", "</");
        }

        private void GetContent()
        {
            _content = _html.Replace(_startTag, string.Empty).Replace(_endTag, string.Empty);
        }
    }
}