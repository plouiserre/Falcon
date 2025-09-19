using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public enum LimitMode
    {
        Start,
        End
    }
    public class LocateLimitTag : ILocateLimitTag
    {
        private string _html;
        private string _startTag;
        private string _baseStartTag;
        private int _baseStartTagCount;
        private bool isMultipleStartTag;

        public int? Search(LimitMode mode, string startTag, string html)
        {
            int result = 0;
            _startTag = startTag;
            _html = html;
           if(mode == LimitMode.End)
            {
                result = LocateEndTag(startTag, html);
            }
            else
            {
                result = LocateStartTag(html);
            }
                return result != 0 ? result : null;
        }

        private int LocateEndTag(string startTag, string html)
        {
            int result = 0;
            string endTag = GetEndTag();
            GetBaseStartTag();
            _baseStartTagCount = 0;
            for (int i = 0; i < _html.Length; i++)
            {
                if (i + endTag.Length > _html.Length)
                    break;
                DeterminateIfItIsAStartTagCandidate(i);

                string endTagCandidate = _html.Substring(i, endTag.Length);
                if (endTagCandidate == endTag && _baseStartTagCount == 1)
                {
                    result = i;
                    break;
                }
                else if (endTagCandidate == endTag)
                {
                    _baseStartTagCount--;
                }
                else
                {
                    continue;
                }
            }
            return result;
        }

        private void DeterminateIfItIsAStartTagCandidate(int index)
        {
            string startTagCandidate = _html.Substring(index, _baseStartTag.Length);
            if (startTagCandidate == _baseStartTag) { 
                _baseStartTagCount += 1;
                if(_baseStartTagCount > 1)
                    isMultipleStartTag = true;
            }
        }

        public bool IndicateIsMultipleStartTag()
        {
            return isMultipleStartTag;
        }

        private int LocateStartTag(string html)
        {
            int Localisation = 0;
            bool IsOpenBracketPresent = false;
            for (int i = 0; i < _html.Length; i++)
            {
                char caracter = _html[i];
                if (caracter !=' ' && caracter != '\n' && caracter != '\r')
                {
                    Localisation = i;
                    IsOpenBracketPresent = true;
                    break;
                }
            }
            if (!IsOpenBracketPresent)
                Localisation = _html.Length;
            return Localisation;
        }

        private void GetBaseStartTag()
        {
            if (_startTag.Contains(" "))
            {
                _baseStartTag = _startTag.Split(" ")[0];
            }
            else
            {
                _baseStartTag =  _startTag.Replace(">", string.Empty);
            }
        }

        private string GetEndTag()
        {
            if (_startTag.Contains(" "))
            {
                string baseStartTag = _startTag.Split(" ")[0];
                baseStartTag = baseStartTag.Replace("<", "</");
                string endTag = string.Concat(baseStartTag, ">");
                return endTag;
            }
            else
            {
                return _startTag.Replace("<", "</");
            }
        }
    }
}
