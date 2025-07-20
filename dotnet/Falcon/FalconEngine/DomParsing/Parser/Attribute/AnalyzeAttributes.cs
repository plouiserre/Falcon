using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.Parser.Attribute
{
    public class AnalyzeAttributes : IAnalyzeAttributes
    {
        private string _startTag;
        private string _baseTag;
        private string _onlyAttributes;

        public List<string> Study(string startTag)
        {
            var results = new List<string>();
            _startTag = startTag;

            _baseTag = GetTagName();
            _onlyAttributes = GetOnlyAttributes();

            results = SeparatesAttributes();
            return results;
        }

        private string GetTagName()
        {
            string startTagWorking = _startTag.Split(" ")[0];
            startTagWorking = startTagWorking.Trim();
            return startTagWorking;
        }

        private string GetOnlyAttributes()
        {
            string onlyAttributes = _startTag.Replace(_baseTag, string.Empty);
            onlyAttributes = onlyAttributes.Replace(">", string.Empty);
            onlyAttributes = onlyAttributes.TrimStart();
            return onlyAttributes;
        }

        private List<string> SeparatesAttributes()
        {
            var parts = new List<string>();
            var partsNotClean = GetAllParts();
            parts = CleanAllParts(partsNotClean);
            return parts;
        }

        private List<string> GetAllParts()
        {
            List<string> parts = new List<string>();
            string recording = string.Empty;
            int countDoubleQuote = 0;
            for (int i = 0; i < _onlyAttributes.Length; i++)
            {
                char caracter = _onlyAttributes[i];
                recording += caracter;
                if (caracter == '"')
                {
                    countDoubleQuote += 1;
                }
                if (countDoubleQuote == 2)
                {
                    parts.Add(recording);
                    recording = string.Empty;
                    countDoubleQuote = 0;
                }
            }
            return parts;
        }

        private List<string> CleanAllParts(List<string> partsNotClean)
        {
            var parts = new List<string>();
            foreach (var part in partsNotClean)
            {
                var partClean = part.TrimStart();
                partClean = partClean.TrimEnd();
                bool isSpace = CheckElementAlone(partClean);
                if (isSpace)
                {
                    string elementAlone = partClean.Split(" ")[0];
                    string restElements = partClean.Replace(elementAlone, string.Empty);
                    restElements = restElements.TrimEnd().TrimStart();
                    parts.Add(elementAlone);
                    parts.Add(restElements);
                }
                else
                {
                    parts.Add(partClean);
                }
            }
            return parts;
        }

        private bool CheckElementAlone(string html)
        {
            bool isSpaceBeforeEqual = false;
            for (int i = 0; i < html.Length; i++)
            {
                var caracter = html[i];
                if (caracter == ' ')
                    isSpaceBeforeEqual = true;
                if (caracter == '=')
                    break;
            }
            return isSpaceBeforeEqual;
        }
    }
}