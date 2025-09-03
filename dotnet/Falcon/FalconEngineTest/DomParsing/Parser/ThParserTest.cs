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

        //TODO faire un test KO à cause des valeurs bidons de scope

        //TODO faire un test où je valide les valeurs de scope et je ne les valide pas

        //TODO faire un test KO à cause d'un attribut bidon
    }
}