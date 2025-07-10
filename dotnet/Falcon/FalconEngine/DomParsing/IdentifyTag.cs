using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public class IdentifyTag : IIdentifyTag
    {
        private string? _html;
        private string? _tagStart { get; set; }
        private string? _tagEnd { get; set; }
        private IDeleteUselessSpace _deleteUselessSpace;

        public IdentifyTag(IDeleteUselessSpace deleteUselessSpace)
        {
            _deleteUselessSpace = deleteUselessSpace;
        }

        public TagModel Analyze(string html)
        {
            _html = html;
            FindTagStart();
            FindTagEnd();
            return new TagModel()
            {
                TagStart = _tagStart,
                TagEnd = _tagEnd
            };
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
            _tagStart = _html?.Substring(0, position + 1);
            _tagStart = _deleteUselessSpace.PurgeUselessCaractersAroundTag(_tagStart);
        }

        private void FindTagEnd()
        {
            string cleanTagStart = _tagStart.Replace("<", string.Empty).Replace(">", string.Empty);
            string baseTag = cleanTagStart.Split(" ")[0];
            string tagEndCandidate = string.Concat("</", baseTag, ">");
            _tagEnd = _html.Contains(tagEndCandidate) ? tagEndCandidate : null;
        }
    }
}