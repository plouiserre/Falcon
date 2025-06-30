using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.CustomException
{
    public enum ErrorType
    {
        html, doctype, head
    }

    public abstract class ParsingException : Exception
    {
        public ErrorType ErrorType { get; set; }
        public string Message { get; set; }

        public ParsingException(ErrorType errorType, string message)
        {
            ErrorType = errorType;
            Message = message;
        }
    }
}