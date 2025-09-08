using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser.Structure
{
    public class NavParserTest
    {
        [Fact]
        public void NavParseAndValidateIt()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.navTag);
            var navParser = TestFactory.InitNavParser();

            var tag = navParser.Parse(html);
            bool isValid = navParser.IsValid();

            AssertTablePage.AssertNav(tag);
            Assert.True(isValid);
        }
    }
}