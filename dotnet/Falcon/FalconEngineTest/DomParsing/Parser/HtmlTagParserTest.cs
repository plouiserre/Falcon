using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class HtmlTagParserTest
    {
        private IdentifyTag _identifyTag;
        private DeterminateContent _determinateContent;
        public HtmlTagParserTest()
        {
            _identifyTag = TestFactory.InitIdentifyTag();
            _determinateContent = TestFactory.InitDeterminateContent();
        }

        [Fact]
        public void ParseSimpleHtmlOneLine()
        {
            string html = HtmlData.HtmlSimple;
            var htmlTagParser = new HtmlTagParser(_identifyTag, _determinateContent);

            var tagHtml = htmlTagParser.Parse(html);
            bool isValid = htmlTagParser.IsValid();

            Assert.Equal(NameTagEnum.html, tagHtml.NameTag);
            Assert.Equal(HtmlData.ContentHtmlSimple, tagHtml.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
            Assert.True(isValid);
        }


        [Fact]
        public void ParseSimpleHtmlWithSpaceAndEscapment()
        {
            string html = HtmlData.HtmlSimpleWithSpace;
            var htmlTagParser = new HtmlTagParser(_identifyTag, _determinateContent);

            var tagHtml = htmlTagParser.Parse(html);
            bool isValid = htmlTagParser.IsValid();

            Assert.Equal(HtmlData.ContentHtmlSimpleWithSpace, tagHtml.Content);
            Assert.Equal(NameTagEnum.html, tagHtml.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
            Assert.True(isValid);
        }


        [Fact]
        public void ErrorDuringParsingHtmlTag()
        {
            string html = "<html>test";
            var htmlTagParser = new HtmlTagParser(_identifyTag, _determinateContent);

            var exception = Assert.Throws<HtmlParsingException>(() => htmlTagParser.Parse(html));

            Assert.Equal("Une erreur a eu lieu lors du parsing de <html>test", exception.Message);
            Assert.Equal(ErrorTypeParsing.html, exception.ErrorType);
        }

        [Theory]
        [InlineData("<html lang=\"fr\" dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\">Hello world</html>")]
        [InlineData("<html manifest=\"app.manifest\">Hello world</html>")]
        [InlineData("<html id=\"root-html\" class=\"theme-light responsive\" style=\"background-color: #f0f0f0;\">Hello world</html>")]
        [InlineData("<html accesskey=\"h\" tabindex=\"0\" contenteditable=\"false\" draggable=\"false\" spellcheck=\"true\">\">Hello world</html>")]
        [InlineData("<html data-theme=\"dark\" data-user=\"admin\" data-page=\"home\">Hello world</html>")]
        // [InlineData("<html hidden translate=\"no\" title=\"Page cachée\">Hello world</html>")]
        public void HtmlTagValidationIsTrue(string html)
        {
            var htmlTagParser = new HtmlTagParser(_identifyTag, _determinateContent);

            htmlTagParser.Parse(html);
            bool isValid = htmlTagParser.IsValid();

            Assert.True(isValid);
        }

        [Fact]
        public void HtmlTagValidationIsFalse()
        {
            var htmlTagParser = new HtmlTagParser(_identifyTag, _determinateContent);

            htmlTagParser.Parse("<html></html>");
            bool isValid = htmlTagParser.IsValid();

            Assert.False(isValid);
        }
    }
}