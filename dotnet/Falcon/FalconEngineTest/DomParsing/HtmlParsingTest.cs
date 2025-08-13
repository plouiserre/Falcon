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
            var extractHtmlRemaining = new ExtractHtmlRemaining();
            _htmlParsing = new HtmlParsing(doctypeParser, htmlTagParser, extractHtmlRemaining, TestFactory.InitAttributeTagManager(true));
        }

        [Fact]
        public void IsSimplePageHasBeenParsingCorrectly()
        {
            HtmlPage htmlPage = SimulateParsingSimplePage.InitHtmlPage();

            var parsing = _htmlParsing.Parse(HtmlData.HtmlSimpleWithSpaceDoctype);

            Assert.True(AssertHtml.AssertTagsAreIdenticals(htmlPage.Tags, parsing.Tags));
            Assert.True(parsing.IsValid);
        }

        [Fact]
        public void IsSimplePageHasBeenNotValidateBecauseOfWrongDoctype()
        {
            HtmlPage htmlPage = SimulateParsingSimplePage.InitHtmlPage();
            string html = string.Concat("<doctype>", HtmlData.HtmlSimpleWithSpace);

            var parsing = _htmlParsing.Parse(html);

            Assert.False(parsing.IsValid);
        }

        [Fact]
        public void IsSimpleHtmlHasBeenNotValidateBecauseOfWrongHtml()
        {
            HtmlPage htmlPage = SimulateParsingSimplePage.InitHtmlPage();
            string html = string.Concat(HtmlData.SimpleDoctype, "<html scheme=\"xml\">Hello World</html>");

            var parsing = _htmlParsing.Parse(html);

            Assert.False(parsing.IsValid);
        }

        [Fact]
        public void IsSimpleHtmlHasBeenNotValidateBecauseOfWrongBodyHtml()
        {
            HtmlPage htmlPage = SimulateParsingSimplePage.InitHtmlPage();
            string html = string.Concat(HtmlData.SimpleDoctype, "<html><head><meta charset=\"UTF-8\"></head><body><a translate=\"english\">Hello World</a></body></html>");

            var parsing = _htmlParsing.Parse(html);

            Assert.False(parsing.IsValid);
        }

    }
}