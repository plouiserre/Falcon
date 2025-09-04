using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser.Table
{
    public class TheadParserTest
    {
        [Fact]
        public void ParseTheadValidateIt()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.thead);
            var theadParser = TestFactory.InitTheadParser();

            var tag = theadParser.Parse(html);
            bool isValid = theadParser.IsValid();

            AssertTablePage.AssertThead(tag);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseTheadAndValidateItWithAttributes()
        {
            string html = "<thead id=\"head\"><tr><td>Hello</td><td>World</td></tr></thead>";
            var theadParser = TestFactory.InitTheadParser();

            var tag = theadParser.Parse(html);
            var childOne = tag.Children[0];
            var grandChildOne = tag.Children[0].Children[0];
            var grandChildTwo = tag.Children[0].Children[1];
            bool isValid = theadParser.IsValid();

            Assert.Equal(NameTagEnum.thead, tag.NameTag);
            Assert.Equal("<thead id=\"head\">", tag.TagStart);
            Assert.Equal("</thead>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("<tr><td>Hello</td><td>World</td></tr>", tag.Content);
            Assert.Equal("id", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("head", tag.Attributes[0].Value);
            Assert.Equal(NameTagEnum.tr, childOne.NameTag);
            Assert.Equal("<tr>", childOne.TagStart);
            Assert.Equal("</tr>", childOne.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, childOne.TagFamily);
            Assert.Equal("<td>Hello</td><td>World</td>", childOne.Content);
            Assert.Null(childOne.Attributes);
            AssertGrandChild(grandChildOne, "Hello");
            AssertGrandChild(grandChildTwo, "World");
            Assert.True(isValid);
        }

        private void AssertGrandChild(TagModel tag, string label)
        {
            Assert.Equal(NameTagEnum.td, tag.NameTag);
            Assert.Equal("<td>", tag.TagStart);
            Assert.Equal("</td>", tag.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal(label, tag.Content);
            Assert.Null(tag.Attributes);
        }

        [Fact]
        public void ParseTheadAndValidateItNoBecauseChildrenNotValid()
        {
            string html = "<thead id=\"head\"><tr alt=\"some description\"><td>Hello</td><td>World</td></tr></thead>";
            var theadParser = TestFactory.InitTheadParser();

            theadParser.Parse(html);
            bool isValid = theadParser.IsValid();

            Assert.False(isValid);
        }
    }
}