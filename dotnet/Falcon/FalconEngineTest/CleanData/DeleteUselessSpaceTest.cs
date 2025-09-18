using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.CleanData
{
    public class DeleteUselessSpaceTest
    {
        [Fact]
        public void PurgeUselessCaractersBeforeTag()
        {
            string htmlNotClean = "                           \r\n                         <meta charset=\"UTF-8\">";

            var cleaner = TestFactory.InitDeleteUselessSpace();

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(htmlNotClean);

            string htmlExpected = "<meta charset=\"UTF-8\">";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void PurgeUselessCaractersAfterTagSimple()
        {
            string htmlNotClean = "<link rel=\"stylesheet\" href=\"main.css\">            \r\n         ";

            var cleaner = TestFactory.InitDeleteUselessSpace();

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(htmlNotClean);

            string htmlExpected = "<link rel=\"stylesheet\" href=\"main.css\">";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void PurgeUselessCaractersInsideTagStartOnlyWithOneWord()
        {
            string htmlNotClean = "<link     rel=\"stylesheet\"   href=\"main.css\">            \r\n         ";

            var cleaner = TestFactory.InitDeleteUselessSpace();

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(htmlNotClean);

            string htmlExpected = "<link rel=\"stylesheet\" href=\"main.css\">";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void PurgeUselessCaractersWithAttributesWithMoreThanOneSpaceInsideTagStartOnlyWithOneWord()
        {
            string htmlNotClean = "<a   title=\"Visiter le site\"    href=\"monsite.html\"> Mon site </a>";

            var cleaner = TestFactory.InitDeleteUselessSpace();

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(htmlNotClean);

            string htmlExpected = "<a title=\"Visiter le site\" href=\"monsite.html\"> Mon site </a>";
            Assert.Equal(htmlExpected, htmlClean);
        }


        [Fact]
        public void PurgeUselessCaractersWithAttributesWithOneAttributeWithoutProperty()
        {
            string htmlNotClean = "<link rel=\"stylesheet\"  href=\"alt-theme.css\" type=\"text/css\" title=\"Thème alternatif\" disabled media=\"screen\">";

            var cleaner = TestFactory.InitDeleteUselessSpace();

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(htmlNotClean);

            string htmlExpected = "<link rel=\"stylesheet\" href=\"alt-theme.css\" type=\"text/css\" title=\"Thème alternatif\" disabled media=\"screen\">";
            Assert.Equal(htmlExpected, htmlClean);
        }


        [Fact]
        public void NoPurgeUselessCaractersTagStartDoctype()
        {
            string htmlNotClean = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Frameset//EN\" \"http://www.w3.org/TR/html4/frameset.dtd\">";

            var cleaner = TestFactory.InitDeleteUselessSpace();

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(htmlNotClean);

            string htmlExpected = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Frameset//EN\" \"http://www.w3.org/TR/html4/frameset.dtd\">";
            Assert.Equal(htmlExpected, htmlClean);
        }



        [Fact]
        public void PurgeUselessCaractersAfterTagComplexe()
        {
            string htmlNotClean = "<head><title>Document</title>\r\n</head>            \r\n         ";

            var cleaner = TestFactory.InitDeleteUselessSpace();

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(htmlNotClean);

            string htmlExpected = "<head><title>Document</title></head>";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void PurgeUselessCaractersAfterDoubleDiv()
        {
            string html = "<div class=\"main\"><div class=\"second\"><p>Hello World!!!</p></div></div>            \r\n         ";
            var cleaner = TestFactory.InitDeleteUselessSpace();

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(html);

            string htmlExpected = "<div class=\"main\"><div class=\"second\"><p>Hello World!!!</p></div></div>";
            Assert.Equal(htmlExpected, htmlClean);

        }
    }
}