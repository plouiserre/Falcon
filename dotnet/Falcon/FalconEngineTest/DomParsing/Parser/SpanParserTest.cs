using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class SpanParserTest
    {
        [Fact]
        public void ParseAndValidateSimpleSpan()
        {
            var html = "<span     class=\"blueText\">A simple text</span>";

            var parser = TestFactory.InitSpanParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.True(isValid);
            Assert.Equal("<span class=\"blueText\">", tag.TagStart);
            Assert.Equal("</span>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal(NameTagEnum.span, tag.NameTag);
            Assert.Equal("A simple text", tag.Content);
            Assert.Single(tag.Attributes);
            Assert.Equal("classCss", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("blueText", tag.Attributes[0].Value);
            Assert.Null(tag.Children);
        }

        [Fact]
        public void ParseAndValidateSimpleSpanWithChildren()
        {
            var html = HtmlPageSimpleData.GetSpanA();

            var parser = TestFactory.InitSpanParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.True(isValid);
            Assert.Equal("<span>", tag.TagStart);
            Assert.Equal("</span>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal(NameTagEnum.span, tag.NameTag);
            Assert.Equal(HtmlPageSimpleData.GetALink(), tag.Content);
            Assert.Single(tag.Children);
            Assert.Equal("paragraphe", tag.Children[0].Content);
            Assert.Single(tag.Children[0].Attributes);
            Assert.Equal("href", tag.Children[0].Attributes[0].FamilyAttribute);
            Assert.Equal("declaration.html", tag.Children[0].Attributes[0].Value);
            Assert.Equal("<a href=\"declaration.html\">", tag.Children[0].TagStart);
            Assert.Equal("</a>", tag.Children[0].TagEnd);
        }

        [Fact]
        public void ParseAndFailValidationSimpleSpan()
        {
            string html = "<span charset=\"UTF-8\"> Hello world </span>";
            var parser = TestFactory.InitSpanParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void ParseComplexeDivAndNotValidateIt()
        {
            string html = "<span> <a inputmode=\"false\">Hello World!!!</a></span>";

            var spanParser = TestFactory.InitSpanParser();

            spanParser.Parse(html);
            bool isValid = spanParser.IsValid();

            Assert.False(isValid);
        }

    }
}