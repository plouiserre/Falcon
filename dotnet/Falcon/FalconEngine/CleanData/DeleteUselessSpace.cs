using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.CleanData
{
    public class DeleteUselessSpace : IDeleteUselessSpace
    {
        private string _startTag;
        private string _startTagCleaned;
        private string _endTag;
        private IIdentifyStartTagEndTag _identifyStartTagEndTag;

        public DeleteUselessSpace(IIdentifyStartTagEndTag identifyStartTagEndTag)
        {
            _identifyStartTagEndTag = identifyStartTagEndTag;
        }

        public string PurgeUselessCaractersAroundTag(string html)
        {
            string htmlPurgeBeforeTag = PurgeBeforeTag(html);
            string htmlPurgeWrongCaracter = PurgeWrongCaracterInsideTag(htmlPurgeBeforeTag);
            string htmlPurge = PurgeAfterTag(htmlPurgeWrongCaracter);
            if (!_startTag.ToLower().Contains("doctype"))
            {
                _startTagCleaned = CleanStartTag();
                htmlPurge = htmlPurge.Replace(_startTag, _startTagCleaned);
            }
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
            _identifyStartTagEndTag.DetermineStartEndTags(html);
            _startTag = _identifyStartTagEndTag.StartTag;
            _endTag = _identifyStartTagEndTag.EndTag != null ? _identifyStartTagEndTag.EndTag : string.Empty;
            string htmlCleaned = OnlyAllTag(html);
            return htmlCleaned;
        }

        private string OnlyAllTag(string html)
        {
            if (!string.IsNullOrEmpty(_endTag))
            {
                int index = html.IndexOf(_endTag);
                return html.Substring(0, index + _endTag.Length);
            }
            return _startTag;
        }

        private string CleanStartTag()
        {
            string cleanContent = string.Empty;
            bool isBracketOpen = false;
            int numberSpace = 0;
            for (int i = 0; i < _startTag.Length; i++)
            {
                char caracter = _startTag[i];
                if (IsOpenBracket(caracter, isBracketOpen))
                    isBracketOpen = true;
                else if (IsClosedBracket(caracter, isBracketOpen))
                {
                    isBracketOpen = false;
                    numberSpace = 0;
                }
                if (IsASpaceCanBeRemoved(caracter, isBracketOpen, numberSpace))
                    continue;
                else if (IsAFirstSpace(caracter, isBracketOpen))
                {
                    numberSpace += 1;
                    cleanContent += caracter;
                }
                else
                {
                    numberSpace = 0;
                    cleanContent += caracter;
                }
            }
            return cleanContent;
        }

        private bool IsOpenBracket(char caracter, bool isBracketOpen)
        {
            return caracter == '"' && isBracketOpen == false;
        }

        private bool IsClosedBracket(char caracter, bool isBracketOpen)
        {
            return caracter == '"' && isBracketOpen == true;
        }

        private bool IsASpaceCanBeRemoved(char caracter, bool isBracketOpen, int numberSpace)
        {
            return caracter == ' ' && !isBracketOpen && numberSpace > 0;
        }

        private bool IsAFirstSpace(char caracter, bool isBracketOpen)
        {
            return caracter == ' ' && !isBracketOpen;
        }
    }
}