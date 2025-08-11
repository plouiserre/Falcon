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
            string html = HtmlData.HtmlSimpleWithDoctype;
            var doctypeTag = HtmlPageData.GetTagModel(TagData.doctype);
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(doctypeTag, html, ExtractionMode.ASide);

            Assert.Equal(HtmlData.HtmlSimple, htmlRemaining);
        }

        [Fact]
        public void RemoveSimpleDoctypeMultipleLine()
        {
            string html = HtmlData.HtmlSimpleWithSpaceDoctype;
            var doctypeTag = HtmlPageData.GetTagModel(TagData.doctype);
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(doctypeTag, html, ExtractionMode.ASide);

            Assert.Equal(HtmlData.HtmlSimpleWithSpace, htmlRemaining);
        }

        //meta

        [Fact]
        public void RemoveFirstMetaHeadContent()
        {
            string html = HtmlData.ContentHeadSimple;
            var meta = HtmlPageData.GetTagModel(TagData.metaCharset);
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(meta, html, ExtractionMode.ASide);

            string htmlRemainingExpected = HtmlData.ContentHeadSimple.Replace(HtmlData.MetaCharset, string.Empty);
            Assert.Equal(htmlRemainingExpected, htmlRemaining);
        }

        [Fact]
        public void RemoveHeadTags()
        {
            string html = HtmlData.HtmlSimple;
            var htmlTag = new TagModel()
            {
                TagStart = "<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">",
                TagEnd = "</html>",
                TagFamily = TagFamilyEnum.WithEnd
            };
            string content = html.Replace(htmlTag.TagStart, string.Empty).Replace(htmlTag.TagEnd, string.Empty);
            htmlTag.Content = content;
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(htmlTag, html, ExtractionMode.Inside);

            Assert.Equal(content, htmlRemaining);
        }
    }
}