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
        public void ParseFirstOptionAndValidate(string content)
        {
            var html = string.Concat("<option>", content, "</option>");

            var parser = TestFactory.InitOptionParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.True(isValid);
            Assert.Equal("<option>", tag.TagStart);
            Assert.Equal("</option>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal(NameTagEnum.option, tag.NameTag);
            Assert.Equal(content, tag.Content);
            Assert.Null(tag.Attributes);
            Assert.Null(tag.Children);
        }

        [Fact]
        public void ParseFirstOptionAndNoValidate()
        {
            var html = "<option>No Job";

            var parser = TestFactory.InitOptionParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.Equal("<option>", tag.TagStart);
            Assert.Null(tag.Attributes);
            Assert.Null(tag.Children);
            Assert.False(isValid);
        }

        [Fact]
        public void ParseOptionWithHisAttributesAndValidate()
        {
            string content = "Job in a company";
            var html = string.Concat("<option disabled label=\"Job in a company\" selected value=\"2\">", content, "</option>");

            var parser = TestFactory.InitOptionParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.True(isValid);
            Assert.Equal("<option disabled label=\"Job in a company\" selected value=\"2\">", tag.TagStart);
            Assert.Equal("</option>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal(NameTagEnum.option, tag.NameTag);
            Assert.Equal(content, tag.Content);
            Assert.Equal(4, tag.Attributes.Count);
            Assert.Equal("disabled", tag.Attributes[0].FamilyAttribute.ToString());
            Assert.Null(tag.Attributes[0].Value);
            Assert.Equal("label", tag.Attributes[1].FamilyAttribute.ToString());
            Assert.Equal("Job in a company", tag.Attributes[1].Value);
            Assert.Equal("selected", tag.Attributes[2].FamilyAttribute.ToString());
            Assert.Null(tag.Attributes[2].Value);
            Assert.Equal("value", tag.Attributes[3].FamilyAttribute.ToString());
            Assert.Equal("2", tag.Attributes[3].Value);
            Assert.Null(tag.Children);
        }

        [Fact]
        public void ParseOptionWithHisAttributesAndNoValidate()
        {
            string content = "Job in a company";
            var html = string.Concat("<option disabled label=\"Job in a company\" selected value=\"2\" alt=\"message secret\">", content, "</option>");

            var parser = TestFactory.InitOptionParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }
    }
}