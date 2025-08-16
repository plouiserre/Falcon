using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Data;
using FalconEngineTest.Utils.HtmlData;

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
            string html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.htmlPageWithDoctype);
            var doctypeTag = TagTestFactory.GetSimpleDoctype();
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(doctypeTag, html, ExtractionMode.ASide);

            Assert.Equal(HtmlPageSimpleData.GetHtml(TagHtmlSimple.htmlPage), htmlRemaining);
        }

        [Fact]
        public void RemoveSimpleDoctypeMultipleLine()
        {
            string html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.htmlPageWithDoctype);
            var doctypeTag = TagTestFactory.GetSimpleDoctype();
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(doctypeTag, html, ExtractionMode.ASide);

            Assert.Equal(HtmlPageSimpleData.GetHtml(TagHtmlSimple.htmlPage), htmlRemaining);
        }

        //meta

        [Fact]
        public void RemoveFirstMetaHeadContent()
        {
            string contentHeadSimple = HtmlPageSimpleData.GetHtml(TagHtmlSimple.head).Replace("<head>", string.Empty).Replace("</head>", string.Empty);
            string html = contentHeadSimple;
            var meta = TagTestFactory.GetMetaCharset();
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(meta, html, ExtractionMode.ASide);

            string htmlRemainingExpected = contentHeadSimple.Replace(HtmlPageSimpleData.GetHtml(TagHtmlSimple.metaCharset), string.Empty);
            Assert.Equal(htmlRemainingExpected, htmlRemaining);
        }

        [Fact]
        public void RemoveHeadTags()
        {
            string html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.htmlPageWithDoctype);
            var htmlTag = TagTestFactory.GetSimpleHtmlTag();
            string content = html.Replace(htmlTag.TagStart, string.Empty).Replace(htmlTag.TagEnd, string.Empty);
            htmlTag.Content = content;
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(htmlTag, html, ExtractionMode.Inside);

            Assert.Equal(content, htmlRemaining);
        }
    }
}