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
var manageChildrenTag = new ManageChildrenTag(deleteUselessSpace, identifyTag, identifyStartTagEndTag, determinateContent, attributeTagManager);
var doctypeParser = new DoctypeParser(identifyTag);
var htmlParser = new HtmlTagParser(identifyTag, manageChildrenTag, attributeTagManager);
//temporary start
var inputParser = new InputParser(identifyTag, attributeTagManager);
var labelParser = new LabelParser(identifyTag, attributeTagManager);
var selectParser = new SelectParser(identifyTag, manageChildrenTag, attributeTagManager);
var h1Parser = new H1Parser(identifyTag, manageChildrenTag, attributeTagManager);
//temporary end
var htmlParsing = new HtmlParsing(doctypeParser, htmlParser, inputParser, labelParser, selectParser, h1Parser, extractHtmlRemaining, attributeTagManager);
var engine = new HtmlEngine(htmlParsing);
var result = engine.Calculate(html);
Console.WriteLine(JsonConvert.SerializeObject(result));
