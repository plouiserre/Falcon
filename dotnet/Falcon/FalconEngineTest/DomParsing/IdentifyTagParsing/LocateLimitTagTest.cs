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


        [Fact]
        public void LocateEndTagWithTwPoFollowsSameTags()
        {
            string html = "<p class=\"declarationText\"> Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span><span class=\"red\">Et il raconte des supers trucs!!!</span></p><p>Allez-vous apprécier mon article?</p>";
            var LocateEndTag = TestFactory.InitLocateEndTag();

            int? place = LocateEndTag.Search(LimitMode.End, "<p class=\"declarationText\">", html);

            Assert.Equal(152, place);
        }

        [Fact]
        public void LocateEndTagWithTwoLiFollowsSameTags()
        {
            string html = "<li>Home</li><li>News</li><li>New organisation</li>";
            var LocateEndTag = TestFactory.InitLocateEndTag();

            int? place = LocateEndTag.Search(LimitMode.End, "<li>", html);

            Assert.Equal(8, place);
        }

        [Fact]
        public void CountNumberStartTagSimple()
        {
            string html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.pDeclarationText);
            var LocateEndTag = TestFactory.InitLocateEndTag();

            LocateEndTag.Search(LimitMode.End, "<p class=\"declarationText\">", html);
            bool occurenceMultiple = LocateEndTag.IndicateIsMultipleStartTag();

            Assert.False(occurenceMultiple);
        }

        [Fact]
        public void CountNumberStartTagDouble()
        {
            string html = "<div class=\"greetings\"><div class=\"doubleDiv\"> Hello World</div></div>";
            var LocateEndTag = TestFactory.InitLocateEndTag();

            LocateEndTag.Search(LimitMode.End, "<div class=\"greetings\">", html);
            bool occurenceMultiple = LocateEndTag.IndicateIsMultipleStartTag();

            Assert.True(occurenceMultiple);
        }
    }
}
