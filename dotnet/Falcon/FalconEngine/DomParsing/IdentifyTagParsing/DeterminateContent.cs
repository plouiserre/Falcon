using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public class DeterminateContent : IDeterminateContent
    {
        public string FindContent(string html, string tagStart, string tagEnd)
        {
            int startTagIndex = html.IndexOf(tagStart);
            if (!string.IsNullOrEmpty(tagEnd))
            {
                int endTagIndex = html.IndexOf(tagEnd);
                string allTag = html.Substring(startTagIndex, endTagIndex + tagEnd.Length - startTagIndex);
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