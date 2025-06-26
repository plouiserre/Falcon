using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.CleanData
{
    public class ExtractHtmlRemaining : IExtractHtmlRemaining
    {

        public ExtractHtmlRemaining()
        {
        }

        public string Extract(TagModel tag, string html)
        {
            string htmlTagModel = GetHtmlTagModel(tag);
            string htmlRemaning = html.Replace(htmlTagModel, string.Empty);
            htmlRemaning = DeleteFrontSpace(htmlRemaning);
            return htmlRemaning;
        }

        //If we need this somewhere else, move to TagModel
        private string GetHtmlTagModel(TagModel tag)
        {
            if (tag.TagFamily == TagFamilyEnum.WithEnd)
                return string.Concat(tag.TagStart, tag.Content, tag.TagEnd);
            else
                return tag.TagStart;
        }

        private string DeleteFrontSpace(string content)
        {
            int startHtml = 0;
            for (int i = 0; i < content.Length; i++)
            {
                char caracter = content[i];
                if (caracter == '<')
                {
                    startHtml = i;
                    break;
                }
            }
            string textCleaned = content.Substring(startHtml, content.Length - startHtml);
            return textCleaned;
        }

    }
}