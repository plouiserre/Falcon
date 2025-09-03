using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class TBodyParserTest
    {
        [Fact]
        public void ParseAndValidateTbody()
        {
            string? html = HtmlPageTableData.GetHtml(TagHtmlTable.tbody);
            var parser = TestFactory.InitTbodyParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            AssertTablePage.AssertTBody(tag);
            Assert.True(isValid);
        }
    }
}