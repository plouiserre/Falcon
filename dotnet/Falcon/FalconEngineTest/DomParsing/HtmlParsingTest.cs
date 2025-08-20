using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
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
            var inputParser = TestFactory.InitInputParser();
            var optionParser = TestFactory.InitOptionParser();
            var extractHtmlRemaining = new ExtractHtmlRemaining();
            _htmlParsing = new HtmlParsing(doctypeParser, htmlTagParser, inputParser, optionParser, extractHtmlRemaining, TestFactory.InitAttributeTagManager(true));
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

            var parsing = _htmlParsing.Parse(HtmlPageFormData.GetHtml(TagHtmlForm.htmlFormWithDoctype), true);

            Assert.True(AssertCommon.AssertTagsAreIdenticals(htmlPage.Tags, parsing.Tags));
            Assert.True(parsing.IsValid);
        }
    }
}