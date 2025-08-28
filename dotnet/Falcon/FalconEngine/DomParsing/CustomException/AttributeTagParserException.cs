using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.CustomException
{
    public class AttributeTagParserException : ParsingException
    {
        public string AttributeTagUnknown { get; set; }
        public string StartTag { get; set; }

        public AttributeTagParserException(string attributeTagUnknown, string startTag, string message) : base(ErrorTypeParsing.attributes, message)
        {
            AttributeTagUnknown = attributeTagUnknown;
            StartTag = startTag;
        }
    }
}