using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;

namespace FalconEngine.DomParsing
{
    public interface IInitiateParser
    {
        List<ITagParser> GetTagParsers(string html);
    }
}