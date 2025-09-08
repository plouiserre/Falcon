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
        public void UlParseAndValidateIt()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.ulMenu);
            var parser = TestFactory.InitUlParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            AssertTablePage.AssertUl(tag);
            Assert.True(isValid);
        }

        [Fact]
        public void UlParseAndNoValidateItWrongAttributs()
        {
            string html = "<ul alt=\"list\"><li><a href=\"article.htm\">My article</a></li></ul>";
            var parser = TestFactory.InitUlParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void UlParseAndNoValidateItChildHaveWrongAttributs()
        {
            string html = "<ul><li><a alt=\"list\" href=\"article.htm\">My article</a></li></ul>";
            var parser = TestFactory.InitUlParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }

    }
}