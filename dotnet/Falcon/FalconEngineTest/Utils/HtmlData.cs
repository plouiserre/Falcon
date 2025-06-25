using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngineTest.Utils
{
    public static class HtmlData
    {
        public static string HtmlSimple = @"<html lang=""en"" dir=""auto"" xmlns=""http://www.w3.org/1999/xhtml""><head><metacharset=""UTF-8""><metaname=""viewport""content=""width=device-width,initial-scale=1.0""><title>Document</title><linkrel=""stylesheet""href=""main.css""></head><body><divid=""content""><pclass=""declarationText"">Ceciestun<span><ahref=""declaration.html"">paragraphe</a></span></p><p>Allez-vousappréciermonarticle?</p></div></body></html>";
        public static string HtmlSimpleWithDoctype = @"<!DOCTYPE html><html lang=""en"" dir=""auto"" xmlns=""http://www.w3.org/1999/xhtml""><head><metacharset=""UTF-8""><metaname=""viewport""content=""width=device-width,initial-scale=1.0""><title>Document</title><linkrel=""stylesheet""href=""main.css""></head><body><divid=""content""><pclass=""declarationText"">Ceciestun<span><ahref=""declaration.html"">paragraphe</a></span></p><p>Allez-vousappréciermonarticle?</p></div></body></html>";

        public static string ContentHtmlSimple = @"<head><metacharset=""UTF-8""><metaname=""viewport""content=""width=device-width,initial-scale=1.0""><title>Document</title><linkrel=""stylesheet""href=""main.css""></head><body><divid=""content""><pclass=""declarationText"">Ceciestun<span><ahref=""declaration.html"">paragraphe</a></span></p><p>Allez-vousappréciermonarticle?</p></div></body>";

        public static string HtmlSimpleWithSpaceDoctype = @"<!DOCTYPE html>
                            <html lang=""en"" dir=""auto"" xmlns=""http://www.w3.org/1999/xhtml"">
                                <head>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <title>Document</title>
                                    <link rel=""stylesheet"" href=""main.css"">
                                </head>
                                <body>
                                    <div id=""content"">
                                        <p class=""declarationText"">Ceci est un <span><a href=""declaration.html"">paragraphe</a></span></p>
                                        <p>Allez-vous apprécier mon article?</p>
                                    </div>
                                </body>
                            </html>";

        public static string HtmlSimpleWithSpace = @"<html lang=""en"" dir=""auto"" xmlns=""http://www.w3.org/1999/xhtml"">
                                                        <head>
                                                            <meta charset=""UTF-8"">
                                                            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                                            <title>Document</title>
                                                            <link rel=""stylesheet"" href=""main.css"">
                                                        </head>
                                                        <body>
                                                            <div id=""content"">
                                                                <p class=""declarationText"">Ceci est un <span><a href=""declaration.html"">paragraphe</a></span></p>
                                                                <p>Allez-vous apprécier mon article?</p>
                                                            </div>
                                                        </body>
                                                    </html>";

        public static string ContentHtmlSimpleWithSpace = @"<head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>Document</title>
                                <link rel=""stylesheet"" href=""main.css"">
                            </head>
                            <body>
                                <div id=""content"">
                                    <p class=""declarationText"">Ceci est un <span><a href=""declaration.html"">paragraphe</a></span></p>
                                    <p>Allez-vous apprécier mon article?</p>
                                </div>
                            </body>";

        public static string ContentHeadSimple = @"<meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>Document</title>
                                <link rel=""stylesheet"" href=""main.css"">";

        public static string BodyHtmlSimple = @"<div id=""content"">
                                    <p class=""declarationText"">
                                        Ceci est un 
                                            <span>
                                                <a href=""declaration.html"">
                                                    paragraphe
                                                </a>
                                            </span>
                                    </p>
                                    <p>Allez-vous apprécier mon article?</p>
                                </div>";

        public static string firstPHtmlSimple = @"<p class=""declarationText"">
                                    Ceci est un 
                                        <span>
                                            <a href=""declaration.html"">
                                                paragraphe
                                            </a>
                                        </span>
                                </p>
                                <p>Allez-vous apprécier mon article?</p>";

        public static string secondPHtmlSimple = @"Ceci est un 
                                    <span>
                                        <a href=""declaration.html"">
                                            paragraphe
                                        </a>
                                    </span>";
    }
}