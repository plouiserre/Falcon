using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;

namespace FalconEngine.DomParsing
{
    public class InitiateParser
    {
        public List<ITagParser> GetTagParsers(string html)
        {
            // var parsers = new List<ITagParser>()
            // {
            //     new MetaParser(),
            //     new MetaParser(),
            //     new TitleParser(),
            //     new LinkParser()
            // };
            // return parsers;
            var parser = GetTagParser(html);
            var parsers = new List<ITagParser>()
            {
                parser
            };
            return parsers;
        }

        //for the moment I mock that but in the futur I will put in automatic
        private List<string> SeparateInsideHtml(string html)
        {
            List<string> results = new List<string>()
            {
                "<meta charset=\"UTF-8\">",
                "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">",
                "<title>Document</title>",
                "<link rel=\"stylesheet\" href=\"main.css\">"
            };
            return results;
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