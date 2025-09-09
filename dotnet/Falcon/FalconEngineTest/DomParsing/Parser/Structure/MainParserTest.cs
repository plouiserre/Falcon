using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser.Structure
{
    public class MainParserTest
    {
        [Fact]
        public void MainParseAndValidateIt()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.mainTag);
            var parser = TestFactory.InitMainParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            AssertTablePage.AssertMain(tag);
            Assert.True(isValid);
        }
    }
}