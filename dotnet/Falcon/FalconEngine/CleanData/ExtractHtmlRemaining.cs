using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;

namespace FalconEngine.CleanData
{
    public enum ExtractionMode
    {
        Inside, ASide
    }
    public class ExtractHtmlRemaining : IExtractHtmlRemaining
    {

        public ExtractHtmlRemaining()
        {
        }

        public string Extract(TagModel tag, string html, ExtractionMode extractionMode)
        {
            if (extractionMode == ExtractionMode.ASide)
            {
                string htmlTagModel = GetHtmlTagModel(tag);
                string htmlRemaning = html.Replace(htmlTagModel, string.Empty);
                htmlRemaning = DeleteFrontSpace(htmlRemaning);
                return htmlRemaning;
            }
            else
            {
                return tag.Content;
            }
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

        public string Extract(string html, NameTagEnum nameTag)
        {
            string htmlCleaned = string.Empty;

            if (nameTag == NameTagEnum.meta || nameTag == NameTagEnum.link)
                htmlCleaned = ExtractWithASideTag(html);
            else if (nameTag == NameTagEnum.title)
                htmlCleaned = ExtractWithInsideTag(html);

            return htmlCleaned;
        }

        private string ExtractWithASideTag(string html)
        {
            string htmlCleaned = string.Empty;
            bool mustBeErased = true;
            for (int i = 0; i < html.Length; i++)
            {
                char caracter = html[i];
                if (!mustBeErased)
                    htmlCleaned += caracter;
                if (caracter == '>')
                    mustBeErased = false;
            }
            return htmlCleaned;
        }

        private string ExtractWithInsideTag(string html)
        {
            string htmlCleaned = string.Empty;
            bool startSearchClosedTag = false;
            int endTagIndex = 0;
            for (int i = 0; i < html.Length; i++)
            {
                char caracter = html[i];
                if (caracter == '/')
                    startSearchClosedTag = true;
                if (startSearchClosedTag && caracter == '>')
                {
                    endTagIndex = i;
                    break;
                }
            }
            string uselessHtml = html.Substring(0, endTagIndex + 1);
            htmlCleaned = html.Replace(uselessHtml, string.Empty);
            return htmlCleaned;
        }
    }
}