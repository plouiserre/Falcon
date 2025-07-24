using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class SpanParserTest
    {
        [Fact]
        public void ParseAndValidateSimpleSpan()
        {
            var html = "<span>A simple text</span>";

            var parser = TestFactory.InitSpanParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.True(isValid);
            Assert.Equal("<span>", tag.TagStart);
            Assert.Equal("</span>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal(NameTagEnum.span, tag.NameTag);
            Assert.Equal("A simple text", tag.Content);
            Assert.Empty(tag.Children);
        }

        [Fact]
        public void ParseAndValidateSimpleSpanWithChildren()
        {
            var html = HtmlData.SpanA;

            var parser = TestFactory.InitSpanParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.True(isValid);
            Assert.Equal("<span>", tag.TagStart);
            Assert.Equal("</span>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal(NameTagEnum.span, tag.NameTag);
            Assert.Equal(HtmlData.ALink, tag.Content);
            Assert.Single(tag.Children);
            Assert.Equal("paragraphe", tag.Children[0].Content);
            Assert.Single(tag.Children[0].Attributes);
            Assert.Equal("href", tag.Children[0].Attributes[0].FamilyAttribute);
            Assert.Equal("declaration.html", tag.Children[0].Attributes[0].Value);
            Assert.Equal("<a href=\"declaration.html\">", tag.Children[0].TagStart);
            Assert.Equal("</a>", tag.Children[0].TagEnd);
        }

        //Test validation false
        // [Theory]
        // [InlineData("<span>")]
        // public void ParseAndFailValidationSimpleSpan(string html)
        // {
        //     var parser = TestFactory.InitSpanParser();

        //     parser.Parse(html);
        //     bool isValid = parser.IsValid();

        //     Assert.False(isValid);
        // }

        //test enfant validation false

        //test avec des espaces dans le tagstart
    }
}