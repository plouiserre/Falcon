using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Configuration;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class FormParserTest
    {
        [Fact]
        public void ParseFormAndValidateIt()
        {
            string? html = HtmlPageFormData.GetHtml(TagHtmlForm.form);
            var formParser = TestFactory.InitFormParser();

            var tag = formParser.Parse(html);
            bool isValid = formParser.IsValid();

            string startTag = "<form method=\"POST\" action=\"/candidate\">";
            string endTag = "</form>";
            string content = html.Replace(startTag, string.Empty).Replace(endTag, string.Empty);
            AssertFormPage.AssertForm(tag, content);
            Assert.True(isValid);
        }

        //non valide
        [Fact]
        public void ParseFormAndNoValidateIt()
        {
            string? html = "<form charset=\"UTF-8\">Big beautiful form</form>";
            var formParser = TestFactory.InitFormParser();

            var tag = formParser.Parse(html);
            bool isValid = formParser.IsValid();

            Assert.Equal(NameTagEnum.form, tag.NameTag);
            Assert.Equal("<form charset=\"UTF-8\">", tag.TagStart);
            Assert.Equal("</form>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("charset", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("UTF-8", tag.Attributes[0].Value);
            Assert.Equal("Big beautiful form", tag.Content);
            Assert.Null(tag.Children);
            Assert.False(isValid);
        }

        //non valide enfant
        [Fact]
        public void ParseFormAndNoValidateItBecauseChildNoValid()
        {
            string? html = "<form><h1 charset=\"UTF-8\">Big beautiful form</h1></form>";
            var formParser = TestFactory.InitFormParser();

            var tag = formParser.Parse(html);
            bool isValid = formParser.IsValid();

            Assert.Equal(NameTagEnum.form, tag.NameTag);
            Assert.Equal("<form>", tag.TagStart);
            Assert.Equal("</form>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("<h1 charset=\"UTF-8\">Big beautiful form</h1>", tag.Content);
            Assert.Equal("charset", tag.Children[0].Attributes[0].FamilyAttribute);
            Assert.Equal("UTF-8", tag.Children[0].Attributes[0].Value);
            Assert.Equal("Big beautiful form", tag.Children[0].Content);
            Assert.Null(tag.Children[0].Children);
            Assert.False(isValid);
        }
    }
}