using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class H1ParserTest
    {
        [Fact]
        public void ParseH1SimpleAndValidateIt()
        {
            string html = HtmlPageFormData.GetHtml(TagHtmlForm.h1Title);
            var h1Parse = TestFactory.InitH1Parser();

            var tag = h1Parse.Parse(html);
            bool isValid = h1Parse.IsValid();

            AssertFormPage.AssertH1Title(tag);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseH1WithChildrenAndValidateIt()
        {
            string html = "<h1 class=\"subForm\"><span>Sub Form</span></h1>";
            var h1Parse = TestFactory.InitH1Parser();

            var tag = h1Parse.Parse(html);
            bool isValid = h1Parse.IsValid();

            Assert.True(isValid);
            Assert.Equal(NameTagEnum.h1, tag.NameTag);
            Assert.Equal("<span>Sub Form</span>", tag.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("<h1 class=\"subForm\">", tag.TagStart);
            Assert.Equal("</h1>", tag.TagEnd);
            Assert.Equal("classCss", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("subForm", tag.Attributes[0].Value);
            Assert.Single(tag.Children);
            Assert.Equal(NameTagEnum.span, tag.Children[0].NameTag);
            Assert.Equal("Sub Form", tag.Children[0].Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.Children[0].TagFamily);
            Assert.Null(tag.Children[0].Attributes);
            Assert.Null(tag.Children[0].Children);
        }

        //Avoir un h1 non valide 
        [Fact]
        public void ParseH1AndNotValidateIt()
        {
            string html = "<h1 alt=\"Text of form\">Sub Form</h1>";
            var h1Parse = TestFactory.InitH1Parser();

            var tag = h1Parse.Parse(html);
            bool isValid = h1Parse.IsValid();

            Assert.False(isValid);
            Assert.Equal(NameTagEnum.h1, tag.NameTag);
            Assert.Equal("<h1 alt=\"Text of form\">", tag.TagStart);
            Assert.Equal("</h1>", tag.TagEnd);
            Assert.Equal("Sub Form", tag.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
        }



        [Fact]
        public void ParseH1WithChildrenAndNotValidateIt()
        {
            string html = "<h1 class=\"subForm\"><span alt=\"Text of form\">Sub Form</span></h1>";
            var h1Parse = TestFactory.InitH1Parser();

            var tag = h1Parse.Parse(html);
            bool isValid = h1Parse.IsValid();

            Assert.False(isValid);
            Assert.Equal(NameTagEnum.h1, tag.NameTag);
            Assert.Equal("<span alt=\"Text of form\">Sub Form</span>", tag.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("<h1 class=\"subForm\">", tag.TagStart);
            Assert.Equal("</h1>", tag.TagEnd);
            Assert.Equal("classCss", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("subForm", tag.Attributes[0].Value);
            Assert.Single(tag.Children);
            Assert.Equal(NameTagEnum.span, tag.Children[0].NameTag);
            Assert.Equal("Sub Form", tag.Children[0].Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.Children[0].TagFamily);
            Assert.Equal("alt", tag.Children[0].Attributes[0].FamilyAttribute);
            Assert.Equal("Text of form", tag.Children[0].Attributes[0].Value);
            Assert.Null(tag.Children[0].Children);
        }
    }
}