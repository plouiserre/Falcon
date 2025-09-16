using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.HtmlData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconEngineTest.DomParsing.IdentifyTagParsing
{
    public class LocateLimitTagTest
    {
        [Fact]
        public void LocateEndTagSimpleHtml()
        {
            string html = "<div>Hello World</div>";
            var LocateEndTag = TestFactory.InitLocateEndTag();

            int? place = LocateEndTag.Search(LimitMode.End, "<div>", html);

            Assert.Equal(16, place);
        }


        [Fact]
        public void LocateEndCaractSimpleHtmlAndStartTagHaveAttributs()
        {
            string html = "<div class=\"greetings\">Hello World</div>";
            var LocateEndTag = TestFactory.InitLocateEndTag();

            int? place = LocateEndTag.Search(LimitMode.End, "<div class=\"greetings\">", html);

            Assert.Equal(34, place);
        }


        [Fact]
        public void LocateEndTagDoubleDivWithAttributsHtml()
        {
            string html = "<div class=\"greetings\"><div class=\"doubleDiv\"> Hello World</div></div>";
            var LocateEndTag = TestFactory.InitLocateEndTag();

            int? place = LocateEndTag.Search(LimitMode.End, "<div class=\"greetings\">", html);

            Assert.Equal(64, place);
        }

        [Fact]
        public void LocateEndTagWithNoEndTag()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.metaCharset);
            var LocateEndTag = TestFactory.InitLocateEndTag();

            int? place = LocateEndTag.Search(LimitMode.End, html, html);

            Assert.Null(place);
        }

        [Fact]
        public void LocateStartDoubleDivWithSpace()
        {
            string html = "   <div class=\"greetings\"><div class=\"doubleDiv\"> Hello World</div></div>";
            var LocateEndTag = TestFactory.InitLocateEndTag();

            int? place = LocateEndTag.Search(LimitMode.Start, "<div class=\"greetings\">", html);

            Assert.Equal(3, place);
        }


        [Fact]
        public void LocateStartDoubleDivWithNewLine()
        {
            string html = " \n \r  <div class=\"greetings\"><div class=\"doubleDiv\"> Hello World</div></div>";
            var LocateEndTag = TestFactory.InitLocateEndTag();

            int? place = LocateEndTag.Search(LimitMode.Start, "<div class=\"greetings\">", html);

            Assert.Equal(6, place);
        }
    }
}
