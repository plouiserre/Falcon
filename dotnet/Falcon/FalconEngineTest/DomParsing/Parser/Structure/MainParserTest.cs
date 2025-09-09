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

        [Fact]
        public void MainParseAndNoValidate()
        {
            string? html = "<main alt=\"all body\"><table><tbody><tr><td>Hello</td></tr><tr><td>World</td></tr></tbody></table></main>";
            var parser = TestFactory.InitMainParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void MainParseAndNoValidateTable()
        {
            string? html = "<main><table alt=\"all body\"><tbody><tr><td>Hello</td></tr><tr><td>World</td></tr></tbody></table></main>";
            var parser = TestFactory.InitMainParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }
    }
}