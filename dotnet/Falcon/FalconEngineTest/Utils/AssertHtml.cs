using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngineTest.Utils
{
    public static class AssertHtml
    {
        public static void AssertHead(List<TagModel> contentHeader)
        {
            AssertMetaCharsetChild(contentHeader[0]);

            AssertMetaViewPortChild(contentHeader[1]);

            AssertTitleDocumentChild(contentHeader[2]);

            AssertLinkCss(contentHeader[3]);
        }

        private static void AssertMetaCharsetChild(TagModel metaCharsetChild)
        {
            Assert.Equal(NameTagEnum.meta, metaCharsetChild.NameTag);
            Assert.Null(metaCharsetChild.Content);
            Assert.Equal(FamilyAttributeEnum.charset.ToString(), metaCharsetChild.Attributes[0].FamilyAttribute);
            Assert.Equal("UTF-8", metaCharsetChild.Attributes[0].Value);
            Assert.Equal(TagFamilyEnum.NoEnd, metaCharsetChild.TagFamily);
            Assert.Equal(HtmlData.MetaCharset, metaCharsetChild.TagStart);
            Assert.Null(metaCharsetChild.TagEnd);
            Assert.Null(metaCharsetChild.Children);
        }

        private static void AssertMetaViewPortChild(TagModel metaViewPort)
        {
            Assert.Equal(NameTagEnum.meta, metaViewPort.NameTag);
            Assert.Null(metaViewPort.Content);
            Assert.Equal(FamilyAttributeEnum.name.ToString(), metaViewPort.Attributes[0].FamilyAttribute);
            Assert.Equal("viewport", metaViewPort.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.content.ToString(), metaViewPort.Attributes[1].FamilyAttribute);
            Assert.Equal("width=device-width, initial-scale=1.0", metaViewPort.Attributes[1].Value);
            Assert.Equal(TagFamilyEnum.NoEnd, metaViewPort.TagFamily);
            Assert.Equal(HtmlData.MetaViewPort, metaViewPort.TagStart);
            Assert.Null(metaViewPort.TagEnd);
            Assert.Null(metaViewPort.Children);
        }

        private static void AssertTitleDocumentChild(TagModel documentChild)
        {
            Assert.Equal(NameTagEnum.title, documentChild.NameTag);
            Assert.Equal("Document", documentChild.Content);
            Assert.Null(documentChild.Attributes);
            Assert.Equal(TagFamilyEnum.WithEnd, documentChild.TagFamily);
            Assert.Equal("<title>", documentChild.TagStart);
            Assert.Equal("</title>", documentChild.TagEnd);
            Assert.Null(documentChild.Children);
        }

        private static void AssertLinkCss(TagModel linkCss)
        {
            Assert.Equal(NameTagEnum.link, linkCss.NameTag);
            Assert.Null(linkCss.Content);
            Assert.Equal(FamilyAttributeEnum.rel.ToString(), linkCss.Attributes[0].FamilyAttribute);
            Assert.Equal("stylesheet", linkCss.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.href.ToString(), linkCss.Attributes[1].FamilyAttribute);
            Assert.Equal("main.css", linkCss.Attributes[1].Value);
            Assert.Equal(TagFamilyEnum.NoEnd, linkCss.TagFamily);
            Assert.Equal("<link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">", linkCss.TagStart);
            Assert.Null(linkCss.TagEnd);
            Assert.Null(linkCss.Children);
        }
    }
}