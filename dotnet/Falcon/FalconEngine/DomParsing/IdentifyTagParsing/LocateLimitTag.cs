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

        //Todo rework this method with two submethods
        private int LocateEndTag(string startTag, string html)
        {
            int result = 0;
            string endTag = GetEndTag();
            string baseStartTag = GetBaseStartTag();
            //int baseStartTagCount = CountBaseStartTag(baseStartTag);
            int baseStartTagCount = 0;
            for (int i = 0; i < _html.Length; i++)
            {
                if (i + endTag.Length > _html.Length)
                    break;
                string startTagCandidate = _html.Substring(i, baseStartTag.Length);
                if (startTagCandidate == baseStartTag)                
                    baseStartTagCount += 1;
                
                string endTagCandidate = _html.Substring(i, endTag.Length);
                if (endTagCandidate == endTag && baseStartTagCount == 1)
                {
                    result = i;
                    break;
                }
                else if (endTagCandidate == endTag)
                {
                    baseStartTagCount--;
                }
                else
                {
                    continue;
                }
            }
            return result;
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

        private string GetBaseStartTag()
        {
            if (_startTag.Contains(" "))
            {
                string baseStartTag = _startTag.Split(" ")[0];
                return baseStartTag;
            }
            else
            {
                return _startTag.Replace(">", string.Empty);
            }
        }

        private int CountBaseStartTag(string baseStartTag)
        {
            int count = 0;
            for(int i = 0; i < _html.Length; i++)
            {
                if(i + baseStartTag.Length > _html.Length)                
                    break;
                
                string startTagCandidate = _html.Substring(i, baseStartTag.Length);
                if(startTagCandidate == baseStartTag)
                {
                    count++;
                }
            }
            return count;
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
