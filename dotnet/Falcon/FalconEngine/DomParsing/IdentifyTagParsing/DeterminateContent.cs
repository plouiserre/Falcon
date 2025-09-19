using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public class DeterminateContent : IDeterminateContent
    {
        private ILocateLimitTag _locateEndCaracter;
        private string _html;
        private string _startTag;
        private string _endTag;
        private int _startTagIndex;
        private int? _endTagIndex;

        public DeterminateContent(ILocateLimitTag locateEndCaracter)
        {
            _locateEndCaracter = locateEndCaracter;
        }

        public string FindContent(string html, string tagStart, string tagEnd)
        {
            _startTagIndex = html.IndexOf(tagStart);
            _startTag = tagStart;
            _endTag = tagEnd;
            _html = html;
            if (!string.IsNullOrEmpty(tagEnd))
            {
                _endTagIndex = _locateEndCaracter.Search(LimitMode.End, tagStart, html);
                bool isMultipleStartTag = _locateEndCaracter.IndicateIsMultipleStartTag();
                if (isMultipleStartTag)
                {
                    return FindMultipleContentExtract();
                }
                else
                {
                    return FindContentSimpleExtract();
                }
            }
            else
            {
                return null;
            }
        }

        private string FindMultipleContentExtract()
        {

            string allTag = _html.Substring(_startTagIndex, _endTagIndex.Value - _startTagIndex);
            string content = allTag.Replace(_startTag, string.Empty);
            return content;
        }

        private string FindContentSimpleExtract()
        {
            string allTag = _html.Substring(_startTagIndex, _endTagIndex.Value - _startTagIndex);
            string content = allTag.Replace(_startTag, string.Empty).Replace(_endTag, string.Empty);
            return content;
        }
    }
}