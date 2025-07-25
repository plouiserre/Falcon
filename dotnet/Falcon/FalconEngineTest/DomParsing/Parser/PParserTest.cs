using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class PParserTest
    {
        [Fact]
        public void ValidateParagraph()
        {
            var html = "<p class=\"blueText\">A simple paragraph</p>";

            var parser = TestFactory.InitPParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.True(isValid);
            Assert.Equal("<p class=\"blueText\">", tag.TagStart);
            Assert.Equal("</p>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal(NameTagEnum.p, tag.NameTag);
            Assert.Equal("A simple paragraph", tag.Content);
            Assert.Single(tag.Attributes);
            Assert.Equal("classCss", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("blueText", tag.Attributes[0].Value);
            Assert.Empty(tag.Children);
        }

        //validate with children
        [Fact]
        public void ValidateParagraphWithChildren()
        {
            var html = HtmlData.ThirdPHtmlSimple;

            var parser = TestFactory.InitPParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.True(isValid);
            Assert.Equal("<p class=\"declarationText\">", tag.TagStart);
            Assert.Equal("</p>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal(NameTagEnum.p, tag.NameTag);
            Assert.Equal(" Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span>", tag.Content);
            Assert.Single(tag.Attributes);
            Assert.Equal("classCss", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("declarationText", tag.Attributes[0].Value);
            Assert.Single(tag.Children);
            Assert.Equal("<span>", tag.Children[0].TagStart);
            Assert.Equal("</span>", tag.Children[0].TagEnd);
            Assert.Null(tag.Children[0].Attributes);
            Assert.Equal("<a href=\"declaration.html\">paragraphe</a>", tag.Children[0].Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.Children[0].TagFamily);
            Assert.Equal(NameTagEnum.p, tag.Children[0].NameTag);
            Assert.Single(tag.Children[0].Children);
            Assert.Equal("<a href=\"declaration.html\">", tag.Children[0].Children[0].TagStart);
            Assert.Equal("</a>", tag.Children[0].Children[0].TagEnd);
            Assert.Equal("paragraphe", tag.Children[0].Children[0].Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.Children[0].TagFamily);
            Assert.Equal(NameTagEnum.a, tag.Children[0].NameTag);
            Assert.Equal("href", tag.Children[0].Attributes[0].FamilyAttribute);
            Assert.Equal("declaration.html", tag.Children[0].Attributes[0].Value);
            Assert.Single(tag.Children[0].Children[0].Children);
        }
        //validate with space

        //no validate only tag

        //no validate because one child is no validate
    }
}