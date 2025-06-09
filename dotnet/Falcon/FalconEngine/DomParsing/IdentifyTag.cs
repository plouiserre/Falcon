using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing
{
    public class IdentifyTag
    {
        private string? _html;
        public string? TagStart { get; set; }
        public string? TagEnd { get; set; }

        public void Analyze(string html)
        {
            _html = html;
            FindTagStart();
            FindTagEnd();
        }

        private void FindTagStart()
        {
            int position = 0;
            for (int i = 0; i < _html?.Length; i++)
            {
                char caracter = _html[i];
                if (caracter == '>')
                {
                    position = i;
                    break;
                }
            }
            TagStart = _html?.Substring(0, position + 1);
        }

        private void FindTagEnd()
        {
            string cleanTagStart = TagStart.Replace("<", string.Empty).Replace(">", string.Empty);
            string baseTag = cleanTagStart.Split(" ")[0];
            TagEnd = string.Concat("</", baseTag, ">");
        }
    }
}