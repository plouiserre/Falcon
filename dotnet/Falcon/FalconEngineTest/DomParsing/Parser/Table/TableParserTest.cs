using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser.Table
{
    public class TableParserTest
    {
        [Fact]
        public void ParseAndValidateTableTag()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.postTable);
            var parser = TestFactory.InitTableParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            AssertTablePage.AssertTable(tag);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseAndNoValidateTable()
        {
            string? html = "<table alt=\"all body\"><tbody><tr><td>Hello</td></tr><tr><td>World</td></tr></tbody></table>";
            var parser = TestFactory.InitTableParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void ParseAndNoValidateChildrenTable()
        {
            string? html = "<table><tbody alt=\"all body\"><tr><td>Hello</td></tr><tr><td>World</td></tr></tbody></table>";
            var parser = TestFactory.InitTableParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }
    }
}