using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.IdentifyTagParsing
{
    public class DeterminateContentTest
    {
        [Fact]
        public void DeterminateContentTitle()
        {
            string html = "<title>Content</title>";
            var determinate = TestFactory.InitDeterminateContent();

            string content = determinate.FindContent(html, "<title>", "</title>");

            Assert.Equal("Content", content);
        }

        [Fact]
        public void DeterminateContentHeader()
        {
            string html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.head);
            var determinate = TestFactory.InitDeterminateContent();

            string content = determinate.FindContent(html, "<head>", "</head>");

            string contentHeadSimple = HtmlPageSimpleData.GetHtml(TagHtmlSimple.head).Replace("<head>", string.Empty).Replace("</head>", string.Empty);
            Assert.Equal(contentHeadSimple, content);
        }

        [Fact]
        public void DeterminateContentMeta()
        {
            string html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.metaviewPort);
            var determinate = TestFactory.InitDeterminateContent();

            string content = determinate.FindContent(html, HtmlPageSimpleData.GetHtml(TagHtmlSimple.metaviewPort), null);

            Assert.Null(content);
        }

        [Fact]
        public void DeterminateContentDoubleDiv()
        {
            string html = "<div class=\"greetings\"><div class=\"doubleDiv\"> Hello World</div></div>";
            var determinate = TestFactory.InitDeterminateContent();

            
            string contentDoubleDiv = determinate.FindContent(html, "<div class=\"greetings\">", "</div>");

            string content = "<div class=\"doubleDiv\"> Hello World</div>";
            Assert.Equal(content, contentDoubleDiv);
        }
    }
}