using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Data;
using FalconEngineTest.Utils;

namespace FalconEngineTest.CleanData
{
    public class ExtractHtmlRemainingTest
    {
        public ExtractHtmlRemainingTest()
        {
        }

        [Fact]
        public void RemoveSimpleDoctypeOneLine()
        {
            string html = HtmlData.HtmlSimpleWithSpaceDoctype;
            var doctypeTag = TagTestFactory.GetSimpleDoctype();
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(doctypeTag, html, ExtractionMode.ASide);

            Assert.Equal(HtmlData.HtmlSimpleWithSpace, htmlRemaining);
        }

        [Fact]
        public void RemoveSimpleDoctypeMultipleLine()
        {
            string html = HtmlData.HtmlSimpleWithSpaceDoctype;
            var doctypeTag = TagTestFactory.GetSimpleDoctype();
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(doctypeTag, html, ExtractionMode.ASide);

            Assert.Equal(HtmlData.HtmlSimpleWithSpace, htmlRemaining);
        }

        //meta

        [Fact]
        public void RemoveFirstMetaHeadContent()
        {
            string html = HtmlData.ContentHeadSimple;
            var meta = TagTestFactory.GetMetaCharset();
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(meta, html, ExtractionMode.ASide);

            string htmlRemainingExpected = HtmlData.ContentHeadSimple.Replace(HtmlData.MetaCharset, string.Empty);
            Assert.Equal(htmlRemainingExpected, htmlRemaining);
        }

        [Fact]
        public void RemoveHeadTags()
        {
            string html = HtmlData.HtmlSimpleWithSpaceDoctype;
            var htmlTag = TagTestFactory.GetSimpleHtmlTag();
            string content = html.Replace(htmlTag.TagStart, string.Empty).Replace(htmlTag.TagEnd, string.Empty);
            htmlTag.Content = content;
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(htmlTag, html, ExtractionMode.Inside);

            Assert.Equal(content, htmlRemaining);
        }
    }
}