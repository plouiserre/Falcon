using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FalconEngine.CleanData;
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

            var htmlRemaining = extraction.Extract(doctypeTag, html);

            Assert.Equal(HtmlData.HtmlSimple, htmlRemaining);
        }

        [Fact]
        public void RemoveSimpleDoctypeMultipleLine()
        {
            string html = HtmlData.HtmlSimpleWithSpaceDoctype;
            var doctypeTag = HtmlPageData.GetTagModel(TagData.doctype);
            var extraction = new ExtractHtmlRemaining();

            var htmlRemaining = extraction.Extract(doctypeTag, html);

            string expected = AssertUtils.DeleteUselessSpace(HtmlData.HtmlSimpleWithSpace);
            string result = AssertUtils.DeleteUselessSpace(htmlRemaining);
            Assert.Equal(expected, result);
        }
    }
}