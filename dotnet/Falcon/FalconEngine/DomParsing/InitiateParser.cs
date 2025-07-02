using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;

namespace FalconEngine.DomParsing
{
    public class InitiateParser
    {
        public InitiateParser()
        {

        }

        public List<ITagParser> GetTagParsers(string html)
        {
            var parser = GetTagParser(html);
            var parsers = new List<ITagParser>()
            {
                parser
            };
            return parsers;
        }

        //TODO subdivise in two differents methods!!!
        private ITagParser GetTagParser(string html)
        {
            int start = 0;
            int end = 0;
            for (int i = 0; i < html.Length; i++)
            {
                char caracter = html[i];
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
            string startTag = html.Substring(start, end - start);
            switch (startTag)
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
    }
}