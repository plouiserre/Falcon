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


            var pFirstTag = tag.Children[0];
            var pSecondTag = tag.Children[1];
            Assert.True(isValid);
            Assert.Equal(HtmlData.FirstPHtmlSimple, tag.Content);
            Assert.Single(tag.Attributes);
            Assert.Equal("id", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("content", tag.Attributes[0].Value);
            Assert.Equal("<div id=\"content\">", tag.TagStart);
            Assert.Equal("</div>", tag.TagEnd);
            Assert.Equal(2, tag.Children.Count);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal(NameTagEnum.div, tag.NameTag);
            Assert.Equal("<p class=\"declarationText\">", pFirstTag.TagStart);
            Assert.Equal("</p>", pFirstTag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, pFirstTag.TagFamily);
            Assert.Equal(NameTagEnum.p, pFirstTag.NameTag);
            Assert.Equal(" Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span>", pFirstTag.Content);
            Assert.Single(pFirstTag.Attributes);
            Assert.Equal("classCss", pFirstTag.Attributes[0].FamilyAttribute);
            Assert.Equal("declarationText", pFirstTag.Attributes[0].Value);
            Assert.Single(pFirstTag.Children);
            Assert.Equal("<span>", pFirstTag.Children[0].TagStart);
            Assert.Equal("</span>", pFirstTag.Children[0].TagEnd);
            Assert.Null(pFirstTag.Children[0].Attributes);
            Assert.Equal("<a href=\"declaration.html\">paragraphe</a>", pFirstTag.Children[0].Content);
            Assert.Equal(TagFamilyEnum.WithEnd, pFirstTag.Children[0].TagFamily);
            Assert.Equal(NameTagEnum.span, pFirstTag.Children[0].NameTag);
            Assert.Single(pFirstTag.Children[0].Children);
            Assert.Equal("<a href=\"declaration.html\">", pFirstTag.Children[0].Children[0].TagStart);
            Assert.Equal("</a>", pFirstTag.Children[0].Children[0].TagEnd);
            Assert.Equal("paragraphe", pFirstTag.Children[0].Children[0].Content);
            Assert.Equal(TagFamilyEnum.WithEnd, pFirstTag.Children[0].Children[0].TagFamily);
            Assert.Equal(NameTagEnum.a, pFirstTag.Children[0].Children[0].NameTag);
            Assert.Equal("href", pFirstTag.Children[0].Children[0].Attributes[0].FamilyAttribute);
            Assert.Equal("declaration.html", pFirstTag.Children[0].Children[0].Attributes[0].Value);
            Assert.Null(pFirstTag.Children[0].Children[0].Children);


            Assert.Equal("<p>", pSecondTag.TagStart);
            Assert.Equal("</p>", pFirstTag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, pSecondTag.TagFamily);
            Assert.Equal(NameTagEnum.p, pSecondTag.NameTag);
            Assert.Equal("Allez-vous apprécier mon article?", pSecondTag.Content);
            Assert.Null(pSecondTag.Attributes);
        }
    }
}