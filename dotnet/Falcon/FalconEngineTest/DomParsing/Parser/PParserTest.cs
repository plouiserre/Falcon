using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class PParserTest
    {
        [Fact]
        public void ValidateParagraph()
        {
            var html = "<p class=\"blueText\">A simple paragraph</p>";

            var parser = TestFactory.InitPParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.True(isValid);
            Assert.Equal("<p class=\"blueText\">", tag.TagStart);
            Assert.Equal("</p>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal(NameTagEnum.p, tag.NameTag);
            Assert.Equal("A simple paragraph", tag.Content);
            Assert.Single(tag.Attributes);
            Assert.Equal("classCss", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("blueText", tag.Attributes[0].Value);
            Assert.Empty(tag.Children);
        }

        //validate with children

        //validate with space

        //no validate
    }
}