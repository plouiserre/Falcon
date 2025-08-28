using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngineTest.Configuration
{
    //I'm in visual studio code I do not want manage jsonfile during test
    public static class HtmlSimpleDataJson
    {
        public static string AllDataJson = """
                {
                  "ALink": "<a href=\"declaration.html\">paragraphe</a>",
                  "SpanRed": "<span class=\"red\">Et il raconte des supers trucs!!!</span>",
                  "SpanInputRed" :"<span alt=\"declaration badass\" class=\"red\">Et il raconte des supers trucs!!!</span>",
                  "SpanA": "<span>{ALink}</span>",                  
                  "PDeclarationText":"<p class=\"declarationText\"> Ceci est un {SpanA}{SpanRed}</p>",
                  "PDeclarationTextNotValid":"<p class=\"declarationText\"> Ceci est un {SpanA}{SpanInputRed}</p>",
                  "PSimple":"<p class=\"declarationText\"> Ceci est un {SpanA}</p>",
                  "QuestionPHtml":"<p>Allez-vous apprécier mon article?</p>",
                  "DivIdContent":"<div id=\"content\">{PDeclarationText}{QuestionPHtml}</div>",
                  "DivIdContentNotValid":"<div id=\"content\">{PDeclarationTextNotValid}{QuestionPHtml}</div>",
                  "BodySimple":"<body class=\"main\">{DivIdContent}</body>",
                  "BodySimpleNotValid":"<body class=\"main\">{DivIdContentNotValid}</body>",
                  "MetaCharset":"<meta charset=\"UTF-8\">",
                  "MetaViewPort":"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">",
                  "TitleDocument":"<title>Document</title>",
                  "LinkHead":"<link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">",
                  "Head":"<head>{MetaCharset}{MetaViewPort}{TitleDocument}{LinkHead}</head>",
                  "HtmlSimple":"<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">{Head}{BodySimple}</html>",                  
                  "HtmlSimpleNotValid":"<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">{Head}{BodySimpleNotValid}</html>",                  
                  "HtmlSimpleWithDoctype":"<!DOCTYPE html>{HtmlSimple}",
                  "HtmlSimpleWithDoctypeNotValid":"<!DOCTYPE html>{HtmlSimpleNotValid}",                  
                  "SimpleDoctype":"<!DOCTYPE html>"
                }
                """;
    }

    public class JsonSimpleDataModel
    {
        public string? ALink { get; set; }
        public string? SpanRed { get; set; }
        public string? SpanInputRed { get; set; }
        public string? SpanA { get; set; }
        public string? PDeclarationText { get; set; }
        public string? PDeclarationTextNotValid { get; set; }
        public string? PSimple { get; set; }
        public string? QuestionPHtml { get; set; }
        public string? DivIdContent { get; set; }
        public string? DivIdContentNotValid { get; set; }
        public string? BodySimple { get; set; }
        public string? BodySimpleNotValid { get; set; }
        public string? MetaCharset { get; set; }
        public string? MetaViewPort { get; set; }
        public string? TitleDocument { get; set; }
        public string? LinkHead { get; set; }
        public string? Head { get; set; }
        public string? HtmlSimple { get; set; }
        public string? HtmlSimpleNotValid { get; set; }
        public string? HtmlSimpleWithDoctype { get; set; }
        public string? HtmlSimpleWithDoctypeNotValid { get; set; }
        public string? SimpleDoctype { get; set; }
    }
}