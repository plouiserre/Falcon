using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class LabelParserTest
    {
        [Fact]
        public void LabelDateParser()
        {
            string html = HtmlPageFormData.GetHtml(TagHtmlForm.labelDate);
            var labelParse = TestFactory.InitLabelParser();

            var tag = labelParse.Parse(html);
            bool isValid = labelParse.IsValid();

            Assert.Equal("forAttr", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("dBirthday", tag.Attributes[0].Value);
            Assert.Equal(NameTagEnum.label, tag.NameTag);
            Assert.Equal("Birthday", tag.Content);
            Assert.Null(tag.Children);
            Assert.True(isValid);
        }


        [Fact]
        public void LabelAllAttributesParseAndValidateIt()
        {
            string html = "<label id=\"label\" for=\"dBirthday\" form=\"Big Future Form\">Birthday</label>";
            var labelParse = TestFactory.InitLabelParser();

            var tag = labelParse.Parse(html);
            bool isValid = labelParse.IsValid();

            Assert.Equal("id", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("label", tag.Attributes[0].Value);
            Assert.Equal("forAttr", tag.Attributes[1].FamilyAttribute);
            Assert.Equal("dBirthday", tag.Attributes[1].Value);
            Assert.Equal("form", tag.Attributes[2].FamilyAttribute);
            Assert.Equal("Big Future Form", tag.Attributes[2].Value);
            Assert.Equal(NameTagEnum.label, tag.NameTag);
            Assert.Equal("Birthday", tag.Content);
            Assert.Null(tag.Children);
            Assert.True(isValid);
        }


        [Fact]
        public void LabelAllAttributesParseAndNotValidateIt()
        {
            string html = "<label id=\"label\" for=\"dBirthday\" alt=\"Big Future Form\">Birthday</label>";
            var labelParse = TestFactory.InitLabelParser();

            var tag = labelParse.Parse(html);
            bool isValid = labelParse.IsValid();

            Assert.Equal("id", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("label", tag.Attributes[0].Value);
            Assert.Equal("forAttr", tag.Attributes[1].FamilyAttribute);
            Assert.Equal("dBirthday", tag.Attributes[1].Value);
            Assert.Equal("alt", tag.Attributes[2].FamilyAttribute);
            Assert.Equal("Big Future Form", tag.Attributes[2].Value);
            Assert.Equal(NameTagEnum.label, tag.NameTag);
            Assert.Equal("Birthday", tag.Content);
            Assert.Null(tag.Children);
            Assert.False(isValid);
        }
    }
}