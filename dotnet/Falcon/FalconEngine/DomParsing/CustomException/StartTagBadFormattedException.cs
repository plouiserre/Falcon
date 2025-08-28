using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.CustomException
{
    public class StartTagBadFormattedException : ParsingException
    {
        public string TagBadFormatting { get; set; }

        public StartTagBadFormattedException(string tag, string message) : base(ErrorTypeParsing.starttagbadformatting, message)
        {
            TagBadFormatting = tag;
        }
    }
}