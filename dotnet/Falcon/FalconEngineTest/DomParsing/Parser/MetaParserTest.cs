using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class MetaParserTest
    {

        private MetaParser _metaParser;

        public MetaParserTest()
        {
            var identifyTag = TestFactory.InitIdentifyTag();
            var attributeTagParser = TestFactory.InitAttributeTagParser();
            var attributeTagManager = TestFactory.InitAttributeTagManager(true);
            _metaParser = new MetaParser(identifyTag, attributeTagParser, attributeTagManager);
        }

        [Fact]
        public void ParseMetaCharset()
        {
            string html = HtmlData.MetaCharset;

            var tag = _metaParser.Parse(html);

            Assert.Equal(html, tag.TagStart);
            Assert.Null(tag.TagEnd);
            Assert.Equal(NameTagEnum.meta, tag.NameTag);
            Assert.Null(tag.Content);
            Assert.Equal(TagFamilyEnum.NoEnd, tag.TagFamily);
            Assert.Single(tag.Attributes);
            Assert.Equal(FamilyAttributeEnum.charset.ToString(), tag.Attributes[0].FamilyAttribute);
            Assert.Equal("UTF-8", tag.Attributes[0].Value);
            Assert.Null(tag.Children);
        }

        [Fact]
        public void ParseMetaProperty()
        {
            string html = HtmlData.MetaViewPort;

            var tag = _metaParser.Parse(html);

            Assert.Equal(html, tag.TagStart);
            Assert.Null(tag.TagEnd);
            Assert.Equal(NameTagEnum.meta, tag.NameTag);
            Assert.Null(tag.Content);
            Assert.Equal(TagFamilyEnum.NoEnd, tag.TagFamily);
            Assert.Equal(2, tag.Attributes.Count);
            Assert.Equal(FamilyAttributeEnum.name.ToString(), tag.Attributes[0].FamilyAttribute);
            Assert.Equal("viewport", tag.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.content.ToString(), tag.Attributes[1].FamilyAttribute);
            Assert.Equal("width=device-width, initial-scale=1.0", tag.Attributes[1].Value);
            Assert.Null(tag.Children);
        }

        [Fact]
        public void ParseMetaCharsetWithTooManyHtml()
        {
            string html = HtmlData.MetaCharset;

            var tag = _metaParser.Parse(HtmlData.ContentHeadSimple);

            Assert.Equal(html, tag.TagStart);
            Assert.Null(tag.TagEnd);
            Assert.Equal(NameTagEnum.meta, tag.NameTag);
            Assert.Null(tag.Content);
            Assert.Equal(TagFamilyEnum.NoEnd, tag.TagFamily);
            Assert.Single(tag.Attributes);
            Assert.Equal(FamilyAttributeEnum.charset.ToString(), tag.Attributes[0].FamilyAttribute);
            Assert.Equal("UTF-8", tag.Attributes[0].Value);
            Assert.Null(tag.Children);
        }

        [Theory]
        [InlineData("<meta charset=\"UTF-8\">")]
        [InlineData("<meta name=\"description\" content=\"Ceci est une description de la page.\">")]
        [InlineData("<meta http-equiv=\"refresh\" content=\"30;url=https://example.com\">")]
        [InlineData("<meta name=\"author\" content=\"Jean Dupont\" id=\"meta-author\" class=\"meta-info\" lang=\"fr\" dir=\"ltr\" title=\"Auteur du document\" style=\"display: none;\">")]
        [InlineData("<meta name=\"example\" content=\"1234\" scheme=\"URI\">")]
        public void ParseMetaAndValidateHtml(string html)
        {
            _metaParser.Parse(html);
            bool isValid = _metaParser.IsValid();

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("<meta charset=\"UTF-8\"></meta>")]
        [InlineData("<meta name=\"description\" spellcheck=\"Ceci est une description de la page.\">")]
        public void ParseMetaAndNoValidateHtml(string html)
        {
            _metaParser.Parse(html);
            bool isValid = _metaParser.IsValid();

            Assert.False(isValid);
        }
    }
}