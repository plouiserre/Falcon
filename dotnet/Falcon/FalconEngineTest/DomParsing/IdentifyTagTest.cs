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

            Assert.Equal("<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">", identifyTag.TagStart);
            Assert.Equal("</html>", identifyTag.TagEnd);
        }

        //TODO add a case when you have more than one space between attributes
    }
}