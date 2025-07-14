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
            string html = HtmlData.HeadSimple;
            var determinate = new DeterminateContent();

            string content = determinate.FindContent(html, "<head>", "</head>");

            Assert.Equal(HtmlData.ContentHeadSimple, content);
        }

        [Fact]
        public void DeterminateContentMeta()
        {
            string html = HtmlData.MetaViewPort;
            var determinate = new DeterminateContent();

            string content = determinate.FindContent(html, HtmlData.MetaViewPort, null);

            Assert.Null(content);
        }
    }
}