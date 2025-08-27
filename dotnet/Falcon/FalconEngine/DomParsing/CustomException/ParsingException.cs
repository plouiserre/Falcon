using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.CustomException
{
    public enum ErrorTypeParsing
    {
        attributes, children, doctype, head, html, parserNotFoundException, starttagmissing, unknownTag, validation
    }

    public abstract class ParsingException : Exception
    {
        public ErrorTypeParsing ErrorType { get; set; }
        public string Message { get; set; }

        public ParsingException(ErrorTypeParsing errorType, string message)
        {
            ErrorType = errorType;
            Message = message;
        }
    }
}