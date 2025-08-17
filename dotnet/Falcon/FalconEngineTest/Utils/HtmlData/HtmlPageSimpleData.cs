using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Configuration;
using FalconEngineTest.Utils.Templating;
using Newtonsoft.Json;

namespace FalconEngineTest.Utils.HtmlData
{
    public enum TagHtmlSimple
    {
        doctype, htmlNotValid, htmlPageWithDoctype, htmlPage, head, metaCharset, metaviewPort, title, link, body, divIdContent, pSimple, pQuestion, pDeclarationText,
        spanA, spanInputRed, spanRed, aLink
    }

    public static class HtmlPageSimpleData
    {
        public static string? GetHtml(TagHtmlSimple tag)
        {
            var json = GetData();
            switch (tag)
            {
                case TagHtmlSimple.doctype:
                    return GetDoctype(json);
                case TagHtmlSimple.htmlNotValid:
                    return GetHtmlSimpleWithDoctypeNotValid(json);
                case TagHtmlSimple.htmlPageWithDoctype:
                    return GetHtmlSimpleWithDoctype(json);
                case TagHtmlSimple.htmlPage:
                    return GetHtmlSimple(json);
                case TagHtmlSimple.head:
                    return GetHead(json);
                case TagHtmlSimple.metaCharset:
                    return GetMetaCharset(json);
                case TagHtmlSimple.metaviewPort:
                    return GetMetaViewPort(json);
                case TagHtmlSimple.title:
                    return GetTitleDocument(json);
                case TagHtmlSimple.link:
                    return GetLinkHead(json);
                case TagHtmlSimple.body:
                    return GetBody(json);
                case TagHtmlSimple.divIdContent:
                    return GetDivIdContent(json);
                case TagHtmlSimple.pSimple:
                    return GetPSimple(json);
                case TagHtmlSimple.pQuestion:
                    return GetQuestionPHtml(json);
                case TagHtmlSimple.pDeclarationText:
                    return GetPDeclarationText(json);
                case TagHtmlSimple.spanA:
                    return GetSpanA(json);
                case TagHtmlSimple.spanInputRed:
                    return GetSpanInputRed(json);
                case TagHtmlSimple.spanRed:
                    return GetSpanRed(json);
                case TagHtmlSimple.aLink:
                    return GetALink(json);
                default:
                    throw new Exception("Unknown Tag");
            }
        }

        private static string? GetDoctype(JsonSimpleDataModel json)
        {
            return json?.SimpleDoctype;
        }

        private static string? GetHtmlSimpleWithDoctypeNotValid(JsonSimpleDataModel json)
        {
            return TemplatingHtmlSimpleData.GetHtmlSimpleData(json.HtmlSimpleWithDoctypeNotValid, json);
        }

        private static string? GetHtmlSimpleWithDoctype(JsonSimpleDataModel json)
        {
            return TemplatingHtmlSimpleData.GetHtmlSimpleData(json.HtmlSimpleWithDoctype, json);
        }

        private static string? GetHtmlSimple(JsonSimpleDataModel json)
        {
            return TemplatingHtmlSimpleData.GetHtmlSimpleData(json.HtmlSimple, json);
        }

        private static string? GetHead(JsonSimpleDataModel json)
        {
            return TemplatingHtmlSimpleData.GetHtmlSimpleData(json.Head, json);
        }

        private static string? GetMetaCharset(JsonSimpleDataModel json)
        {
            return json?.MetaCharset;
        }

        private static string? GetMetaViewPort(JsonSimpleDataModel json)
        {
            return json?.MetaViewPort;
        }

        private static string? GetTitleDocument(JsonSimpleDataModel json)
        {
            return json?.TitleDocument;
        }

        private static string? GetLinkHead(JsonSimpleDataModel json)
        {
            return json?.LinkHead;
        }

        private static string? GetBody(JsonSimpleDataModel json)
        {
            return TemplatingHtmlSimpleData.GetHtmlSimpleData(json.BodySimple, json);
        }

        private static string? GetDivIdContent(JsonSimpleDataModel json)
        {
            return TemplatingHtmlSimpleData.GetHtmlSimpleData(json.DivIdContent, json);
        }

        private static string? GetPSimple(JsonSimpleDataModel json)
        {
            return TemplatingHtmlSimpleData.GetHtmlSimpleData(json.PSimple, json);
        }

        private static string? GetQuestionPHtml(JsonSimpleDataModel json)
        {
            return json?.QuestionPHtml;
        }

        private static string? GetPDeclarationText(JsonSimpleDataModel json)
        {
            return TemplatingHtmlSimpleData.GetHtmlSimpleData(json.PDeclarationText, json);
        }

        private static string? GetSpanA(JsonSimpleDataModel json)
        {
            return TemplatingHtmlSimpleData.GetHtmlSimpleData(json.SpanA, json);
        }

        private static string? GetSpanInputRed(JsonSimpleDataModel json)
        {
            return json.SpanInputRed;
        }

        private static string? GetSpanRed(JsonSimpleDataModel json)
        {
            return json.SpanRed;
        }

        private static string? GetALink(JsonSimpleDataModel json)
        {
            return json.ALink;
        }

        private static JsonSimpleDataModel? GetData()
        {
            string dataJson = HtmlSimpleDataJson.AllDataJson;
            JsonSimpleDataModel? data = JsonConvert.DeserializeObject<JsonSimpleDataModel>(dataJson);
            return data;
        }
    }
}