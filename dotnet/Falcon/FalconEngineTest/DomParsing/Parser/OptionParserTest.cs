using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class OptionParserTest
    {
        [Theory]
        [InlineData("No Job")]
        [InlineData("Job in a company")]
        [InlineData("Entrepreneur")]
        public void ParseFirstOption(string content)
        {
            var html = string.Concat("<option>", content, "</option>");

            var parser = TestFactory.InitOptionParser();

            var tag = parser.Parse(html);

            Assert.Equal("<option>", tag.TagStart);
            Assert.Equal("</option>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal(NameTagEnum.option, tag.NameTag);
            Assert.Equal(content, tag.Content);
            Assert.Null(tag.Attributes);
            Assert.Null(tag.Children);
        }
    }
}