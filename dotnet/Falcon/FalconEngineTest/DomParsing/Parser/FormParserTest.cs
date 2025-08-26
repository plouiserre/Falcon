using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Configuration;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class FormParserTest
    {
        [Fact]
        public void ParseFormAndValidateIt()
        {
            string? html = HtmlPageFormData.GetHtml(TagHtmlForm.form);
            var formParser = TestFactory.InitFormParser();

            var tag = formParser.Parse(html);
            bool isValid = formParser.IsValid();

            string startTag = "<form method=\"POST\" action=\"/candidate\">";
            string endTag = "</form>";
            string content = html.Replace(startTag, string.Empty).Replace(endTag, string.Empty);
            AssertFormPage.AssertForm(tag, content);
            Assert.True(isValid);
        }
    }
}