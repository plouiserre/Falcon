using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.CustomException;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public class IdentifyStartTagEndTag : IIdentifyStartTagEndTag
    {
        public string StartTag { get; set; }
        public string EndTag { get; set; }
        private string _html;

        public void DetermineStartEndTags(string html)
        {
            _html = html;
            StartTag = CalculateStartTag();
            EndTag = CalculateEndTag();
        }


        private string CalculateStartTag()
        {
            string htmlWorking = _html;
            int startStartTag = _html.IndexOf('<');
            int endStartTag = _html.IndexOf('>');
            if (startStartTag == -1 || endStartTag == -1)
                throw new NoStartTagException(ErrorTypeParsing.starttagmissing, $"{_html} doesn't contains tags");
            return htmlWorking.Substring(startStartTag, endStartTag + 1);
        }

        private string CalculateEndTag()
        {
            string tag = StartTag.Replace("<", string.Empty).Replace(">", string.Empty);
            string cleanTag = tag.Split(' ')[0];
            string potentialEndTag = string.Concat("</", cleanTag, ">");
            return _html.Contains(potentialEndTag) ? potentialEndTag : null;
        }
    }
}