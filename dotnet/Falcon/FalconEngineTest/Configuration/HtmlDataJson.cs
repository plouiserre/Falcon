using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngineTest.Configuration
{
    //I'm in visual studio code I do not want manage jsonfile during test
    public static class HtmlDataJson
    {
        public static string AllDataJson = """
                {
                  "ALink": "<a href=\"declaration.html\">paragraphe</a>",
                  "SpanRed": "<span class=\"red\">Et il raconte des supers trucs!!!</span>",
                  "SpanInputRed" :"<span inputmode=\"false\" class=\"red\">Et il raconte des supers trucs!!!</span>",
                  //"SpanA": "<span><a href=\"declaration.html\">paragraphe</a></span>",
                  "SpanA": "<span>{ALink}</span>",                  
                  //"PDeclarationText":"<p class=\"declarationText\"> Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span><span class=\"red\">Et il raconte des supers trucs!!!</span></p>",
                  "PDeclarationText":"<p class=\"declarationText\"> Ceci est un {SpanA}{SpanRed}</p>",
                  //"PSimple":"<p class=\"declarationText\"> Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span></p>",
                  "PSimple":"<p class=\"declarationText\"> Ceci est un {SpanA}</p>",
                  "QuestionPHtml":"<p>Allez-vous apprécier mon article?</p>",
                  //"DivIdContent":"<div id=\"content\"><p class=\"declarationText\"> Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span><span class=\"red\">Et il raconte des supers trucs!!!</span></p><p>Allez-vous apprécier mon article?</p></div>",
                  "DivIdContent":"<div id=\"content\">{PDeclarationText}{QuestionPHtml}</div>",
                  //"BodySimple":"<body class=\"main\"><div id=\"content\"><p class=\"declarationText\"> Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span><span class=\"red\">Et il raconte des supers trucs!!!</span></p><p>Allez-vous apprécier mon article?</p></div></body>",
                  "BodySimple":"<body class=\"main\">{DivIdContent}</body>",
                  "MetaCharset":"<meta charset=\"UTF-8\">",
                  "MetaViewPort":"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">",
                  "TitleDocument":"<title>Document</title>",
                  "LinkHead":"<link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">",
                  "Head":"<head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\"></head>",
                  "HtmlSimple":"<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\"></head><body class=\"main\"><div id=\"content\"><p class=\"declarationText\"> Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span><span class=\"red\">Et il raconte des supers trucs!!!</span></p><p>Allez-vous apprécier mon article?</p></div></body></html>",
                  "HtmlSimpleWithDoctype":"<!DOCTYPE html><html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\"></head><body class=\"main\"><div id=\"content\"><p class=\"declarationText\"> Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span><span class=\"red\">Et il raconte des supers trucs!!!</span></p><p>Allez-vous apprécier mon article?</p></div></body></html>",
                  "HtmlNotValidSimpleWithSpaceDoctype":"<!DOCTYPE html><html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\"></head><body class=\"main\"><div id=\"content\"><p class=\"declarationText\"> Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span><span inputmode=\"false\" class=\"red\">Et il raconte des supers trucs!!!</span></p><p>Allez-vous apprécier mon article?</p></div></body></html>",
                  "SimpleDoctype":"<!DOCTYPE html>"
                }
                """;
    }

    public class JsonModel
    {
        public string? ALink { get; set; }
        public string? SpanRed { get; set; }
        public string? SpanInputRed { get; set; }
        public string? SpanA { get; set; }
        public string? PDeclarationText { get; set; }
        public string? PSimple { get; set; }
        public string? QuestionPHtml { get; set; }
        public string? DivIdContent { get; set; }
        public string? BodySimple { get; set; }
        public string? MetaCharset { get; set; }
        public string? MetaViewPort { get; set; }
        public string? TitleDocument { get; set; }
        public string? LinkHead { get; set; }
        public string? Head { get; set; }
        public string? HtmlSimple { get; set; }
        public string? HtmlSimpleWithDoctype { get; set; }
        public string? HtmlNotValidSimpleWithSpaceDoctype { get; set; }
        public string? SimpleDoctype { get; set; }
    }
}