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
    }
}