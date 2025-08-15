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
                  "SpanA": "<span><a href=\"declaration.html\">paragraphe</a></span>",
                  "PDeclarationText":"<p class=\"declarationText\"> Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span><span class=\"red\">Et il raconte des supers trucs!!!</span></p>",
                  "QuestionPHtml":"<p>Allez-vous apprécier mon article?</p>"
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
        public string? QuestionPHtml { get; set; }
    }
}