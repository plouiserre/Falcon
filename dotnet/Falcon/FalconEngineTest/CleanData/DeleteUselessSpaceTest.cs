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

            string htmlClean = cleaner.Clean(html);

            Assert.Equal(html, htmlClean);
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

            string htmlClean = cleaner.Clean(html);

            Assert.Equal(html, htmlClean);
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

            string htmlClean = cleaner.Clean(html);

            string htmlExpected = "<p><span> Hello</span> bro!!!!</p>";
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

            string htmlClean = cleaner.Clean(html);

            string htmlExpected = "<p><span> Hello</span> bro!!!!</p>";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void CleanManySpaceWithNewLine()
        {
            string html = @"<p>
<span> Hello</span> bro!!!!</p>";
            var tag = new TagModel()
            {
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            var cleaner = new DeleteUselessSpace(tag);

            string htmlClean = cleaner.Clean(html);

            string htmlExpected = "<p><span> Hello</span> bro!!!!</p>";
            Assert.Equal(htmlExpected, htmlClean);
        }
    }
}