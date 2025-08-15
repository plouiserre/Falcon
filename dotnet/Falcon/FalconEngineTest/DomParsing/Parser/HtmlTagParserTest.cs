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
    //TODO revoir ces tests
    public class HtmlTagParserTest
    {
        [Fact]
        public void ParseSimpleHtmlOneLine()
        {
            string html = HtmlData.GetHtmlSimple();
            var htmlTagParser = TestFactory.InitHtmlTagParser(true);

            var tagHtml = htmlTagParser.Parse(html);
            bool isValid = htmlTagParser.IsValid();

            string contentHtmlSimpleWithSpace = HtmlData.GetHtmlSimple().Replace("<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">", string.Empty)
                                                .Replace("</html>", string.Empty);
            Assert.Equal(NameTagEnum.html, tagHtml.NameTag);
            Assert.Equal(contentHtmlSimpleWithSpace, tagHtml.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
            Assert.True(isValid);
        }


        [Fact]
        public void ParseSimpleHtmlWithSpaceAndEscapment()
        {
            string html = HtmlData.GetHtmlSimple();
            var htmlTagParser = TestFactory.InitHtmlTagParser(true);

            var tagHtml = htmlTagParser.Parse(html);
            bool isValid = htmlTagParser.IsValid();

            string contentHtmlSimpleWithSpace = HtmlData.GetHtmlSimple().Replace("<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">", string.Empty)
                                                .Replace("</html>", string.Empty);
            Assert.Equal(contentHtmlSimpleWithSpace, tagHtml.Content);
            Assert.Equal(NameTagEnum.html, tagHtml.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
            Assert.True(isValid);
        }


        [Fact]
        public void ErrorDuringParsingHtmlTag()
        {
            string html = "<html>test";
            var htmlTagParser = TestFactory.InitHtmlTagParser(true);

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
        public void HtmlTagValidationIsTrue(string html)
        {
            var htmlTagParser = TestFactory.InitHtmlTagParser(true);

            htmlTagParser.Parse(html);
            bool isValid = htmlTagParser.IsValid();

            Assert.True(isValid);
        }

        [Fact]
        public void HtmlTagValidationIsFalse()
        {
            var htmlTagParser = TestFactory.InitHtmlTagParser(true);

            htmlTagParser.Parse("<html></html>");
            bool isValid = htmlTagParser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void HtmlTagWithChildrenIsParsedAndOk()
        {
            var html = string.Concat("<html>", HtmlData.GetDivIdContent(), "</html>");
            var htmlTagParser = TestFactory.InitHtmlTagParser(true);

            var tag = htmlTagParser.Parse(html);
            bool isValid = htmlTagParser.IsValid();

            Assert.True(isValid);
            Assert.Equal("<html>", tag.TagStart);
            Assert.Equal("</html>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal(HtmlData.GetDivIdContent(), tag.Content);
            Assert.Single(tag.Children);
            AssertHtml.AssertDivContent(tag.Children[0]);
        }
    }
}