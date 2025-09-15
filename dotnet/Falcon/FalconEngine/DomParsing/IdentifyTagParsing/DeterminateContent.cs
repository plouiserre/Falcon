using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public class DeterminateContent : IDeterminateContent
    {
        private ILocateEndTag _locateEndCaracter;

        public DeterminateContent(ILocateEndTag locateEndCaracter)
        {
            _locateEndCaracter = locateEndCaracter;
        }

        public string FindContent(string html, string tagStart, string tagEnd)
        {
            int startTagIndex = html.IndexOf(tagStart);
            if (!string.IsNullOrEmpty(tagEnd))
            {
                int? endTagIndex = _locateEndCaracter.Search(tagStart, html);//  html.IndexOf(tagEnd);
                string allTag = html.Substring(startTagIndex, endTagIndex.Value + tagEnd.Length - startTagIndex);
                string content = allTag.Replace(tagStart, string.Empty).Replace(tagEnd, string.Empty);
                return content;
            }
            else
            {
                return null;
            }
        }
    }
}