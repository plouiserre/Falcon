using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class HtmlTagParserTest
    {
        private IdentifyTag _identifyTag;
        public HtmlTagParserTest()
        {
            var deleteUselessSpace = new DeleteUselessSpace();
            var attributeTagParser = new AttributeTagParser();
            _identifyTag = new IdentifyTag(deleteUselessSpace, attributeTagParser);
        }

        [Fact]
        public void ParseSimpleHtmlOneLine()
        {
            string html = HtmlData.HtmlSimple;
            var htmlTagParser = new HtmlTagParser(_identifyTag);

            var tagHtml = htmlTagParser.Parse(html);
            bool isValid = htmlTagParser.IsValid(tagHtml);

            Assert.Equal(NameTagEnum.html, tagHtml.NameTag);
            Assert.Equal(HtmlData.ContentHtmlSimple, tagHtml.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
            Assert.True(isValid);
        }


        [Fact]
        public void ParseSimpleHtmlWithSpaceAndEscapment()
        {
            string html = HtmlData.HtmlSimpleWithSpace;
            var htmlTagParser = new HtmlTagParser(_identifyTag);

            var tagHtml = htmlTagParser.Parse(html);
            bool isValid = htmlTagParser.IsValid(tagHtml);

            Assert.Equal(HtmlData.ContentHtmlSimpleWithSpace, tagHtml.Content);
            Assert.Equal(NameTagEnum.html, tagHtml.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
            Assert.True(isValid);
        }


        [Fact]
        public void ErrorDuringParsingHtmlTag()
        {
            string html = "<html>test";
            var htmlTagParser = new HtmlTagParser(_identifyTag);

            var exception = Assert.Throws<HtmlParsingException>(() => htmlTagParser.Parse(html));

            Assert.Equal("Une erreur a eu lieu lors du parsing de <html>test", exception.Message);
            Assert.Equal(ErrorTypeParsing.html, exception.ErrorType);
        }

    }
}