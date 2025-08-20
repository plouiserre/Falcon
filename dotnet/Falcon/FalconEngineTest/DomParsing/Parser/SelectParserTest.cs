using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class SelectParserTest
    {
        [Fact]
        public void ParseSelect()
        {
            var html = HtmlPageFormData.GetHtml(TagHtmlForm.selectSituation);
            var parser = TestFactory.InitSelectParser();

            var tag = parser.Parse(html);

            AssertFormPage.AssertSelectSituation(tag);
        }
    }
}