using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Engine;
using FalconEngine.Models;
using FalconEngineTest.Data;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing
{
    public class HtmlParsingTest
    {
        private HtmlParsing _htmlParsing { get; set; }

        public HtmlParsingTest()
        {
            var identifyTag = TestFactory.InitIdentifyTag();
            var doctypeParser = new DoctypeParser(identifyTag);
            var htmlTagParser = TestFactory.InitHtmlTagParser(true);
            var trParser = TestFactory.InitTrParser();
            var extractHtmlRemaining = new ExtractHtmlRemaining();
            _htmlParsing = new HtmlParsing(doctypeParser, htmlTagParser, trParser, extractHtmlRemaining, TestFactory.InitAttributeTagManager(true));
        }

        [Fact]
        public void IsSimplePageHasBeenParsingCorrectly()
        {
            HtmlPage htmlPage = SimulateParsingSimplePage.InitHtmlPage();

            var parsing = _htmlParsing.Parse(HtmlPageSimpleData.GetHtml(TagHtmlSimple.htmlPageWithDoctype), false);

            Assert.True(AssertCommon.AssertTagsAreIdenticals(htmlPage.Tags, parsing.Tags));
            Assert.True(parsing.IsValid);
        }

        [Fact]
        public void IsSimplePageHasBeenNotValidateBecauseOfWrongDoctype()
        {
            string html = string.Concat("<doctype>", HtmlPageSimpleData.GetHtml(TagHtmlSimple.htmlPage));

            var parsing = _htmlParsing.Parse(html, false);

            Assert.False(parsing.IsValid);
        }

        [Fact]
        public void IsSimpleHtmlHasBeenNotValidateBecauseOfWrongHtml()
        {
            string html = string.Concat(HtmlPageSimpleData.GetHtml(TagHtmlSimple.doctype), "<html scheme=\"xml\">Hello World</html>");

            var parsing = _htmlParsing.Parse(html, false);

            Assert.False(parsing.IsValid);
        }

        [Fact]
        public void IsSimpleWrongHtmlParseAndNotValidate()
        {
            string html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.htmlNotValid);

            var parsing = _htmlParsing.Parse(html, false);

            Assert.False(parsing.IsValid);
        }

        [Fact]
        public void IsFormPageHasBeenParsingCorrectly()
        {
            HtmlPage htmlPage = SimulateParsingFormPage.InitHtmlPage();

            var parsing = _htmlParsing.Parse(HtmlPageFormData.GetHtml(TagHtmlForm.htmlFormWithDoctype), false);

            Assert.True(AssertCommon.AssertTagsAreIdenticals(htmlPage.Tags, parsing.Tags));
            Assert.True(parsing.IsValid);
        }

        [Fact]
        public void HtmlParsingFailBecauseUnknowsTagIsDiscovered()
        {
            var html = "<!DOCTYPE html><html><body><div id=\"main\"><declaration>Hello world</declaration></div></body></html>";

            var exception = Assert.Throws<UnknownTagException>(() => _htmlParsing.Parse(html, false));

            Assert.Equal(ErrorTypeParsing.unknownTag, exception.ErrorType);
            Assert.Equal("<declaration> tag is unknown", exception.Message);
        }

        [Fact]
        public void HtmlParsingFailBecauseUnknowsAttributeIsDiscovered()
        {
            var html = "<!DOCTYPE html><html><body><div id=\"main\"><p mode=\"declaration\">Hello world</p></div></body></html>";

            var exception = Assert.Throws<UnknownAttributeException>(() => _htmlParsing.Parse(html, false));

            Assert.Equal(ErrorTypeParsing.unknownAttribute, exception.ErrorType);
            Assert.Equal("mode attribute in <p mode=\"declaration\"> tag is unknown", exception.Message);
        }

        [Fact]
        public void HtmlParsingFailBecauseHtmlIsBad()
        {
            var html = "<!DOCTYPE html><html><body><div id=\"main\"><p Hello world</p></div></body></html>";

            var exception = Assert.Throws<TagBadFormattingException>(() => _htmlParsing.Parse(html, false));

            Assert.Equal(ErrorTypeParsing.badFormatting, exception.ErrorType);
            Assert.Equal("<p Hello world</p> is bad formatting", exception.Message);
        }



        [Fact]
        public void IsTablePageHasBeenParsingCorrectly()
        {
            HtmlPage htmlPage = SimulateParsingTablePage.InitHtmlPage();

            var parsing = _htmlParsing.Parse(HtmlPageTableData.GetHtml(TagHtmlTable.htmlTableWithDoctype), true);

            Assert.True(AssertCommon.AssertTagsAreIdenticals(htmlPage.Tags, parsing.Tags));
            Assert.True(parsing.IsValid);
        }
    }
}