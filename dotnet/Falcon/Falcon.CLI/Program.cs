// See https://aka.ms/new-console-template for more information
using FalconEngine.DomParsing;
using FalconEngine.Engine;
using Newtonsoft.Json;

string html = @"<html lang=""en"">
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

var htmlParse = new HtmlTagParse();
var htmlParsing = new HtmlParsing(htmlParse, html);
var engine = new HtmlEngine(htmlParsing);
var result = engine.Calculate(html);
Console.WriteLine(JsonConvert.SerializeObject(result));
