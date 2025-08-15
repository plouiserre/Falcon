using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.IdentifyTagParsing
{
    public class DeterminateContentTest
    {
        [Fact]
        public void DeterminateContentTitle()
        {
            string html = "<title>Content</title>";
            var determinate = new DeterminateContent();

            string content = determinate.FindContent(html, "<title>", "</title>");

            Assert.Equal("Content", content);
        }

        [Fact]
        public void DeterminateContentHeader()
        {
            string html = HtmlData.GetHead();
            var determinate = new DeterminateContent();

            string content = determinate.FindContent(html, "<head>", "</head>");

            string contentHeadSimple = HtmlData.GetHead().Replace("<head>", string.Empty).Replace("</head>", string.Empty);
            Assert.Equal(contentHeadSimple, content);
        }

        [Fact]
        public void DeterminateContentMeta()
        {
            string html = HtmlData.GetMetaViewPort();
            var determinate = new DeterminateContent();

            string content = determinate.FindContent(html, HtmlData.GetMetaViewPort(), null);

            Assert.Null(content);
        }
    }
}