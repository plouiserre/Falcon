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
            string htmlResult = string.Empty;
            string htmlWorking = html;
            do
            {
                htmlResult = ManageHtmlTemplate(htmlWorking, jsonModel);
                htmlWorking = htmlResult;
            } while (IsTemplatingPresent(htmlWorking));

            return htmlResult;
        }

        private static string ManageHtmlTemplate(string html, JsonModel jsonModel)
        {
            if (!IsTemplatingPresent(html))
                return html;
            string templateTag = GetTemplateName(html);
            string template = templateTag.Replace("{", string.Empty).Replace("}", string.Empty);
            string valueHtml = GetValue(template, jsonModel);
            string result = html.Replace(templateTag, valueHtml);
            return result;
        }

        private static bool IsTemplatingPresent(string html)
        {
            if (html.Contains("{") && html.Contains("}"))
                return true;
            else
                return false;
        }

        private static string GetTemplateName(string html)
        {
            int firstPartTemplate = html.IndexOf('{');
            int secondPartTemplate = html.IndexOf('}');
            return html.Substring(firstPartTemplate, secondPartTemplate - firstPartTemplate + 1);
        }

        private static string? GetValue(string template, JsonModel jsonModel)
        {
            switch (template)
            {
                case "ALink":
                    return jsonModel.ALink;
                case "SpanA":
                    return jsonModel.SpanA;
                case "SpanRed":
                    return jsonModel.SpanRed;
                case "PDeclarationText":
                    return jsonModel.PDeclarationText;
                case "QuestionPHtml":
                    return jsonModel.QuestionPHtml;
                case "DivIdContent":
                    return jsonModel.DivIdContent;
                case "MetaCharset":
                    return jsonModel.MetaCharset;
                case "MetaViewPort":
                    return jsonModel.MetaViewPort;
                case "TitleDocument":
                    return jsonModel.TitleDocument;
                case "LinkHead":
                    return jsonModel.LinkHead;
                default:
                    return jsonModel.ALink;
            }
        }
    }
}