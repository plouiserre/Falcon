using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.IdentifyTagParsing
{
    public class IdentifyTagNameTest
    {
        //link, body, p, a
        [Theory]
        [InlineData("<!doctype html>")]
        [InlineData("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">")]
        public void FindTagNameDoctype(string tagStart)
        {
            var identifyTagName = TestFactory.InitIdentifyTagName();

            var name = identifyTagName.FindTagName(tagStart);

            Assert.Equal(NameTagEnum.doctype, name);
        }


        [Fact]
        public void FindTagNameHtml()
        {
            string tagStart = "<html lang=\"en\" dir=\"auto\">";
            var identifyTagName = TestFactory.InitIdentifyTagName();

            var name = identifyTagName.FindTagName(tagStart);

            Assert.Equal(NameTagEnum.html, name);
        }


        [Fact]
        public void FindTagTitleHtml()
        {
            string tagStart = "<title>";
            var identifyTagName = TestFactory.InitIdentifyTagName();

            var name = identifyTagName.FindTagName(tagStart);

            Assert.Equal(NameTagEnum.title, name);
        }


        [Fact]
        public void FindTagHeadHtml()
        {
            string tagStart = "<head>";
            var identifyTagName = TestFactory.InitIdentifyTagName();

            var name = identifyTagName.FindTagName(tagStart);

            Assert.Equal(NameTagEnum.head, name);
        }


        [Fact]
        public void FindTagNameDiv()
        {
            string tagStart = "<div class=\"name\">";
            var identifyTagName = TestFactory.InitIdentifyTagName();

            var name = identifyTagName.FindTagName(tagStart);

            Assert.Equal(NameTagEnum.div, name);
        }


        [Fact]
        public void FindTagNameSpan()
        {
            string tagStart = "<span class=\"important\">";
            var identifyTagName = TestFactory.InitIdentifyTagName();

            var name = identifyTagName.FindTagName(tagStart);

            Assert.Equal(NameTagEnum.span, name);
        }


        [Fact]
        public void FindTagNameMeta()
        {
            string tagStart = "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">";
            var identifyTagName = TestFactory.InitIdentifyTagName();

            var name = identifyTagName.FindTagName(tagStart);

            Assert.Equal(NameTagEnum.meta, name);
        }


        [Fact]
        public void FindTagLinkHtml()
        {
            string tagStart = "<link rel=\"stylesheet\" href=\"main.css\">";
            var identifyTagName = TestFactory.InitIdentifyTagName();

            var name = identifyTagName.FindTagName(tagStart);

            Assert.Equal(NameTagEnum.link, name);
        }




        [Fact]
        public void FindTagNameBody()
        {
            string tagStart = "<body id=\"main\">";
            var identifyTagName = TestFactory.InitIdentifyTagName();

            var name = identifyTagName.FindTagName(tagStart);

            Assert.Equal(NameTagEnum.body, name);
        }


        [Fact]
        public void FindTagNameP()
        {
            string tagStart = "<p class=\"text\">";
            var identifyTagName = TestFactory.InitIdentifyTagName();

            var name = identifyTagName.FindTagName(tagStart);

            Assert.Equal(NameTagEnum.p, name);
        }


        [Fact]
        public void FindTagNameA()
        {
            string tagStart = "<a href=\"declaration.html\">";
            var identifyTagName = TestFactory.InitIdentifyTagName();

            var name = identifyTagName.FindTagName(tagStart);

            Assert.Equal(NameTagEnum.a, name);
        }
    }
}