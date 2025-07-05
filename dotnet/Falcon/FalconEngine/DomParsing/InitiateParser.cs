using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;

namespace FalconEngine.DomParsing
{
    public class InitiateParser
    {
        private string _html;
        private string _startTag;
        public InitiateParser()
        {

        }

        public List<ITagParser> GetTagParsers(string html)
        {
            _html = html;
            var parsers = new List<ITagParser>();
            while (_html.Length > 0)
            {
                var parser = GetTagParser();
                parsers.Add(parser);
            }
            return parsers;
        }

        //TODO subdivise in three differents methods one already did!!!
        private ITagParser GetTagParser()
        {
            GetStartTag();
            string endTag = GetEndTag();
            RemoveUselessHtml(_startTag, endTag);
            return ChooseGoodTagParser();
        }

        private void GetStartTag()
        {
            int start = 0;
            int end = 0;
            for (int i = 0; i < _html.Length; i++)
            {
                char caracter = _html[i];
                if (caracter == '<')
                {
                    start = i;
                    continue;
                }
                else if (caracter == '>')
                {
                    end = i;
                    break;
                }
            }
            _startTag = _html.Substring(start, end - start + 1);
        }

        private ITagParser ChooseGoodTagParser()
        {
            switch (_startTag)
            {
                case string tag when tag.ToLower().Contains("doctype"):
                    return new DoctypeParser();
                case string tag when tag.ToLower().Contains("html"):
                    return new HtmlTagParser();
                case string tag when tag.ToLower().Contains("head"):
                    return new HeadParser();
                case string tag when tag.ToLower().Contains("meta"):
                    return new MetaParser();
                case string tag when tag.ToLower().Contains("link"):
                    return new LinkParser();
                case string tag when tag.ToLower().Contains("title"):
                    return new TitleParser();
                default:
                    return null;
            }
        }

        private string GetEndTag()
        {
            string coreTag = _startTag.Split(" ")[0].Replace("<", string.Empty).Replace(">", string.Empty);
            string endTagPossible = string.Concat("</", coreTag, ">");
            if (_html.Contains(endTagPossible))
                return endTagPossible;
            else
                return string.Empty;
        }

        private void RemoveUselessHtml(string startTag, string endTag)
        {
            if (endTag == string.Empty)
                _html = _html.Replace(startTag, string.Empty);
            else
            {
                var startTagIndex = _html.IndexOf(startTag);
                var endTagIndex = _html.IndexOf(endTag);
                string htmlToRemove = _html.Substring(startTagIndex, endTagIndex - startTagIndex + endTag.Length);
                _html = _html.Replace(htmlToRemove, string.Empty);
            }
        }
    }
}