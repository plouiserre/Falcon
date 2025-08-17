using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Configuration;

namespace FalconEngineTest.Utils.Templating
{
    public class TemplatingHtmlFormData
    {
        public static string GetHtmlFormData(string html, JsonFormDataModel jsonModel)
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

        private static string ManageHtmlTemplate(string html, JsonFormDataModel jsonModel)
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

        private static string? GetValue(string template, JsonFormDataModel jsonModel)
        {
            switch (template)
            {
                case "InputSubmit":
                    return jsonModel.InputSubmit;
                case "DivSubmit":
                    return jsonModel.DivSubmit;
                case "InputFile":
                    return jsonModel.InputFile;
                case "LabelResume":
                    return jsonModel.LabelResume;
                case "DivResume":
                    return jsonModel.DivResume;
                case "InputDate":
                    return jsonModel.InputDate;
                case "LabelDate":
                    return jsonModel.LabelDate;
                case "DivDate":
                    return jsonModel.DivDate;
                case "RadioMale":
                    return jsonModel.RadioMale;
                case "LabelMale":
                    return jsonModel.LabelMale;
                case "RadioFemale":
                    return jsonModel.RadioFemale;
                case "LabelFemale":
                    return jsonModel.LabelFemale;
                case "RadioUndefined":
                    return jsonModel.RadioUndefined;
                case "LabelUndefined":
                    return jsonModel.LabelUndefined;
                case "DivGender":
                    return jsonModel.DivGender;
                case "InputFirstName":
                    return jsonModel.InputFirstName;
                case "InputLastName":
                    return jsonModel.InputLastName;
                case "DivIdentity":
                    return jsonModel.DivIdentity;
                case "H1Title":
                    return jsonModel.H1Title;
                case "DivH1":
                    return jsonModel.DivH1;
                case "Form":
                    return jsonModel.Form;
                case "Body":
                    return jsonModel.Body;
                    return jsonModel.DivIdentity;
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
                case "HtmlForm":
                    return jsonModel.HtmlForm;
                case "HtmlFormWithDoctype":
                    return jsonModel.HtmlFormWithDoctype;
                default:
                    return jsonModel.InputSubmit;
            }
        }

    }
}