using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.CleanData
{
    public class DeleteUselessSpace : IDeleteUselessSpace
    {
        private TagModel _tag;
        private string _startTag;
        private string _endTag;

        public DeleteUselessSpace(TagModel tag)
        {
            _tag = tag;
        }

        public string PurgeUselessCaractersAroundTag(string html)
        {
            string htmlPurgeBeforeTag = PurgeBeforeTag(html);
            string htmlPurgeWrongCaracter = PurgeWrongCaracterInsideTag(htmlPurgeBeforeTag);
            string htmlPurge = PurgeAfterTag(htmlPurgeWrongCaracter);
            return htmlPurge;
        }

        private string PurgeBeforeTag(string html)
        {
            string htmlWorking = html;
            int goodStartHtml = LocateFirstCorrectCaracter(htmlWorking);
            return htmlWorking.Substring(goodStartHtml, htmlWorking.Length - goodStartHtml);
        }

        private int LocateFirstCorrectCaracter(string content)
        {
            int Localisation = 0;
            bool IsOpenBracket = false;
            for (int i = 0; i < content.Length; i++)
            {
                char caracter = content[i];
                if (caracter != ' ' && caracter != '\n' && caracter != '\r')
                {
                    Localisation = i;
                    IsOpenBracket = true;
                    break;
                }
            }
            if (!IsOpenBracket)
                Localisation = content.Length;
            return Localisation;
        }

        private string PurgeWrongCaracterInsideTag(string html)
        {
            return html.Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        private string PurgeAfterTag(string html)
        {
            _startTag = CalculateStartTag(html);
            _endTag = CalculateEndTag();
            string htmlCleaned = OnlyAllTag(html);
            return htmlCleaned;
        }


        //TODO when I centralize the start and end tag I must replace this part
        private string CalculateStartTag(string html)
        {
            string htmlWorking = html;
            int startStartTag = html.IndexOf('<');
            int endStartTag = html.IndexOf('>');
            return htmlWorking.Substring(startStartTag, endStartTag + 1);
        }

        private string CalculateEndTag()
        {
            string tag = _startTag.Replace("<", string.Empty).Replace(">", string.Empty);
            string cleanTag = tag.Split(' ')[0];
            return string.Concat("</", cleanTag, ">");
        }

        private string OnlyAllTag(string html)
        {
            int index = html.IndexOf(_endTag);
            return index == -1 ? _startTag : html.Substring(0, index + _endTag.Length);
        }
    }
}