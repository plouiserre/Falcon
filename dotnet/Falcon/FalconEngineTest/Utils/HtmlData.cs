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

        public static string? GetHtmlNotValidSimpleWithSpaceDoctype()
        {
            JsonModel? json = GetData();
            return json?.HtmlNotValidSimpleWithSpaceDoctype;
        }

        public static string? GetHtmlSimpleWithDoctype()
        {
            JsonModel? json = GetData();
            return json?.HtmlSimpleWithDoctype;
        }

        public static string? GetHtmlSimple()
        {
            JsonModel? json = GetData();
            return json?.HtmlSimple;
        }

        public static string? GetHead()
        {
            JsonModel? json = GetData();
            string value = TemplatingHtmlData.GetHtmlData(json.Head, json);
            return value;
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
            string value = TemplatingHtmlData.GetHtmlData(json.BodySimple, json);
            return value;
        }

        public static string? GetDivIdContent()
        {
            JsonModel? json = GetData();
            string value = TemplatingHtmlData.GetHtmlData(json.DivIdContent, json);
            return value;
        }

        public static string? GetPSimple()
        {
            JsonModel? json = GetData();
            string value = TemplatingHtmlData.GetHtmlData(json.PSimple, json);
            return value;
        }

        public static string? GetQuestionPHtml()
        {
            JsonModel? json = GetData();
            return json?.QuestionPHtml;
        }

        public static string? GetPDeclarationText()
        {
            JsonModel? json = GetData();
            string value = TemplatingHtmlData.GetHtmlData(json.PDeclarationText, json);
            return value;
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