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
            onlyAttributes = onlyAttributes.Replace("/>", string.Empty);
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
            bool isFirstBracketFind = false;
            for (int i = 0; i < _onlyAttributes.Length; i++)
            {
                char caracter = _onlyAttributes[i];
                if (IsFirstBracketBeforeValue(caracter, isFirstBracketFind))
                {
                    isFirstBracketFind = true;
                    recording += caracter;
                }
                else if (IsSecondBracketAfterValue(caracter, isFirstBracketFind))
                {
                    isFirstBracketFind = false;
                    recording += caracter;
                    parts.Add(recording);
                    recording = string.Empty;
                }
                else if (IsNotSpace(caracter))
                {
                    recording += caracter;
                }
                else if (IsOnlyAttributeFinishSaving(caracter, recording, isFirstBracketFind))
                {
                    parts.Add(recording);
                    recording = string.Empty;
                }
                else if (IsSpaceBetweenValue(caracter, isFirstBracketFind))
                {
                    recording += caracter;
                }
            }
            if (!string.IsNullOrEmpty(recording))
                parts.Add(recording);
            return parts;
        }

        private bool IsFirstBracketBeforeValue(char caracter, bool isFirstBracketFind)
        {
            return caracter == '\"' && !isFirstBracketFind;
        }

        private bool IsSecondBracketAfterValue(char caracter, bool isFirstBracketFind)
        {
            return caracter == '\"' && isFirstBracketFind;
        }

        private bool IsNotSpace(char caracter)
        {
            return caracter != ' ';
        }

        private bool IsOnlyAttributeFinishSaving(char caracter, string recording, bool isFirstBracketFind)
        {
            return caracter == ' ' && !string.IsNullOrEmpty(recording) && !isFirstBracketFind;
        }

        private bool IsSpaceBetweenValue(char caracter, bool isFirstBracketFind)
        {
            return caracter == ' ' && isFirstBracketFind;
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