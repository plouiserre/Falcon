using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class AParserTest
    {
        [Fact]
        public void ParseATagAndValidateIt()
        {
            string html = HtmlPageSimpleData.GetALink();

            var aParser = TestFactory.InitAParser();

            var tag = aParser.Parse(html);
            bool isValid = aParser.IsValid();

            Assert.True(isValid);
            AssertHtml.AssertLinkDeclaration(tag);
        }

        [Fact]
        public void ParseComplexATagAndValidateIt()
        {
            string html = "<a href=\"https://example.com\" target=\"_blank\" rel=\"noopener noreferrer\" hreflang=\"en\" hidden type=\"text/html\" download=\"fichier.html\" referrerpolicy=\"no-referrer\" class=\"lien\" id=\"mon-lien\" title=\"Visiter le site\" data-info=\"exemple\" role=\"link\"> Visitez notre site </a>";

            var aParser = TestFactory.InitAParser();

            var tag = aParser.Parse(html);
            bool isValid = aParser.IsValid();

            Assert.True(isValid);
            Assert.Equal(" Visitez notre site ", tag.Content);
            Assert.Equal(13, tag.Attributes.Count);
            Assert.Equal("href", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("https://example.com", tag.Attributes[0].Value);
            Assert.Equal("target", tag.Attributes[1].FamilyAttribute);
            Assert.Equal("_blank", tag.Attributes[1].Value);
            Assert.Equal("rel", tag.Attributes[2].FamilyAttribute);
            Assert.Equal("noopener noreferrer", tag.Attributes[2].Value);
            Assert.Equal("hreflang", tag.Attributes[3].FamilyAttribute);
            Assert.Equal("en", tag.Attributes[3].Value);
            Assert.Equal("hidden", tag.Attributes[4].FamilyAttribute);
            Assert.Equal("type", tag.Attributes[5].FamilyAttribute);
            Assert.Equal("text/html", tag.Attributes[5].Value);
            Assert.Equal("download", tag.Attributes[6].FamilyAttribute);
            Assert.Equal("fichier.html", tag.Attributes[6].Value);
            Assert.Equal("referrerpolicy", tag.Attributes[7].FamilyAttribute);
            Assert.Equal("no-referrer", tag.Attributes[7].Value);
            Assert.Equal("classCss", tag.Attributes[8].FamilyAttribute);
            Assert.Equal("lien", tag.Attributes[8].Value);
            Assert.Equal("id", tag.Attributes[9].FamilyAttribute);
            Assert.Equal("mon-lien", tag.Attributes[9].Value);
            Assert.Equal("title", tag.Attributes[10].FamilyAttribute);
            Assert.Equal("Visiter le site", tag.Attributes[10].Value);
            Assert.Equal("data-info", tag.Attributes[11].FamilyAttribute);
            Assert.Equal("exemple", tag.Attributes[11].Value);
            Assert.Equal("role", tag.Attributes[12].FamilyAttribute);
            Assert.Equal("link", tag.Attributes[12].Value);
            Assert.Equal("<a href=\"https://example.com\" target=\"_blank\" rel=\"noopener noreferrer\" hreflang=\"en\" hidden type=\"text/html\" download=\"fichier.html\" referrerpolicy=\"no-referrer\" class=\"lien\" id=\"mon-lien\" title=\"Visiter le site\" data-info=\"exemple\" role=\"link\">", tag.TagStart);
            Assert.Equal("</a>", tag.TagEnd);
        }

        [Fact]
        public void ParseATagAndNoValidateIt()
        {
            string html = "<a href=\"index.html\" spellcheck=\"true\">Mon site</a>";

            var aParser = TestFactory.InitAParser();

            aParser.Parse(html);
            bool isValid = aParser.IsValid();

            Assert.False(isValid);
        }
    }
}