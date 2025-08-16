using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Configuration;
using Newtonsoft.Json;

namespace FalconEngineTest.Utils
{
    public static class HtmlData
    {
        public static string? GetSimpleDoctype()
        {
            JsonModel? json = GetData();
            return json?.SimpleDoctype;
        }

        public static string? GetHtmlSimpleWithDoctypeNotValid()
        {
            JsonModel? json = GetData();
            return TemplatingHtmlData.GetHtmlData(json.HtmlSimpleWithDoctypeNotValid, json);
        }

        public static string? GetHtmlSimpleWithDoctype()
        {
            JsonModel? json = GetData();
            return TemplatingHtmlData.GetHtmlData(json.HtmlSimpleWithDoctype, json);
        }

        public static string? GetHtmlSimple()
        {
            JsonModel? json = GetData();
            return TemplatingHtmlData.GetHtmlData(json.HtmlSimple, json);
        }

        public static string? GetHtmlSimpleNotValid()
        {
            JsonModel? json = GetData();
            return TemplatingHtmlData.GetHtmlData(json.HtmlSimpleNotValid, json);
        }

        public static string? GetHead()
        {
            JsonModel? json = GetData();
            return TemplatingHtmlData.GetHtmlData(json.Head, json);
        }

        public static string? GetMetaCharset()
        {
            JsonModel? json = GetData();
            return json?.MetaCharset;
        }

        public static string? GetMetaViewPort()
        {
            JsonModel? json = GetData();
            return json?.MetaViewPort;
        }

        public static string? GetTitleDocument()
        {
            JsonModel? json = GetData();
            return json?.TitleDocument;
        }

        public static string? GetLinkHead()
        {
            JsonModel? json = GetData();
            return json?.LinkHead;
        }

        public static string? GetBodySimple()
        {
            JsonModel? json = GetData();
            return TemplatingHtmlData.GetHtmlData(json.BodySimple, json);
        }

        public static string? GetBodySimpleNotValid()
        {
            JsonModel? json = GetData();
            return TemplatingHtmlData.GetHtmlData(json.BodySimpleNotValid, json);
        }

        public static string? GetDivIdContent()
        {
            JsonModel? json = GetData();
            return TemplatingHtmlData.GetHtmlData(json.DivIdContent, json);
        }

        public static string? GetDivIdContentNotValid()
        {
            JsonModel? json = GetData();
            return TemplatingHtmlData.GetHtmlData(json.DivIdContentNotValid, json);
        }

        public static string? GetPSimple()
        {
            JsonModel? json = GetData();
            return TemplatingHtmlData.GetHtmlData(json.PSimple, json);
        }

        public static string? GetQuestionPHtml()
        {
            JsonModel? json = GetData();
            return json?.QuestionPHtml;
        }

        public static string? GetPDeclarationText()
        {
            JsonModel? json = GetData();
            return TemplatingHtmlData.GetHtmlData(json.PDeclarationText, json);
        }

        public static string? GetPDeclarationTextNotValid()
        {
            JsonModel? json = GetData();
            return TemplatingHtmlData.GetHtmlData(json.PDeclarationTextNotValid, json);
        }

        public static string? GetSpanA()
        {
            JsonModel? json = GetData();
            string value = TemplatingHtmlData.GetHtmlData(json.SpanA, json);
            return value;
        }

        public static string? GetSpanInputRed()
        {
            JsonModel? json = GetData();
            return json.SpanInputRed;
        }

        public static string? GetSpanRed()
        {
            JsonModel? json = GetData();
            return json.SpanRed;
        }

        public static string? GetALink()
        {
            JsonModel? json = GetData();
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