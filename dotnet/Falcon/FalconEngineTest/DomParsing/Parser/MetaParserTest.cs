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
            var attributeTagManager = TestFactory.InitAttributeTagManager(true);
            _metaParser = new MetaParser(identifyTag, attributeTagManager);
        }

        [Fact]
        public void ParseMetaCharset()
        {
            string html = HtmlPageSimpleData.GetMetaCharset();

            var tag = _metaParser.Parse(html);

            AssertHtml.AssertMetaCharsetChild(tag);
        }

        [Fact]
        public void ParseMetaProperty()
        {
            string html = HtmlPageSimpleData.GetMetaViewPort();

            var tag = _metaParser.Parse(html);

            AssertHtml.AssertMetaViewPortChild(tag);
        }

        [Fact]
        public void ParseMetaCharsetWithTooManyHtml()
        {
            string html = HtmlPageSimpleData.GetMetaCharset();

            string contentHeadSimple = HtmlPageSimpleData.GetHead().Replace("<head>", string.Empty).Replace("</head>", string.Empty);
            var tag = _metaParser.Parse(contentHeadSimple);

            AssertHtml.AssertMetaCharsetChild(tag);
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
        [InlineData("<meta ondragleave=\"leave()\">")]
        [InlineData("<meta name=\"description\" spellcheck=\"Ceci est une description de la page.\">")]
        public void ParseMetaAndNoValidateHtml(string html)
        {
            _metaParser.Parse(html);
            bool isValid = _metaParser.IsValid();

            Assert.False(isValid);
        }
    }
}