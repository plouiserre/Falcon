using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class ThParserTest
    {
        [Fact]
        public void ParseAndValidateTh()
        {
            string html = "<th scope=\"col\">Title</th>";
            var thParser = TestFactory.InitThParser();

            var tag = thParser.Parse(html);
            var isValid = thParser.IsValid();

            Assert.Equal(NameTagEnum.th, tag.NameTag);
            Assert.Equal("<th scope=\"col\">", tag.TagStart);
            Assert.Equal("</th>", tag.TagEnd);
            Assert.Equal("Title", tag.Content);
            Assert.Equal("scope", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("col", tag.Attributes[0].Value);
            Assert.Null(tag.Children);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseAndValidateWithAllAttributesTh()
        {
            string html = "<th abbr=\"head first column\" colspan=\"2\" headers=\"blank\" rowspan=\"3\">Title</th>";
            var thParser = TestFactory.InitThParser();

            var tag = thParser.Parse(html);
            var isValid = thParser.IsValid();

            Assert.Equal(NameTagEnum.th, tag.NameTag);
            Assert.Equal("<th abbr=\"head first column\" colspan=\"2\" headers=\"blank\" rowspan=\"3\">", tag.TagStart);
            Assert.Equal("</th>", tag.TagEnd);
            Assert.Equal("Title", tag.Content);
            Assert.Equal(4, tag.Attributes.Count);
            Assert.Equal("abbr", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("head first column", tag.Attributes[0].Value);
            Assert.Equal("colspan", tag.Attributes[1].FamilyAttribute);
            Assert.Equal("2", tag.Attributes[1].Value);
            Assert.Equal("headers", tag.Attributes[2].FamilyAttribute);
            Assert.Equal("blank", tag.Attributes[2].Value);
            Assert.Equal("rowspan", tag.Attributes[3].FamilyAttribute);
            Assert.Equal("3", tag.Attributes[3].Value);
            Assert.Null(tag.Children);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseAndNoValidateThBecauseScopeHaveWrongValue()
        {
            string html = "<th scope=\"table\">Title</th>";
            var thParser = TestFactory.InitThParser();

            var tag = thParser.Parse(html);
            var isValid = thParser.IsValid();

            Assert.Equal(NameTagEnum.th, tag.NameTag);
            Assert.Equal("<th scope=\"table\">", tag.TagStart);
            Assert.Equal("</th>", tag.TagEnd);
            Assert.Equal("Title", tag.Content);
            Assert.Equal("scope", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("table", tag.Attributes[0].Value);
            Assert.Null(tag.Children);
            Assert.False(isValid);
        }

        [Fact]
        public void ValidateOrNotThTagWithDifferentsScopeValue()
        {
            string[] goodTags = new string[]{"<th scope=\"row\">Title</th>", "<th scope=\"col\">Title</th>", "<th scope=\"rowgroup\">Title</th>",
                    "<th scope=\"colgroup\">Title</th>"};
            foreach (var html in goodTags)
            {
                var thParser = TestFactory.InitThParser();
                var tag = thParser.Parse(html);
                var isValid = thParser.IsValid();
                Assert.True(isValid);
            }
            string[] badTags = new string[] { "<th scope=\"test\">Title</th>", "<th scope=\"azef\">Title</th>", "<th scope=\"2332d\">Title</th>" };
            foreach (var html in badTags)
            {
                var thParser = TestFactory.InitThParser();
                var tag = thParser.Parse(html);
                var isValid = thParser.IsValid();
                Assert.False(isValid);
            }
        }

        [Fact]
        public void ParseAndNoValidateThBecauseWrongAttributs()
        {
            string html = "<th scope=\"col\" alt=\"head column\">Title</th>";
            var thParser = TestFactory.InitThParser();

            var tag = thParser.Parse(html);
            var isValid = thParser.IsValid();

            Assert.Equal(NameTagEnum.th, tag.NameTag);
            Assert.Equal("<th scope=\"col\" alt=\"head column\">", tag.TagStart);
            Assert.Equal("</th>", tag.TagEnd);
            Assert.Equal("Title", tag.Content);
            Assert.Equal("scope", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("col", tag.Attributes[0].Value);
            Assert.Equal("alt", tag.Attributes[1].FamilyAttribute);
            Assert.Equal("head column", tag.Attributes[1].Value);
            Assert.Null(tag.Children);
            Assert.False(isValid);
        }
    }
}