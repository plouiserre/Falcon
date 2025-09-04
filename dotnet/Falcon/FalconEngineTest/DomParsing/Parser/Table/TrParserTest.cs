using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser.Table
{
    public class TrParserTest
    {
        [Fact]
        public void ParseAndValidateTrWithManyTexts()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.architectTable);
            var parser = TestFactory.InitTrParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            AssertTablePage.AssertArchitecteTr(tag);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseAndValidateTrWithUniversalAttributsWithHelloWorld()
        {
            string html = "<tr class=\"hiddenTr\" hidden><td>Hello</td><td>World</td></tr>";
            var parser = TestFactory.InitTrParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.Equal(NameTagEnum.tr, tag.NameTag);
            Assert.Equal("<tr class=\"hiddenTr\" hidden>", tag.TagStart);
            Assert.Equal("</tr>", tag.TagEnd);
            Assert.Equal("<td>Hello</td><td>World</td>", tag.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("classCss", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("hiddenTr", tag.Attributes[0].Value);
            Assert.Equal("hidden", tag.Attributes[1].FamilyAttribute);
            AssertTrHelloWorld(tag.Children[0], "Hello");
            AssertTrHelloWorld(tag.Children[1], "World");
            Assert.True(isValid);
        }

        [Fact]
        public void ParseAndNoValidateTrWithUniversalAttributsWithHelloWorld()
        {
            string html = "<tr class=\"hiddenTr\" hidden alt=\"wrong attributs\"><td>Hello</td><td>World</td></tr>";
            var parser = TestFactory.InitTrParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.Equal(NameTagEnum.tr, tag.NameTag);
            Assert.Equal("<tr class=\"hiddenTr\" hidden alt=\"wrong attributs\">", tag.TagStart);
            Assert.Equal("</tr>", tag.TagEnd);
            Assert.Equal("<td>Hello</td><td>World</td>", tag.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("classCss", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("hiddenTr", tag.Attributes[0].Value);
            Assert.Equal("hidden", tag.Attributes[1].FamilyAttribute);
            Assert.Equal("alt", tag.Attributes[2].FamilyAttribute);
            Assert.Equal("wrong attributs", tag.Attributes[2].Value);
            AssertTrHelloWorld(tag.Children[0], "Hello");
            AssertTrHelloWorld(tag.Children[1], "World");
            Assert.False(isValid);
        }

        private void AssertTrHelloWorld(TagModel tag, string content)
        {
            Assert.Equal(NameTagEnum.td, tag.NameTag);
            Assert.Equal("<td>", tag.TagStart);
            Assert.Equal("</td>", tag.TagEnd);
            Assert.Equal(content, tag.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Null(tag.Attributes);
            Assert.Null(tag.Children);
        }

        [Fact]
        public void ParseAndNoValidateTrBecauseFirstTdIsWrong()
        {
            string html = "<tr class=\"hiddenTr\" hidden><td alt=\"wrong attributs\">Hello</td><td>World</td></tr>";
            var parser = TestFactory.InitTrParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.Equal(NameTagEnum.tr, tag.NameTag);
            Assert.Equal("<tr class=\"hiddenTr\" hidden>", tag.TagStart);
            Assert.Equal("</tr>", tag.TagEnd);
            Assert.Equal("<td alt=\"wrong attributs\">Hello</td><td>World</td>", tag.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("classCss", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("hiddenTr", tag.Attributes[0].Value);
            Assert.Equal("hidden", tag.Attributes[1].FamilyAttribute);
            Assert.False(isValid);
        }
        //Add test with valid th
    }
}