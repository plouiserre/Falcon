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
    public class ScriptParserTest
    {
        [Fact]
        public void ScriptParseAndValidate()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.scriptTable);
            var parser = TestFactory.InitScriptParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            AssertTablePage.AssertScript(tag);
            Assert.True(isValid);
        }


        [Fact]
        public void ScriptParseWithAllAttributsAndValidate()
        {
            string html = "<script async=\"true\" crossorigin=\"anonymous\" defer=\"false\" integrity=\"filehash\" nomodume=\"false\" nonce=\"hash\" referrerpolicy=\"origin\" src=\"script.js\" type=\"script\"></script>";
            var parser = TestFactory.InitScriptParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.Equal(NameTagEnum.script, tag.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("<script async=\"true\" crossorigin=\"anonymous\" defer=\"false\" integrity=\"filehash\" nomodume=\"false\" nonce=\"hash\" referrerpolicy=\"origin\" src=\"script.js\" type=\"script\">", tag.TagStart);
            Assert.Equal("</script>", tag.TagEnd);
            Assert.Empty(tag.Content);
            Assert.Equal("async", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("true", tag.Attributes[0].Value);
            Assert.Equal("crossorigin", tag.Attributes[1].FamilyAttribute);
            Assert.Equal("anonymous", tag.Attributes[1].Value);
            Assert.Equal("defer", tag.Attributes[2].FamilyAttribute);
            Assert.Equal("false", tag.Attributes[2].Value);
            Assert.Equal("integrity", tag.Attributes[3].FamilyAttribute);
            Assert.Equal("filehash", tag.Attributes[3].Value);
            Assert.Equal("nomodume", tag.Attributes[4].FamilyAttribute);
            Assert.Equal("false", tag.Attributes[4].Value);
            Assert.Equal("nonce", tag.Attributes[5].FamilyAttribute);
            Assert.Equal("hash", tag.Attributes[5].Value);
            Assert.Equal("referrerpolicy", tag.Attributes[6].FamilyAttribute);
            Assert.Equal("origin", tag.Attributes[6].Value);
            Assert.Equal("src", tag.Attributes[7].FamilyAttribute);
            Assert.Equal("script.js", tag.Attributes[7].Value);
            Assert.Equal("type", tag.Attributes[8].FamilyAttribute);
            Assert.Equal("script", tag.Attributes[8].Value);
            Assert.True(isValid);
        }

        [Fact]
        public void ScriptParseAndNoValidateItBecauseReffererpolicyHaveBadValue()
        {
            string html = "<script referrerpolicy=\"nothing\"></script>";
            var parser = TestFactory.InitScriptParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void ScriptParseAndValidateItBecauseReffererpolicyHaveGoodValue()
        {
            var valuesReferredPolicy = new string[] {"no-referrer", "no-referrer-when-downgrade", "origin", "", "origin-when-cross-origin",
            "same-origin", "strict-origin", "strict-origin-when-cross-origin", "unsafe-url" };
            foreach (string value in valuesReferredPolicy)
            {
                string html = $"<script referrerpolicy=\"{value}\"></script>";
                var parser = TestFactory.InitScriptParser();

                parser.Parse(html);
                bool isValid = parser.IsValid();

                Assert.True(isValid);
            }
        }


        [Fact]
        public void ScriptParseAndNoValidateItBadAttribute()
        {
            string html = "<script alt=\"nothing\"></script>";
            var parser = TestFactory.InitScriptParser();

            parser.Parse(html);
            bool isValid = parser.IsValid();

            Assert.False(isValid);
        }
    }
}