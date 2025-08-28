using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.CustomException
{
    public class UnknownTagException : ParsingException
    {
        public UnknownTagException(string message) : base(ErrorTypeParsing.unknownTag, message)
        {
        }
    }
}