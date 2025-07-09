using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.Models;

namespace FalconEngineTest.CleanData
{
    public class DeleteUselessSpaceTest
    {
        [Fact]
        public void NoCleanIfItIsOnlyText()
        {
            string html = "<p> Hello bro !!!!</p>";
            var tag = new TagModel()
            {
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            var cleaner = new DeleteUselessSpace(tag);

            string htmlClean = cleaner.CleanContent(html);

            string htmlExpected = " Hello bro !!!!";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void CleanNoSpaceWithTags()
        {
            string html = "<p><span> Hello</span> bro!!!!</p>";
            var tag = new TagModel()
            {
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            var cleaner = new DeleteUselessSpace(tag);

            string htmlClean = cleaner.CleanContent(html);

            string htmlExpected = "<span> Hello</span> bro!!!!";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void CleanOneSpaceWithTags()
        {
            string html = "<p> <span> Hello</span> bro!!!!</p>";
            var tag = new TagModel()
            {
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            var cleaner = new DeleteUselessSpace(tag);

            string htmlClean = cleaner.CleanContent(html);

            string htmlExpected = "<span> Hello</span> bro!!!!";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void CleanManySpaceWithTags()
        {
            string html = "<p>                 <span> Hello</span> bro!!!!</p>";
            var tag = new TagModel()
            {
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            var cleaner = new DeleteUselessSpace(tag);

            string htmlClean = cleaner.CleanContent(html);

            string htmlExpected = "<span> Hello</span> bro!!!!";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void CleanNewLine()
        {
            string html = @"<p>
<span> Hello</span> bro!!!!</p>";
            var tag = new TagModel()
            {
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            var cleaner = new DeleteUselessSpace(tag);

            string htmlClean = cleaner.CleanContent(html);

            string htmlExpected = "<span> Hello</span> bro!!!!";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void CleanMultipleNewLine()
        {
            string html = @"<p>



<span> Hello</span> bro!!!!</p>";
            var tag = new TagModel()
            {
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            var cleaner = new DeleteUselessSpace(tag);

            string htmlClean = cleaner.CleanContent(html);

            string htmlExpected = "<span> Hello</span> bro!!!!";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void CleanManySpaceWithMultipleNewLine()
        {
            string html = @"<p>



                 <span> Hello</span> bro!!!!</p>";
            var tag = new TagModel()
            {
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            var cleaner = new DeleteUselessSpace(tag);

            string htmlClean = cleaner.CleanContent(html);

            string htmlExpected = "<span> Hello</span> bro!!!!";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void CleanTab()
        {
            string html = @"<p> <span> Hello</span> bro!!!!</p>";
            var tag = new TagModel()
            {
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            var cleaner = new DeleteUselessSpace(tag);

            string htmlClean = cleaner.CleanContent(html);

            string htmlExpected = "<span> Hello</span> bro!!!!";
            Assert.Equal(htmlExpected, htmlClean);
        }


        [Fact]
        public void CleanManyTabs()
        {
            string html = @"<p>         <span> Hello</span> bro!!!!</p>";
            var tag = new TagModel()
            {
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            var cleaner = new DeleteUselessSpace(tag);

            string htmlClean = cleaner.CleanContent(html);

            string htmlExpected = "<span> Hello</span> bro!!!!";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void CleanManySpaceWithMultipleNewLineManyTabs()
        {
            string html = @"<p>



                            <span> Hello</span> bro!!!!</p>";
            var tag = new TagModel()
            {
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            var cleaner = new DeleteUselessSpace(tag);

            string htmlClean = cleaner.CleanContent(html);

            string htmlExpected = "<span> Hello</span> bro!!!!";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void CleanManySpaceWithMultipleNewLineManyTabsBeforeAndAfter()
        {
            string html = @"<p>



                            <span> Hello</span> bro!!!!
                                       </p>";
            var tag = new TagModel()
            {
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            var cleaner = new DeleteUselessSpace(tag);

            string htmlClean = cleaner.CleanContent(html);

            string htmlExpected = "<span> Hello</span> bro!!!!";
            Assert.Equal(htmlExpected, htmlClean);
        }


        [Fact]
        public void RemoveSpecialCaracters()
        {
            string html = "<span>\n \r Hello\n \r </span>";

            var cleaner = new DeleteUselessSpace(null);

            string htmlClean = cleaner.RemoveSpecialCaracter(html);

            string htmlExpected = "<span>  Hello  </span>";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void RemoveUselessSpace()
        {
            string htmlNotClean = "                                                    <meta charset=\"UTF-8\">                            <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">                            <title>Document</title>                            <link rel=\"stylesheet\" href=\"main.css\">                        ";

            var cleaner = new DeleteUselessSpace(null);

            string htmlClean = cleaner.RemoveUselessSpace(htmlNotClean);

            string htmlExpected = "<meta charset=\"UTF-8\">                            <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">                            <title>Document</title>                            <link rel=\"stylesheet\" href=\"main.css\">                        ";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void CleanTagStart()
        {
            string html = "<link rel=\"stylesheet\" href=\"main.css\">                     ";

            var cleaner = new DeleteUselessSpace(null);

            string htmlClean = cleaner.CleanTagStart(html);

            string htmlExpected = "<link rel=\"stylesheet\" href=\"main.css\">";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void PurgeUselessCaractersBeforeTag()
        {
            string htmlNotClean = "                           \r\n                         <meta charset=\"UTF-8\">";

            var cleaner = new DeleteUselessSpace(null);

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(htmlNotClean);

            string htmlExpected = "<meta charset=\"UTF-8\">";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void PurgeUselessCaractersAfterTagSimple()
        {
            string htmlNotClean = "<link rel=\"stylesheet\" href=\"main.css\">            \r\n         ";

            var cleaner = new DeleteUselessSpace(null);

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(htmlNotClean);

            string htmlExpected = "<link rel=\"stylesheet\" href=\"main.css\">";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void PurgeUselessCaractersAfterTagComplexe()
        {
            string htmlNotClean = "<head><title>Document</title>\r\n</head>            \r\n         ";

            var cleaner = new DeleteUselessSpace(null);

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(htmlNotClean);

            string htmlExpected = "<head><title>Document</title></head>";
            Assert.Equal(htmlExpected, htmlClean);
        }
    }
}