using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing
{
    public class HtmlTagParseTest
    {
        public HtmlTagParseTest()
        {

        }

        [Fact]
        public void ParseSimpleHtmlOneLine()
        {
            string html = HtmlData.HtmlSimple;
            var htmlTagParse = new HtmlTagParse();

            var tagHtml = htmlTagParse.Parse(html);
            bool isValid = htmlTagParse.IsValid(tagHtml);

            Assert.Equal(NameTagEnum.html, tagHtml.NameTag);
            Assert.Equal(HtmlData.ContentHtmlSimple, tagHtml.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
            Assert.True(isValid);
        }


        [Fact]
        public void ParseSimpleHtmlWithSpaceAndEscapment()
        {
            string html = HtmlData.HtmlSimpleWithSpace;
            var htmlTagParse = new HtmlTagParse();

            var tagHtml = htmlTagParse.Parse(html);
            bool isValid = htmlTagParse.IsValid(tagHtml);

            Assert.Equal(HtmlData.ContentHtmlSimpleWithSpace, tagHtml.Content);
            Assert.Equal(NameTagEnum.html, tagHtml.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
            Assert.True(isValid);
        }


        [Fact]
        public void ErrorDuringParsingHtmlTag()
        {
            string html = "<html>test";
            var htmlTagParse = new HtmlTagParse();

            var exception = Assert.Throws<HtmlParsingException>(() => htmlTagParse.Parse(html));

            Assert.Equal("Une erreur a eu lieu lors du parsing de <html>test", exception.Message);
            Assert.Equal(ErrorType.html, exception.ErrorType);
        }

    }
}