using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class LinkParserTest
    {
        [Fact]
        public void ParseLinkTag()
        {
            string html = HtmlData.LinkHead;
            var linkTagParser = TestFactory.InitLinkParser();

            var tag = linkTagParser.Parse(html);

            Assert.Equal(html, tag.TagStart);
            Assert.Null(tag.TagEnd);
            Assert.Equal(NameTagEnum.link, tag.NameTag);
            Assert.Null(tag.Content);
            Assert.Equal(TagFamilyEnum.NoEnd, tag.TagFamily);
            Assert.Equal(2, tag.Attributes.Count);
            Assert.Equal(FamilyAttributeEnum.rel, tag.Attributes[0].FamilyAttribute);
            Assert.Equal("stylesheet", tag.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.href, tag.Attributes[1].FamilyAttribute);
            Assert.Equal("main.css", tag.Attributes[1].Value);
            Assert.Null(tag.Children);
        }

        [Theory]
        [InlineData("<link rel=\"stylesheet\" href=\"styles.css\" type=\"text / css\" media=\"screen\" hreflang=\"en\" crossorigin=\"anonymous\" integrity=\"sha384 - abc123...\" referrerpolicy=\"no - referrer\" title=\"Thème clair\" id=\"main - stylesheet\" class=\"theme - style\" style=\"\" lang=\"en\" dir=\"ltr\" accesskey=\"l\" tabindex=\"0\" draggable=\"false\" spellcheck=\"false\" translate=\"no\" role=\"presentation\" data-theme=\"light\">")]
        [InlineData("<link rel=\"icon\" href=\"favicon - 32x32.png\" type=\"image / png\" sizes=\"32x32\" hreflang=\"en\" crossorigin=\"use - credentials\" referrerpolicy=\"origin\" id=\"site - icon\" class=\"favicon\" lang=\"fr\" data-purpose=\"favicon\">")]
        [InlineData("<link rel=\"preload\" href=\"script.js\" as=\"script\" type=\"application / javascript\" crossorigin=\"anonymous\" integrity=\"sha384 - xyz456...\" referrerpolicy=\"strict - origin\" media=\"all\" id=\"preload - script\" data-preload=\"true\">")]
        public void ParseLinkTagValidate(string html)
        {
            var linkTagParser = TestFactory.InitLinkParser();

            linkTagParser.Parse(html);
            bool isValid = linkTagParser.IsValid();

            Assert.True(isValid);
        }

        [Fact]
        public void ParseLinkTableAndValidateWithOneAttributePresent()
        {
            string html = "<link rel=\"stylesheet\" href=\"alt-theme.css\" type=\"text/css\" title=\"Thème alternatif\" disabled media=\"screen\">";
            var linkTagParser = TestFactory.InitLinkParser();

            var tag = linkTagParser.Parse(html);
            bool isValid = linkTagParser.IsValid();

            Assert.True(isValid);
            Assert.Equal(6, tag.Attributes.Count);
            Assert.Equal(FamilyAttributeEnum.rel, tag.Attributes[0].FamilyAttribute);
            Assert.Equal("stylesheet", tag.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.href, tag.Attributes[1].FamilyAttribute);
            Assert.Equal("alt-theme.css", tag.Attributes[1].Value);
            Assert.Equal(FamilyAttributeEnum.type, tag.Attributes[2].FamilyAttribute);
            Assert.Equal("text/css", tag.Attributes[2].Value);
            Assert.Equal(FamilyAttributeEnum.title, tag.Attributes[3].FamilyAttribute);
            Assert.Equal("Thème alternatif", tag.Attributes[3].Value);
            Assert.Equal(FamilyAttributeEnum.disabled, tag.Attributes[4].FamilyAttribute);
            Assert.Null(tag.Attributes[4].Value);
            Assert.Equal(FamilyAttributeEnum.media, tag.Attributes[5].FamilyAttribute);
            Assert.Equal("screen", tag.Attributes[5].Value);
        }

        [Theory]
        [InlineData("<link rel=\"stylesheet\" href=\"styles.css\"></link>")]
        [InlineData("<link rel=\"preload\" href=\"script.js\" charset=\"UTF-8\">")]
        public void ParseLinkAndNoValidateHtml(string html)
        {
            var linkTagParser = TestFactory.InitLinkParser();

            linkTagParser.Parse(html);
            bool isValid = linkTagParser.IsValid();

            Assert.False(isValid);
        }
    }
}