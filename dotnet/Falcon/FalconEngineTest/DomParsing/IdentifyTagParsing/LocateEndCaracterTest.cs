using FalconEngineTest.Utils;
using FalconEngineTest.Utils.HtmlData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconEngineTest.DomParsing.IdentifyTagParsing
{
    public class LocateEndCaracterTest
    {
        [Fact]
        public void LocateEndCaracterSimpleHtml()
        {
            string html = "<div>Hello World</div>";
            var locateEndCaracter = TestFactory.InitLocateEndCaracter();

            int? place = locateEndCaracter.Search("<div>", html);

            Assert.Equal(16, place);
        }


        [Fact]
        public void LocateEndCaractSimpleHtmlAndStartTagHaveAttributs()
        {
            string html = "<div class=\"greetings\">Hello World</div>";
            var locateEndCaracter = TestFactory.InitLocateEndCaracter();

            int? place = locateEndCaracter.Search("<div class=\"greetings\">", html);

            Assert.Equal(34, place);
        }


        [Fact]
        public void LocateEndCaracterDoubleDivWithAttributsHtml()
        {
            string html = "<div class=\"greetings\"><div class=\"doubleDiv\"> Hello World</div></div>";
            var locateEndCaracter = TestFactory.InitLocateEndCaracter();

            int? place = locateEndCaracter.Search("<div class=\"greetings\">", html);

            Assert.Equal(64, place);
        }


        //noendTag
        [Fact]
        public void LocateEndCaracterWithNoEndTag()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.metaCharset);
            var locateEndCaracter = TestFactory.InitLocateEndCaracter();

            int? place = locateEndCaracter.Search(html, html);

            Assert.Null(place);
        }
    }
}
