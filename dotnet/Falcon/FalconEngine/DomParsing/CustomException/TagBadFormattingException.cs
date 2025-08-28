using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.CustomException
{
    public class TagBadFormattingException : ParsingException
    {
        public TagBadFormattingException(string message) : base(ErrorTypeParsing.badFormatting, message)
        {
        }
    }
}