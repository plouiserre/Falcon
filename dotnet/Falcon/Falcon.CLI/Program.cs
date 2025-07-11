// See https://aka.ms/new-console-template for more information
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Engine;
using Newtonsoft.Json;

string html = @"<!DOCTYPE html>
                    <html lang=""en"">
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

var deleteUselessSpace = new DeleteUselessSpace();
var attributeTagParser = new AttributeTagParser();
var identifyTagName = new IdentifyTagName();
var identifyTagFamily = new IdentifyTagFamily();
var identifyTag = new IdentifyTag(deleteUselessSpace, attributeTagParser, identifyTagName, identifyTagFamily);
var doctypeParser = new DoctypeParser(identifyTag);
var htmlParser = new HtmlTagParser(identifyTag);
var headParser = new HeadParser(deleteUselessSpace, identifyTag);
var extractHtmlRemaining = new ExtractHtmlRemaining();
var htmlParsing = new HtmlParsing(doctypeParser, htmlParser, headParser, extractHtmlRemaining);
var engine = new HtmlEngine(htmlParsing);
var result = engine.Calculate(html);
Console.WriteLine(JsonConvert.SerializeObject(result));
