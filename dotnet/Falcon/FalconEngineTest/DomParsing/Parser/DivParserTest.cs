using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class DivParserTest
    {
        [Fact]
        public void ParseSimpleDivAndValidateIt()
        {
            string html = "<div class=\"main\" onclick=\"popup()\"> hello world!!!</div>";

            var divParser = TestFactory.InitDivParser();

            var tag = divParser.Parse(html);
            bool isValid = divParser.IsValid();

            Assert.True(isValid);
            Assert.Equal(" hello world!!!", tag.Content);
            Assert.Equal(2, tag.Attributes.Count);
            Assert.Equal("classCss", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("main", tag.Attributes[0].Value);
            Assert.Equal("onclick", tag.Attributes[1].FamilyAttribute);
            Assert.Equal("popup()", tag.Attributes[1].Value);
            Assert.Equal("<div class=\"main\" onclick=\"popup()\">", tag.TagStart);
            Assert.Equal("</div>", tag.TagEnd);
        }

        [Fact]
        public void ParseComplexeDivAndValidateIt()
        {
            string html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.divIdContent);

            var divParser = TestFactory.InitDivParser();

            var tag = divParser.Parse(html);
            bool isValid = divParser.IsValid();

            Assert.True(isValid);
            AssertSimplePage.AssertDivContent(tag);
        }


        [Fact]
        public void ParseComplexeDivAndNotValidateIt()
        {
            string html = "<div><p inputmode=\"false\">Hello World!!!</p></div>";

            var divParser = TestFactory.InitDivParser();

            divParser.Parse(html);
            bool isValid = divParser.IsValid();

            Assert.False(isValid);
        }
    }
}