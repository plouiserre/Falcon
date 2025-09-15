using FalconEngineTest.Utils;
using FalconEngineTest.Utils.HtmlData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconEngineTest.DomParsing.IdentifyTagParsing
{
    public class LocateEndTagTest
    {
        [Fact]
        public void LocateEndTagSimpleHtml()
        {
            string html = "<div>Hello World</div>";
            var LocateEndTag = TestFactory.InitLocateEndTag();

            int? place = LocateEndTag.Search("<div>", html);

            Assert.Equal(16, place);
        }


        [Fact]
        public void LocateEndCaractSimpleHtmlAndStartTagHaveAttributs()
        {
            string html = "<div class=\"greetings\">Hello World</div>";
            var LocateEndTag = TestFactory.InitLocateEndTag();

            int? place = LocateEndTag.Search("<div class=\"greetings\">", html);

            Assert.Equal(34, place);
        }


        [Fact]
        public void LocateEndTagDoubleDivWithAttributsHtml()
        {
            string html = "<div class=\"greetings\"><div class=\"doubleDiv\"> Hello World</div></div>";
            var LocateEndTag = TestFactory.InitLocateEndTag();

            int? place = LocateEndTag.Search("<div class=\"greetings\">", html);

            Assert.Equal(64, place);
        }


        //noendTag
        [Fact]
        public void LocateEndTagWithNoEndTag()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.metaCharset);
            var LocateEndTag = TestFactory.InitLocateEndTag();

            int? place = LocateEndTag.Search(html, html);

            Assert.Null(place);
        }
    }
}
