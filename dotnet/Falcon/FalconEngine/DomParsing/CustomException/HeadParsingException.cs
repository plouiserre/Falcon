using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.CustomException
{
    public class HeadParsingException : ParsingException
    {
        public HeadParsingException(ErrorType errorType, string message) : base(errorType, message)
        {

        }
    }
}