using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Configuration;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser.List
{
    public class UlParserTest
    {
        [Fact]
        public void ParseAndValidateIt()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.ulMenu);
            var parser = TestFactory.InitUlParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            AssertTablePage.AssertUl(tag);
            Assert.True(isValid);
        }
    }
}