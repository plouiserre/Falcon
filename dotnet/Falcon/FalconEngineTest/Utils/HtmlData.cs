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
        public static string SimpleDoctype = @"<!DOCTYPE html>";

        public static string ContentHeadSimple = string.Concat(GetMetaCharset(), GetMetaViewPort(), GetTitleDocument(), GetLinkHead());

        public static string ContentPHtmlSimple = @" Ceci est un <span><a href=""declaration.html"">paragraphe</a></span>";

        public static string ContentHtmlSimpleWithSpace = string.Concat(GetHead(), GetBodySimple());

        public static string HtmlSimpleWithSpaceDoctype = string.Concat("<!DOCTYPE html>", GetHtmlSimple());

        public static string? GetHtmlSimple()
        {
            JsonModel? json = GetData();
            return json?.HtmlSimple;
        }

        public static string? GetHead()
        {
            JsonModel? json = GetData();
            return json?.Head;
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
            return json?.BodySimple;
        }

        public static string? GetDivIdContent()
        {
            JsonModel? json = GetData();
            return json?.DivIdContent;
        }

        public static string? GetPSimple()
        {
            JsonModel? json = GetData();
            return json?.PSimple;
        }

        public static string? GetQuestionPHtml()
        {
            JsonModel? json = GetData();
            return json?.QuestionPHtml;
        }

        public static string? GetPDeclarationText()
        {
            JsonModel? json = GetData();
            return json.PDeclarationText;
        }

        public static string? GetSpanA()
        {
            JsonModel? json = GetData();
            return json.SpanA;
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