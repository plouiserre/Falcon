using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Configuration;

namespace FalconEngineTest.Utils
{
    public class TemplatingHtmlData
    {
        public static string GetHtmlData(string html, JsonModel jsonModel)
        {
            if (!(html.Contains("{") && html.Contains("}")))
                return html;
            string templateTag = GetTemplateName(html);
            string template = templateTag.Replace("{", string.Empty).Replace("}", string.Empty);
            string valueHtml = GetValue(template, jsonModel);
            string result = html.Replace(templateTag, valueHtml);
            return result;
        }

        private static string GetTemplateName(string html)
        {
            int firstPartTemplate = html.IndexOf('{');
            int secondPartTemplate = html.IndexOf('}');
            return html.Substring(firstPartTemplate, secondPartTemplate - firstPartTemplate + 1);
        }

        private static string GetValue(string template, JsonModel jsonModel)
        {
            switch (template)
            {
                case "ALink":
                default:
                    return jsonModel.ALink;
            }
        }
    }
}