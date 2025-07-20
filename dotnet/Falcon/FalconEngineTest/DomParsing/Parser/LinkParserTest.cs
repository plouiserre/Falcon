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
        // [InlineData("<link rel=\"manifest\" href=\" / site.webmanifest\" type=\"application / manifest + json\" crossorigin=\"use - credentials\" referrerpolicy=\"origin - when - cross - origin\" id=\"web - app - manifest\">")]
        // [InlineData("<link rel=\"stylesheet\" href=\"alt-theme.css\" type=\"text/css\" title=\"Thème alternatif\" disabled media=\"screen\">")]
        public void ParseLinkTagValidate(string html)
        {
            var linkTagParser = TestFactory.InitLinkParser();

            linkTagParser.Parse(html);
            bool isValid = linkTagParser.IsValid();

            Assert.True(isValid);
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