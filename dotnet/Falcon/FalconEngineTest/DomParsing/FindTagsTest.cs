using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing
{
    public class FindTagsTest
    {
        [Fact]
        public void FindHeadTags()
        {
            string html = HtmlData.ContentHeadSimple;
            var finder = new FindTags();

            var tags = finder.Find(html);

            Assert.Equal(HtmlData.MetaCharset, tags[0]);
            Assert.Equal(HtmlData.MetaViewPort, tags[1]);
            Assert.Equal(HtmlData.TitleDocument, tags[2]);
            Assert.Equal(HtmlData.LinkHead, tags[3]);
        }
    }
}