using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Configuration;
using FalconEngineTest.Utils.Templating;
using Newtonsoft.Json;

namespace FalconEngineTest.Utils.HtmlData
{
    public enum TagHtmlForm
    {
        body, divDate, divGender, divH1, divIdentity, divResume, divSituation, divSubmit, form, h1Title, head,
        htmlForm, htmlFormWithDoctype, inputDate, inputFile, inputFirstName, inputLastName, inputSubmit, labelDate,
        labelFemale, labelGender, labelMale, labelResume, labelSituation, labelUndefined, linkHead, metaCharset,
        metaViewPort, radioFemale, radioMale, radioUndefined, selectSituation, titleDocument
    }

    public class HtmlPageFormData
    {
        public static string? GetHtml(TagHtmlForm tag)
        {
            var json = GetData();
            switch (tag)
            {
                case TagHtmlForm.body:
                    return GetBody(json);
                case TagHtmlForm.divDate:
                    return GetDivDate(json);
                case TagHtmlForm.divGender:
                    return GetDivGender(json);
                case TagHtmlForm.divH1:
                    return GetDivH1(json);
                case TagHtmlForm.divIdentity:
                    return GetDivIdentity(json);
                case TagHtmlForm.divResume:
                    return GetDivResume(json);
                case TagHtmlForm.divSituation:
                    return GetDivSituation(json);
                case TagHtmlForm.divSubmit:
                    return GetDivSubmit(json);
                case TagHtmlForm.form:
                    return GetForm(json);
                case TagHtmlForm.h1Title:
                    return GetH1Title(json);
                case TagHtmlForm.head:
                    return GetHead(json);
                case TagHtmlForm.htmlForm:
                    return GetHtmlForm(json);
                case TagHtmlForm.htmlFormWithDoctype:
                    return GetHtmlFormWithDoctype(json);
                case TagHtmlForm.inputDate:
                    return GetInputDate(json);
                case TagHtmlForm.inputFile:
                    return GetInputFile(json);
                case TagHtmlForm.inputFirstName:
                    return GetInputFirstName(json);
                case TagHtmlForm.inputLastName:
                    return GetInputLastName(json);
                case TagHtmlForm.inputSubmit:
                    return GetInputSubmit(json);
                case TagHtmlForm.labelDate:
                    return GetLabelDate(json);
                case TagHtmlForm.labelFemale:
                    return GetLabelFemale(json);
                case TagHtmlForm.labelGender:
                    return GetLabelGender(json);
                case TagHtmlForm.labelMale:
                    return GetLabelMale(json);
                case TagHtmlForm.labelResume:
                    return GetLabelResume(json);
                case TagHtmlForm.labelSituation:
                    return GetLabelSituation(json);
                case TagHtmlForm.labelUndefined:
                    return GetLabelUndefined(json);
                case TagHtmlForm.linkHead:
                    return GetLinkHead(json);
                case TagHtmlForm.metaCharset:
                    return GetMetaCharset(json);
                case TagHtmlForm.metaViewPort:
                    return GetMetaViewPort(json);
                case TagHtmlForm.radioFemale:
                    return GetRadioFemale(json);
                case TagHtmlForm.radioMale:
                    return GetRadioMale(json);
                case TagHtmlForm.radioUndefined:
                    return GetRadioUndefined(json);
                case TagHtmlForm.selectSituation:
                    return GetSelectionSituation(json);
                case TagHtmlForm.titleDocument:
                    return GetTitleDocument(json);
                default:
                    throw new Exception("Unknown Tag");
            }
        }
        private static string? GetInputSubmit(JsonFormDataModel? json)
        {
            return json?.InputSubmit;
        }

        private static string? GetDivSubmit(JsonFormDataModel? json)
        {
            return TemplatingHtmlFormData.GetHtmlFormData(json.DivSubmit, json);
        }

        private static string? GetInputFile(JsonFormDataModel? json)
        {
            return json?.InputFile;
        }

        private static string? GetLabelResume(JsonFormDataModel? json)
        {
            return json?.LabelResume;
        }

