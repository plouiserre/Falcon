using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class SelectParserTest
    {
        [Fact]
        public void ParseSelectAndValidIt()
        {
            var html = HtmlPageFormData.GetHtml(TagHtmlForm.selectSituation);
            var parser = TestFactory.InitSelectParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.True(isValid);
            AssertFormPage.AssertSelectSituation(tag);
        }

        [Fact]
        public void ParseSelectWithAllOkAttributes()
        {
            var html = "<select autocomplete=\"none\" autofocus form=\"my form\" multiple name=\"test\" required size=\"30\"><option>test 1</option><option>test 2</option><option>test 3</option></select>";
            var parser = TestFactory.InitSelectParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.True(isValid);
            Assert.Equal(NameTagEnum.select, tag.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("<select autocomplete=\"none\" autofocus form=\"my form\" multiple name=\"test\" required size=\"30\">", tag.TagStart);
            Assert.Equal("</select>", tag.TagEnd);
            Assert.Equal("<option>test 1</option><option>test 2</option><option>test 3</option>", tag.Content);
            Assert.Equal("autocomplete", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("none", tag.Attributes[0].Value);
            Assert.Equal("autofocus", tag.Attributes[1].FamilyAttribute);
            Assert.Equal("form", tag.Attributes[2].FamilyAttribute);
            Assert.Equal("my form", tag.Attributes[2].Value);
            Assert.Equal("multiple", tag.Attributes[3].FamilyAttribute);
            Assert.Equal("name", tag.Attributes[4].FamilyAttribute);
            Assert.Equal("test", tag.Attributes[4].Value);
            Assert.Equal("required", tag.Attributes[5].FamilyAttribute);
            Assert.Equal("size", tag.Attributes[6].FamilyAttribute);
            Assert.Equal("30", tag.Attributes[6].Value);
        }

        [Fact]
        public void ParseSelectAndNoValidItBecauseWrongAttributs()
        {
            var html = "<select charset=\"UTF-8\"><option>test 1</option><option>test 2</option><option>test 3</option></select>";
            var parser = TestFactory.InitSelectParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void ParseSelectAndNoValidItBecauseChildHaveWrongAttribut()
        {
            var html = "<select><option alt=\"message secret\">test 1</option><option>test 2</option><option>test 3</option></select>";
            var parser = TestFactory.InitSelectParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }
    }
}