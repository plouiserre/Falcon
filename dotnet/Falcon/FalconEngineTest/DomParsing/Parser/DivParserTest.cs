using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class DivParserTest
    {
        [Fact]
        public void ParseSimpleDivAndValidateIt()
        {
            string html = "<div class=\"main\"> hello world!!!</div>";

            var divParser = TestFactory.InitDivParser();

            var tag = divParser.Parse(html);
            bool isValid = divParser.IsValid();

            Assert.True(isValid);
            Assert.Equal(" hello world!!!", tag.Content);
            Assert.Single(tag.Attributes);
            Assert.Equal("classCss", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("main", tag.Attributes[0].Value);
            Assert.Equal("<div class=\"main\">", tag.TagStart);
            Assert.Equal("</div>", tag.TagEnd);
        }

        [Fact]
        public void ParseComplexeDivAndValidateIt()
        {
            string html = HtmlData.DivIdContent;

            var divParser = TestFactory.InitDivParser();

            var tag = divParser.Parse(html);
            bool isValid = divParser.IsValid();

            Assert.True(isValid);
            AssertHtml.AssertDivContent(tag);
        }
    }
}