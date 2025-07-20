using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.Parser.Attribute
{
    public interface IAnalyzeAttributes
    {
        List<string> Study(string startTag);
    }
}