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

        //head autour
        [Fact]
        public void RemoveHtmlTags()
        {
            string html = HtmlData.HeadSimple;
            var head = HtmlPageData.GetTagModel(TagData.html);
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(head, html, ExtractionMode.Inside);

            Assert.Equal(string.Concat(HtmlData.HeadSimple, HtmlData.BodySimple), htmlRemaining);
        }

        [Fact]
        public void ManageRemoveFirstMetalInChildrenSearchTagHeader()
        {
            string html = HtmlData.ContentHeadSimple;
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(html, NameTagEnum.meta);

            string htmlExpected = HtmlData.ContentHeadSimple.Replace(HtmlData.MetaCharset, string.Empty);
            Assert.Equal(htmlExpected, htmlRemaining);
        }

        [Fact]
        public void ManageRemoveTitleInChildrenSearchTagHeader()
        {
            string html = HtmlData.ContentHeadSimple.Replace(HtmlData.MetaCharset, string.Empty)
                                                    .Replace(HtmlData.MetaViewPort, string.Empty);
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(html, NameTagEnum.title);

            string htmlExpected = html.Replace(HtmlData.TitleDocument, string.Empty);
            Assert.Equal(htmlExpected, htmlRemaining);
        }

        [Fact]
        public void ManageRemoveLinkInChildrenSearchTagHeader()
        {
            string html = HtmlData.ContentHeadSimple.Replace(HtmlData.MetaCharset, string.Empty)
                                                    .Replace(HtmlData.MetaViewPort, string.Empty)
                                                    .Replace(HtmlData.TitleDocument, string.Empty);
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(html, NameTagEnum.link);

            Assert.Equal(string.Empty, htmlRemaining);
        }

        [Fact]
        public void GetHtmlParseMeta()
        {
            string html = HtmlData.ContentHeadSimple;

            var extraction = new ExtractHtmlRemaining();

            string htmlToParse = extraction.FindHtmlParse(html);

            Assert.Equal(HtmlData.MetaCharset, htmlToParse);
        }

        [Fact]
        public void GetHtmlParseTitle()
        {
            string html = HtmlData.ContentHeadSimple.Replace(HtmlData.MetaCharset, string.Empty)
                                                    .Replace(HtmlData.MetaViewPort, string.Empty);

            var extraction = new ExtractHtmlRemaining();

            string htmlToParse = extraction.FindHtmlParse(html);

            Assert.Equal(HtmlData.TitleDocument, htmlToParse);
        }
    }
}