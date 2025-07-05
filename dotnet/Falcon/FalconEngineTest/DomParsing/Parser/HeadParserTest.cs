using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class HeadParserTest
    {

        public HeadParserTest()
        {
        }

        [Fact]
        public void IsHeadParseIsExtract()
        {
            var headParser = new HeadParser();

            var tagHtml = headParser.Parse(HtmlData.HeadSimple);
            var firstChild = tagHtml.Children[0];
            var secondChild = tagHtml.Children[1];
            var thirdChild = tagHtml.Children[2];
            var fourthChild = tagHtml.Children[3];

            Assert.Equal(NameTagEnum.head, tagHtml.NameTag);
            Assert.Equal(HtmlData.ContentHeadSimple, tagHtml.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
            Assert.Equal("<head>", tagHtml.TagStart);
            Assert.Equal("</head>", tagHtml.TagEnd);
            Assert.NotNull(tagHtml.Children);

            AssertMetaCharsetChild(firstChild);

            AssertMetaViewPortChild(secondChild);

            AssertTitleDocumentChild(thirdChild);

            AssertLinkCss(fourthChild);
        }

        private void AssertMetaCharsetChild(TagModel metaCharsetChild)
        {
            Assert.Equal(NameTagEnum.meta, metaCharsetChild.NameTag);
            Assert.Null(metaCharsetChild.Content);
            Assert.Equal(FamilyAttributeEnum.charset, metaCharsetChild.Attributes[0].FamilyAttribute);
            Assert.Equal("UTF-8", metaCharsetChild.Attributes[0].Value);
            Assert.Equal(TagFamilyEnum.NoEnd, metaCharsetChild.TagFamily);
            Assert.Equal(HtmlData.MetaCharset, metaCharsetChild.TagStart);
            Assert.Null(metaCharsetChild.TagEnd);
            Assert.Null(metaCharsetChild.Children);
        }

        private void AssertMetaViewPortChild(TagModel metaViewPort)
        {
            Assert.Equal(NameTagEnum.meta, metaViewPort.NameTag);
            Assert.Null(metaViewPort.Content);
            Assert.Equal(FamilyAttributeEnum.name, metaViewPort.Attributes[0].FamilyAttribute);
            Assert.Equal("viewport", metaViewPort.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.content, metaViewPort.Attributes[1].FamilyAttribute);
            Assert.Equal("width=device-width, initial-scale=1.0", metaViewPort.Attributes[1].Value);
            Assert.Equal(TagFamilyEnum.NoEnd, metaViewPort.TagFamily);
            Assert.Equal(HtmlData.MetaViewPort, metaViewPort.TagStart);
            Assert.Null(metaViewPort.TagEnd);
            Assert.Null(metaViewPort.Children);
        }

        private void AssertTitleDocumentChild(TagModel documentChild)
        {
            Assert.Equal(NameTagEnum.title, documentChild.NameTag);
            Assert.Equal("Document", documentChild.Content);
            Assert.Null(documentChild.Attributes);
            Assert.Equal(TagFamilyEnum.WithEnd, documentChild.TagFamily);
            Assert.Equal("<title>", documentChild.TagStart);
            Assert.Equal("</title>", documentChild.TagEnd);
            Assert.Null(documentChild.Children);
        }

        private void AssertLinkCss(TagModel linkCss)
        {
            Assert.Equal(NameTagEnum.link, linkCss.NameTag);
            Assert.Null(linkCss.Content);
            Assert.Equal(FamilyAttributeEnum.rel, linkCss.Attributes[0].FamilyAttribute);
            Assert.Equal("stylesheet", linkCss.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.href, linkCss.Attributes[1].FamilyAttribute);
            Assert.Equal("main.css", linkCss.Attributes[1].Value);
            Assert.Equal(TagFamilyEnum.NoEnd, linkCss.TagFamily);
            Assert.Equal(HtmlData.LinkHead, linkCss.TagStart);
            Assert.Null(linkCss.TagEnd);
            Assert.Null(linkCss.Children);
        }

        [Fact]
        public void IsHeadParseIsFailingBecauseNoTagEnd()
        {
            var headParser = new HeadParser();

            string badHtml = HtmlData.ContentHtmlSimpleWithSpace.Replace("</head>", string.Empty);
            var exception = Assert.Throws<HeadParsingException>(() => headParser.Parse(badHtml));

            Assert.Equal($"Une erreur a eu lieu lors du parsing de {badHtml}", exception.Message);
            Assert.Equal(ErrorTypeParsing.head, exception.ErrorType);
        }
    }
}