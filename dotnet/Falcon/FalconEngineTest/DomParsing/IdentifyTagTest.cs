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

            identifyTag.Analyze(html);

            Assert.Equal("<html lang=\"en\" dir=\"auto\">", identifyTag.TagStart);
            Assert.Equal("</html>", identifyTag.TagEnd);
        }
    }
}