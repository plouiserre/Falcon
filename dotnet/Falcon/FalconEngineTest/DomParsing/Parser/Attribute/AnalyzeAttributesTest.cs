using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser.Attribute;

namespace FalconEngineTest.DomParsing.Parser.Attribute
{
    public class AnalyzeAttributesTest
    {
        [Fact]
        public void IdentifyPartCorrectlySimpleStartTag()
        {
            string html = "<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">";

            var analyze = new AnalyzeAttributes();

            var parts = analyze.Study(html);

            Assert.Equal(3, parts.Count);
            Assert.Equal("lang=\"en\"", parts[0]);
            Assert.Equal("dir=\"auto\"", parts[1]);
            Assert.Equal("xmlns=\"http://www.w3.org/1999/xhtml\"", parts[2]);
        }

        [Fact]
        public void IdentifyPartCorrecltyComplexeStartTag()
        {
            string html = "<link rel=\"manifest\" href=\" / site.webmanifest\" type=\"application / manifest + json\" crossorigin=\"use - credentials\" referrerpolicy=\"origin - when - cross - origin\" id=\"web - app - manifest\">";

            var analyze = new AnalyzeAttributes();

            var parts = analyze.Study(html);

            Assert.Equal(6, parts.Count);
            Assert.Equal("rel=\"manifest\"", parts[0]);
            Assert.Equal("href=\" / site.webmanifest\"", parts[1]);
            Assert.Equal("type=\"application / manifest + json\"", parts[2]);
            Assert.Equal("crossorigin=\"use - credentials\"", parts[3]);
            Assert.Equal("referrerpolicy=\"origin - when - cross - origin\"", parts[4]);
            Assert.Equal("id=\"web - app - manifest\"", parts[5]);
        }

        [Fact]
        public void IdentifyPartCorrecltyComplexeWithOneAttributeStartTag()
        {
            string html = "<link rel=\"stylesheet\" href=\"alt-theme.css\" type=\"text/css\" title=\"Thème alternatif\" disabled media=\"screen\">";

            var analyze = new AnalyzeAttributes();

            var parts = analyze.Study(html);

            Assert.Equal(6, parts.Count);
            Assert.Equal("rel=\"stylesheet\"", parts[0]);
            Assert.Equal("href=\"alt-theme.css\"", parts[1]);
            Assert.Equal("type=\"text/css\"", parts[2]);
            Assert.Equal("title=\"Thème alternatif\"", parts[3]);
            Assert.Equal("disabled", parts[4]);
            Assert.Equal("media=\"screen\"", parts[5]);
        }


    }
}