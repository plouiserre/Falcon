using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser.Structure
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
            string html = "<div><p alt=\"basic message\">Hello World!!!</p></div>";

            var divParser = TestFactory.InitDivParser();

            divParser.Parse(html);
            bool isValid = divParser.IsValid();

            Assert.False(isValid);
        }

        //[Fact]
        //public void ParseMultipleDivValidateIt()
        //{
        //    string html = "<div class=\"main\"><div class=\"second\"><p>Hello World!!!</p></div></div>";

        //    var divParser = TestFactory.InitDivParser();

        //    var tag = divParser.Parse(html);
        //    bool isValid = divParser.IsValid();

        //    Assert.Equal(NameTagEnum.div, tag.NameTag);
        //    Assert.Equal("<div class=\"main\">", tag.TagStart);
        //    Assert.Equal("</div>", tag.TagEnd);
        //    Assert.Equal("<div class=\"second\"><p>Hello World!!!</p></div>", tag.Content);
        //    Assert.Equal(FamilyAttributeEnum.classCss.ToString(), tag.Attributes[0].FamilyAttribute);
        //    Assert.Equal("main", tag.Attributes[0].Value);
        //    Assert.Equal(NameTagEnum.div, tag.Children[0].NameTag);
        //    Assert.Equal("<div class=\"second\">", tag.Children[0].TagStart);
        //    Assert.Equal("</div>", tag.Children[0].TagEnd);
        //    Assert.Equal("<p>Hello World!!!</p>", tag.Children[0].Content);
        //    Assert.Equal(FamilyAttributeEnum.classCss.ToString(), tag.Children[0].Attributes[0].FamilyAttribute);
        //    Assert.Equal("second", tag.Attributes[0].Value);
        //    Assert.Equal(NameTagEnum.p, tag.Children[0].NameTag);
        //    Assert.Equal("<p>", tag.Children[0].Children[0].TagStart);
        //    Assert.Equal("</p>", tag.Children[0].Children[0].TagEnd);
        //    Assert.Equal("Hello World!!!", tag.Children[0].Children[0].Content);
        //    Assert.Null(tag.Children[0].Children[0].Attributes);
        //    Assert.True(isValid);
        //}
    }
}