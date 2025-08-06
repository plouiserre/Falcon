// See https://aka.ms/new-console-template for more information
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.DomParsing.Parser.Attribute;
using FalconEngine.Engine;
using FalconEngine.Models;
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

var attributeTagManager = new AttributeTagManager();
var identifyTagName = new IdentifyTagName();
var determinateContent = new DeterminateContent();
var identifyStartTagEndTag = new IdentifyStartTagEndTag();
var extractHtmlRemaining = new ExtractHtmlRemaining();
var analyzeAttributes = new AnalyzeAttributes();
var deleteUselessSpace = new DeleteUselessSpace(identifyStartTagEndTag);
var attributeTagParser = new AttributeTagParser(identifyStartTagEndTag, analyzeAttributes);
var identifyTag = new IdentifyTag(deleteUselessSpace, attributeTagParser, identifyTagName, identifyStartTagEndTag, determinateContent);
var doctypeParser = new DoctypeParser(identifyTag);
var htmlParser = new HtmlTagParser(identifyTag, determinateContent, attributeTagManager);
var manageChildrenTag = new ManageChildrenTag(deleteUselessSpace, identifyTag, identifyStartTagEndTag, attributeTagParser, determinateContent, extractHtmlRemaining, attributeTagManager);
var spanParse = new SpanParser(identifyTag, attributeTagManager, manageChildrenTag, NameTagEnum.span);
var pParser = new PParser(identifyTag, manageChildrenTag, attributeTagManager);
var divParser = new DivParser(identifyTag, manageChildrenTag, attributeTagManager);
var headParser = new HeadParser(deleteUselessSpace, identifyTag, manageChildrenTag);
var htmlParsing = new HtmlParsing(doctypeParser, htmlParser, headParser, extractHtmlRemaining, attributeTagManager, spanParse, pParser, divParser);
var engine = new HtmlEngine(htmlParsing);
var result = engine.Calculate(html);
Console.WriteLine(JsonConvert.SerializeObject(result));
