using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.CustomException
{
    public class HtmlParsingException : ParsingException
    {
        public HtmlParsingException(ErrorType errorType, string message)
        {
            ErrorType = errorType;
            Message = message;
        }
    }
}