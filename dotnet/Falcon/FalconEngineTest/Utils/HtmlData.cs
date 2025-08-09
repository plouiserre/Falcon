using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngineTest.Utils
{
    public static class HtmlData
    {
        public static string HtmlSimple = @"<html lang=""en"" dir=""auto"" xmlns=""http://www.w3.org/1999/xhtml""><head><metacharset=""UTF-8""><metaname=""viewport""content=""width=device-width,initial-scale=1.0""><title>Document</title><linkrel=""stylesheet""href=""main.css""></head><body><divid=""content""><pclass=""declarationText"">Ceciestun<span><ahref=""declaration.html"">paragraphe</a></span></p><p>Allez-vousappréciermonarticle?</p></div></body></html>";
        public static string SimpleDoctype = @"<!DOCTYPE html>";
        public static string HtmlSimpleWithDoctype = string.Concat(SimpleDoctype, @"<html lang=""en"" dir=""auto"" xmlns=""http://www.w3.org/1999/xhtml""><head><metacharset=""UTF-8""><metaname=""viewport""content=""width=device-width,initial-scale=1.0""><title>Document</title><linkrel=""stylesheet""href=""main.css""></head><body><divid=""content""><pclass=""declarationText"">Ceciestun<span><ahref=""declaration.html"">paragraphe</a></span></p><p>Allez-vousappréciermonarticle?</p></div></body></html>");

        public static string ContentHtmlSimple = @"<head><metacharset=""UTF-8""><metaname=""viewport""content=""width=device-width,initial-scale=1.0""><title>Document</title><linkrel=""stylesheet""href=""main.css""></head><body><divid=""content""><pclass=""declarationText"">Ceciestun<span><ahref=""declaration.html"">paragraphe</a></span></p><p>Allez-vousappréciermonarticle?</p></div></body>";

        public static string MetaCharset = @"<meta charset=""UTF-8"">";
        public static string MetaViewPort = @"<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">";
        public static string TitleDocument = @"<title>Document</title>";
        public static string LinkHead = "<link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">";
        public static string ContentHeadSimple = string.Concat(MetaCharset, MetaViewPort, TitleDocument, LinkHead);

        public static string HeadSimple = string.Concat("<head>", ContentHeadSimple, "</head>");

        public static string BodySimple = string.Concat("<body>", DivIdContent, "</body>");

        public static string ContentHtmlSimpleWithSpace = string.Concat(HeadSimple, BodySimple);

        public static string HtmlSimpleWithSpace = string.Concat("<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">", ContentHtmlSimpleWithSpace, "</html>");

        public static string HtmlSimpleWithSpaceDoctype = string.Concat("<!DOCTYPE html>", HtmlSimpleWithSpace);

        public static string ALink = "<a href=\"declaration.html\">paragraphe</a>";

        public static string SpanA = string.Concat("<span>", ALink, "</span>");

        public static string QuestionPHtml = "<p>Allez-vous apprécier mon article?</p>";

        public static string ContentPHtmlSimple = @" Ceci est un <span><a href=""declaration.html"">paragraphe</a></span>";

        //TODO rename
        public static string ThirdPHtmlSimple = "<p class=\"declarationText\"> Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span></p>";

        public static string spanRed = "<span class=\"red\">Et il raconte des supers trucs!!!</span>";
        public static string PHtmlSimple = string.Concat("<p class=\"declarationText\">", ContentPHtmlSimple, spanRed, "</p>");
        public static string DivIdContent = string.Concat("<div id=\"content\">", PHtmlSimple, QuestionPHtml, "</div>");

    }
}