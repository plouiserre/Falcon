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

        [Fact]
        public void ParseAndNoValidateTbody()
        {
            string? html = "<tbody alt=\"all body\"><tr><td>Hello</td></tr><tr><td>World</td></tr></tbody>";
            var parser = TestFactory.InitTbodyParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void ParseAndNoValidateChildrenTbody()
        {
            string? html = "<tbody><tr><td alt=\"all body\">Hello</td></tr><tr><td>World</td></tr></tbody>";
            var parser = TestFactory.InitTbodyParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }
    }
}