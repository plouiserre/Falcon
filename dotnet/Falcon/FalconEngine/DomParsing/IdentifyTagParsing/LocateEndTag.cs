using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public class LocateEndTag : ILocateEndTag
    {
        private string _html;
        private string _startTag;

        public int? Search(string startTag, string html)
        {
            int result = 0;
            _startTag = startTag;
            _html = html;
            string endTag = GetEndTag();
            string baseStartTag = GetBaseStartTag();
            int baseStartTagCount = CountBaseStartTag(baseStartTag);
            for(int i = 0; i < _html.Length; i++)
            {
                if(i + endTag.Length > _html.Length)                
                    break;
                string endTagCandidate = _html.Substring(i, endTag.Length);
                if(endTagCandidate == endTag && baseStartTagCount == 1)
                 {
                    result = i;
                    break;
                }
                else if(endTagCandidate == endTag) { 
                    baseStartTagCount--;
                }
                else
                {
                    continue;
                }
            }
            return result != 0 ? result : null;
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
