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

        public static string MetaCharset = @"<meta charset=""UTF-8"">";

        public static string MetaViewPort = @"<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">";

        public static string TitleDocument = @"<title>Document</title>";

        public static string LinkHead = "<link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">";

        public static string ContentHeadSimple = string.Concat(MetaCharset, MetaViewPort, TitleDocument, LinkHead);

        public static string HeadSimple = string.Concat("<head>", ContentHeadSimple, "</head>");

        public static string ContentPHtmlSimple = @" Ceci est un <span><a href=""declaration.html"">paragraphe</a></span>";

        public static string ContentHtmlSimpleWithSpace = string.Concat(HeadSimple, GetBodySimple());

        public static string HtmlSimpleWithSpace = string.Concat("<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">", ContentHtmlSimpleWithSpace, "</html>");

        public static string HtmlSimpleWithSpaceDoctype = string.Concat("<!DOCTYPE html>", HtmlSimpleWithSpace);

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