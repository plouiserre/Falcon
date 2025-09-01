using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class TdParserTest
    {
        [Fact]
        public void ParseAndValidateTdOnlyText()
        {
            string html = "<td>Create and ordered features from the wishes of the business </td>";
            var parser = TestFactory.InitTdParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.Equal(NameTagEnum.td, tag.NameTag);
            Assert.Equal("<td>", tag.TagStart);
            Assert.Equal("</td>", tag.TagEnd);
            Assert.Equal("Create and ordered features from the wishes of the business ", tag.Content);
            Assert.Null(tag.Attributes);
            Assert.Null(tag.Children);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseAndValidateTdWithAttributs()
        {
            string html = "<td colspan=\"2\" headers=\"blank\" rowspan=\"6\" hidden class=\"tdhidden\">Technical</td>";
            var parser = TestFactory.InitTdParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.Equal(NameTagEnum.td, tag.NameTag);
            Assert.Equal("<td colspan=\"2\" headers=\"blank\" rowspan=\"6\" hidden class=\"tdhidden\">", tag.TagStart);
            Assert.Equal("</td>", tag.TagEnd);
            Assert.Equal("Technical", tag.Content);
            Assert.Equal(5, tag.Attributes.Count);
            Assert.Equal("colspan", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("2", tag.Attributes[0].Value);
            Assert.Equal("headers", tag.Attributes[1].FamilyAttribute);
            Assert.Equal("blank", tag.Attributes[1].Value);
            Assert.Equal("rowspan", tag.Attributes[2].FamilyAttribute);
            Assert.Equal("6", tag.Attributes[2].Value);
            Assert.Equal("hidden", tag.Attributes[3].FamilyAttribute);
            Assert.Equal("classCss", tag.Attributes[4].FamilyAttribute);
            Assert.Equal("tdhidden", tag.Attributes[4].Value);
            Assert.Null(tag.Children);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseAndNoValidateTdWithAttributs()
        {
            string html = "<td colspan=\"2\" alt=\"you cannot see me\">Technical</td>";
            var parser = TestFactory.InitTdParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.Equal(NameTagEnum.td, tag.NameTag);
            Assert.Equal("<td colspan=\"2\" alt=\"you cannot see me\">", tag.TagStart);
            Assert.Equal("</td>", tag.TagEnd);
            Assert.Equal("Technical", tag.Content);
            Assert.Equal(2, tag.Attributes.Count);
            Assert.Equal("colspan", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("2", tag.Attributes[0].Value);
            Assert.Equal("alt", tag.Attributes[1].FamilyAttribute);
            Assert.Equal("you cannot see me", tag.Attributes[1].Value);
            Assert.Null(tag.Children);
            Assert.False(isValid);
        }
    }
}