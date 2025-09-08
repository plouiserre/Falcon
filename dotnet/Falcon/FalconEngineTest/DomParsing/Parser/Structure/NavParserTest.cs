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

        [Fact]
        public void UlParseAndNoValidateItWrongAttributs()
        {
            string html = "<nav alt=\"list\"><ul><li><a href=\"article.htm\">My article</a></li></ul></nav>";
            var navParser = TestFactory.InitNavParser();

            navParser.Parse(html);
            bool isValid = navParser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void UlParseAndNoValidateItChildrenWrongAttributs()
        {
            string html = "<nav><ul><li alt=\"list\"><a href=\"article.htm\">My article</a></li></ul></nav>";
            var navParser = TestFactory.InitNavParser();

            navParser.Parse(html);
            bool isValid = navParser.IsValid();

            Assert.False(isValid);
        }
    }
}