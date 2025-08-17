using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Configuration;

namespace FalconEngineTest.Utils.Templating
{
    public class TemplatingHtmlSimpleData
    {
        public static string GetHtmlSimpleData(string html, JsonSimpleDataModel jsonModel)
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

        private static string ManageHtmlTemplate(string html, JsonSimpleDataModel jsonModel)
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

        private static string? GetValue(string template, JsonSimpleDataModel jsonModel)
        {
            switch (template)
            {
                case "ALink":
                    return jsonModel.ALink;
                case "SpanA":
                    return jsonModel.SpanA;
                case "SpanRed":
                    return jsonModel.SpanRed;
                case "SpanInputRed":
                    return jsonModel.SpanInputRed;
                case "PDeclarationText":
                    return jsonModel.PDeclarationText;
                case "PDeclarationTextNotValid":
                    return jsonModel.PDeclarationTextNotValid;
                case "QuestionPHtml":
                    return jsonModel.QuestionPHtml;
                case "DivIdContent":
                    return jsonModel.DivIdContent;
                case "DivIdContentNotValid":
                    return jsonModel.DivIdContentNotValid;
                case "BodySimple":
                    return jsonModel.BodySimple;
                case "BodySimpleNotValid":
                    return jsonModel.BodySimpleNotValid;
                case "MetaCharset":
                    return jsonModel.MetaCharset;
                case "MetaViewPort":
                    return jsonModel.MetaViewPort;
                case "TitleDocument":
                    return jsonModel.TitleDocument;
                case "LinkHead":
                    return jsonModel.LinkHead;
                case "Head":
                    return jsonModel.Head;
                case "HtmlSimple":
                    return jsonModel.HtmlSimple;
                case "HtmlSimpleNotValid":
                    return jsonModel.HtmlSimpleNotValid;
                default:
                    return jsonModel.ALink;
            }
        }
    }
}