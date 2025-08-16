using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Configuration;
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

        private static string? GetDoctype(JsonModel json)
        {
            return json?.SimpleDoctype;
        }

        private static string? GetHtmlSimpleWithDoctypeNotValid(JsonModel json)
        {
            return TemplatingHtmlData.GetHtmlData(json.HtmlSimpleWithDoctypeNotValid, json);
        }

        private static string? GetHtmlSimpleWithDoctype(JsonModel json)
        {
            return TemplatingHtmlData.GetHtmlData(json.HtmlSimpleWithDoctype, json);
        }

        private static string? GetHtmlSimple(JsonModel json)
        {
            return TemplatingHtmlData.GetHtmlData(json.HtmlSimple, json);
        }

        private static string? GetHead(JsonModel json)
        {
            return TemplatingHtmlData.GetHtmlData(json.Head, json);
        }

        private static string? GetMetaCharset(JsonModel json)
        {
            return json?.MetaCharset;
        }

        private static string? GetMetaViewPort(JsonModel json)
        {
            return json?.MetaViewPort;
        }

        private static string? GetTitleDocument(JsonModel json)
        {
            return json?.TitleDocument;
        }

        private static string? GetLinkHead(JsonModel json)
        {
            return json?.LinkHead;
        }

        private static string? GetBody(JsonModel json)
        {
            return TemplatingHtmlData.GetHtmlData(json.BodySimple, json);
        }

        private static string? GetDivIdContent(JsonModel json)
        {
            return TemplatingHtmlData.GetHtmlData(json.DivIdContent, json);
        }

        private static string? GetPSimple(JsonModel json)
        {
            return TemplatingHtmlData.GetHtmlData(json.PSimple, json);
        }

        private static string? GetQuestionPHtml(JsonModel json)
        {
            return json?.QuestionPHtml;
        }

        private static string? GetPDeclarationText(JsonModel json)
        {
            return TemplatingHtmlData.GetHtmlData(json.PDeclarationText, json);
        }

        private static string? GetSpanA(JsonModel json)
        {
            return TemplatingHtmlData.GetHtmlData(json.SpanA, json);
        }

        private static string? GetSpanInputRed(JsonModel json)
        {
            return json.SpanInputRed;
        }

        private static string? GetSpanRed(JsonModel json)
        {
            return json.SpanRed;
        }

        private static string? GetALink(JsonModel json)
        {
            return json.ALink;
        }

        private static JsonModel? GetData()
        {
            string dataJson = HtmlDataJson.AllDataJson;
            JsonModel? data = JsonConvert.DeserializeObject<JsonModel>(dataJson);
            return data;
        }
    }
}