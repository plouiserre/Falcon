using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}