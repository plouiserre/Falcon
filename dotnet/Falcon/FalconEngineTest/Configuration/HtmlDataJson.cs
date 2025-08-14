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
                  "SpanRed": "<span class=\"red\">Et il raconte des supers trucs!!!</span>"
                }
                """;
    }

    public class JsonModel
    {
        public string? ALink { get; set; }
        public string? SpanRed { get; set; }
    }
}