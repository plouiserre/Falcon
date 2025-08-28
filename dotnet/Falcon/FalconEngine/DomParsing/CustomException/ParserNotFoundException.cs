using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.CustomException
{
    public class ParserNotFoundException : ParsingException
    {
        public string NameTag { get; set; }

        public ParserNotFoundException(string nameTag, ErrorTypeParsing errorType, string message) : base(errorType, message)
        {
            NameTag = nameTag;
        }
    }
}