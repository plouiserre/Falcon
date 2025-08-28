using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.CustomException
{
    public class UnknownAttributeException : ParsingException
    {
        public UnknownAttributeException(string message) : base(ErrorTypeParsing.unknownAttribute, message)
        {
        }
    }
}