using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser.List
{
    public class LiParserTest
    {
        [Fact]
        public void LiParseAndValid()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.liHome);
            var liParser = TestFactory.InitLiParser();

            var tag = liParser.Parse(html);
            var isValid = liParser.IsValid();

            AssertTablePage.AssertLiHome(tag);
            Assert.True(isValid);
        }

        [Fact]
        public void LiWithALinkParseAndValid()
        {
            string html = "<li><a href=\"article.htm\">My article</a></li>";
            var liParser = TestFactory.InitLiParser();

            var tag = liParser.Parse(html);
            var isValid = liParser.IsValid();

            Assert.Equal(NameTagEnum.li, tag.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("<li>", tag.TagStart);
            Assert.Equal("</li>", tag.TagEnd);
            Assert.Equal("<a href=\"article.htm\">My article</a>", tag.Content);
            Assert.Null(tag.Attributes);
            Assert.Equal(NameTagEnum.a, tag.Children[0].NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.Children[0].TagFamily);
            Assert.Equal("<a href=\"article.htm\">", tag.Children[0].TagStart);
            Assert.Equal("</a>", tag.Children[0].TagEnd);
            Assert.Equal("My article", tag.Children[0].Content);
            Assert.Equal("href", tag.Children[0].Attributes[0].FamilyAttribute);
            Assert.Equal("article.htm", tag.Children[0].Attributes[0].Value);
            Assert.Null(tag.Children[0].Children);
            Assert.True(isValid);
        }

        [Fact]
        public void LiParseAndNoValidIt()
        {
            string html = "<li alt=\"home description\">Home</li>";
            var liParser = TestFactory.InitLiParser();

            liParser.Parse(html);
            var isValid = liParser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void LiWithALinkParseAndNoValidIt()
        {
            string html = "<li><a href=\"article.htm\" alt=\"article description\">My article</a></li>";
            var liParser = TestFactory.InitLiParser();

            liParser.Parse(html);
            var isValid = liParser.IsValid();

            Assert.False(isValid);
        }
    }
}