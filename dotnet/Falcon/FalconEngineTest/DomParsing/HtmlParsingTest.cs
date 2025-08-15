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

            var parsing = _htmlParsing.Parse(HtmlData.GetHtmlSimpleWithDoctype());

            Assert.True(AssertHtml.AssertTagsAreIdenticals(htmlPage.Tags, parsing.Tags));
            Assert.True(parsing.IsValid);
        }

        [Fact]
        public void IsSimplePageHasBeenNotValidateBecauseOfWrongDoctype()
        {
            string html = string.Concat("<doctype>", HtmlData.GetHtmlSimple());

            var parsing = _htmlParsing.Parse(html);

            Assert.False(parsing.IsValid);
        }

        [Fact]
        public void IsSimpleHtmlHasBeenNotValidateBecauseOfWrongHtml()
        {
            string html = string.Concat(HtmlData.GetSimpleDoctype(), "<html scheme=\"xml\">Hello World</html>");

            var parsing = _htmlParsing.Parse(html);

            Assert.False(parsing.IsValid);
        }

        [Fact]
        public void IsSimpleWrongHtmlParseAndNotValidate()
        {
            string html = HtmlData.GetHtmlNotValidSimpleWithSpaceDoctype();

            var parsing = _htmlParsing.Parse(html);

            Assert.False(parsing.IsValid);
        }
    }
}