using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class HeadParser : ITagParser
    {

        private string _html;
        private string _tagStart;
        private string _tagEnd;

        public HeadParser()
        {
            _tagStart = "<head>";
            _tagEnd = "</head>";
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
                _html = CleanHtml();
                string content = GetContent();
                return new TagModel()
                {
                    Content = content,
                    NameTag = NameTagEnum.head,
                    TagFamily = TagFamilyEnum.WithEnd,
                    TagEnd = _tagEnd,
                    TagStart = _tagStart,
                    Children = DeterminateChildren(content)
                };
            }
            catch (Exception ex)
            {
                string message = $"Une erreur a eu lieu lors du parsing de {html}";
                throw new HeadParsingException(ErrorTypeParsing.head, message);
            }
        }


        private string CleanHtml()
        {
            string htmlWorking = _html;
            htmlWorking = htmlWorking.Replace("\n", string.Empty);
            htmlWorking = htmlWorking.Replace("\r", string.Empty);
            return htmlWorking;
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

        private List<TagModel> DeterminateChildren(string content)
        {
            var initiateParser = new InitiateParser();
            var children = new List<TagModel>();
            var parsers = initiateParser.GetTagParsers(content);
            foreach (var parser in parsers)
            {
                content = RemoveUselessSpace(content);
                var tagChild = parser.Parse(content);
                children.Add(tagChild);
                string tagToRemove = CalculateAllTagAnalyze(tagChild);
                content = content.Replace(tagToRemove, string.Empty);
            }
            return children;
        }

        //TODO même code présent dans initiateParser à remettre ailleurs ici CleanHtml!!!
        private string RemoveUselessSpace(string content)
        {
            int goodStartHtml = LocateFirstCaracter(content);
            return content.Substring(goodStartHtml, content.Length - goodStartHtml);
        }

        //TODO même code présent dans initiateParser à remettre ailleurs!!!
        private int LocateFirstCaracter(string content)
        {
            int localisation = 0;
            for (int i = 0; i < content.Length; i++)
            {
                char caracter = content[i];
                if (caracter != ' ')
                {
                    localisation = i;
                    break;
                }
            }
            if (localisation == 0)
                localisation = content.Length;
            return localisation;
        }

        private string CalculateAllTagAnalyze(TagModel tag)
        {
            string allTag = tag.TagStart;
            if (!string.IsNullOrEmpty(tag.Content))
                allTag += tag.Content;
            if (!string.IsNullOrEmpty(tag.TagEnd))
                allTag += tag.TagEnd;
            return allTag;
        }

    }
}