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
            var parsers = new List<ITagParser>()
            {
                new MetaParser(),
                new MetaParser(),
                new TitleParser(),
                new LinkParser()
            };
            return parsers;
        }
    }
}