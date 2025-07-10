using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing
{
    public class IdentifyTagTest
    {
        [Fact]
        public void IdentifyClairlyHtmlTagElement()
        {
            string html = HtmlData.HtmlSimpleWithSpace;
            var identifyTag = new IdentifyTag();

            var tag = identifyTag.Analyze(html);

            Assert.Equal("<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">", tag.TagStart);
            Assert.Equal("</html>", tag.TagEnd);
        }


        [Fact]
        public void IdentifyClairlyDoctypeTagElement()
        {
            string html = "<!DOCTYPE html>";
            var identifyTag = new IdentifyTag();

            var tag = identifyTag.Analyze(html);

            Assert.Equal("<!DOCTYPE html>", tag.TagStart);
            Assert.Null(tag.TagEnd);
        }
    }
}