        private static string GetDivResume(JsonFormDataModel? json)
        {
            return TemplatingHtmlFormData.GetHtmlFormData(json.DivResume, json);
        }

        private static string? GetInputDate(JsonFormDataModel? json)
        {
            return json?.InputDate;
        }

        private static string? GetLabelDate(JsonFormDataModel? json)
        {
            return json?.LabelDate;
        }

        private static string GetDivDate(JsonFormDataModel? json)
        {
            return TemplatingHtmlFormData.GetHtmlFormData(json.DivDate, json);
        }

        private static string? GetRadioMale(JsonFormDataModel? json)
        {
            return json?.RadioMale;
        }

        private static string? GetLabelMale(JsonFormDataModel? json)
        {
            return json?.LabelMale;
        }

        private static string? GetRadioFemale(JsonFormDataModel? json)
        {
            return json?.RadioFemale;
        }

        private static string? GetLabelFemale(JsonFormDataModel? json)
        {
            return json?.LabelFemale;
        }

        private static string? GetLabelGender(JsonFormDataModel? json)
        {
            return json?.LabelGender;
        }

        private static string? GetRadioUndefined(JsonFormDataModel? json)
        {
            return json?.RadioUndefined;
        }

        private static string? GetLabelUndefined(JsonFormDataModel? json)
        {
            return json?.LabelUndefined;
        }

        private static string GetDivGender(JsonFormDataModel? json)
        {
            return TemplatingHtmlFormData.GetHtmlFormData(json.DivGender, json);
        }

        private static string? GetLabelSituation(JsonFormDataModel? json)
        {
            return json?.LabelSituation;
        }

        private static string? GetSelectionSituation(JsonFormDataModel? json)
        {
            return json?.SelectSituation;
        }

        private static string? GetDivSituation(JsonFormDataModel? json)
        {
            return TemplatingHtmlFormData.GetHtmlFormData(json?.DivSituation, json);
        }

        private static string? GetInputFirstName(JsonFormDataModel? json)
        {
            return json?.InputFirstName;
        }

        private static string? GetInputLastName(JsonFormDataModel? json)
        {
            return json?.InputLastName;
        }

        private static string GetDivIdentity(JsonFormDataModel? json)
        {
            return TemplatingHtmlFormData.GetHtmlFormData(json.DivIdentity, json);
        }

        private static string? GetH1Title(JsonFormDataModel? json)
        {
            return json?.H1Title;
        }

        private static string GetDivH1(JsonFormDataModel? json)
        {
            return TemplatingHtmlFormData.GetHtmlFormData(json.DivH1, json);
        }

        private static string GetForm(JsonFormDataModel? json)
        {
            return TemplatingHtmlFormData.GetHtmlFormData(json.Form, json);
        }

        private static string GetBody(JsonFormDataModel? json)
        {
            return TemplatingHtmlFormData.GetHtmlFormData(json.Body, json);
        }

        private static string? GetMetaCharset(JsonFormDataModel? json)
        {
            return json?.MetaCharset;
        }

        private static string? GetMetaViewPort(JsonFormDataModel? json)
        {
            return json?.MetaViewPort;
        }

        private static string? GetTitleDocument(JsonFormDataModel? json)
        {
            return json?.TitleDocument;
        }

        private static string? GetLinkHead(JsonFormDataModel? json)
        {
            return json?.LinkHead;
        }

        private static string? GetHead(JsonFormDataModel? json)
        {
            return TemplatingHtmlFormData.GetHtmlFormData(json.Head, json);
        }

        private static string? GetHtmlForm(JsonFormDataModel? json)
        {
            return TemplatingHtmlFormData.GetHtmlFormData(json.HtmlForm, json);
        }

        private static string? GetHtmlFormWithDoctype(JsonFormDataModel? json)
        {
            return TemplatingHtmlFormData.GetHtmlFormData(json.HtmlFormWithDoctype, json);
        }

        private static JsonFormDataModel? GetData()
        {
            string dataJson = HtmlFormDataJson.AllDataJson;
            JsonFormDataModel? data = JsonConvert.DeserializeObject<JsonFormDataModel>(dataJson);
            return data;
        }
    }
}