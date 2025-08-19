using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.Utils.AssertHtml
{
    public static class AssertCommon
    {
        public static bool AssertTagsAreIdenticals(List<TagModel> allExpected, List<TagModel> results)
        {
            if (allExpected.Count != results.Count)
                return false;
            for (int i = 0; i < allExpected.Count; i++)
            {
                var expectedTag = allExpected[i];
                var resultTag = results[i];
                if (!CheckTagModel(expectedTag, resultTag))
                    return false;
            }
            return true;
        }

        private static bool CheckTagModel(TagModel expected, TagModel result)
        {
            if (expected.Content != result.Content)
                return false;
            if (expected.NameTag != result.NameTag)
                return false;
            if (expected.TagFamily != result.TagFamily)
                return false;
            if (expected.TagStart != result.TagStart)
                return false;
            if (expected.TagEnd != result.TagEnd)
                return false;
            if (expected.Attributes != null && result.Attributes != null)
            {
                if (expected.Attributes.Count != result.Attributes.Count)
                    return false;
                for (int j = 0; j < expected.Attributes.Count; j++)
                {
                    if (!CheckAttribute(expected.Attributes[j], result.Attributes[j]))
                        return false;
                }
            }
            if (expected.Children != null && result.Children != null)
            {
                if (expected.Children.Count != result.Children.Count)
                    return false;
                for (int j = 0; j < expected.Children.Count; j++)
                {
                    if (!CheckTagModel(expected.Children[j], result.Children[j]))
                        return false;
                }
            }
            else if ((expected.Attributes == null && result.Attributes != null) ||
                (expected.Attributes != null && result.Attributes == null))
                return false;
            return true;
        }

        private static bool CheckAttribute(AttributeModel expectedAttribute, AttributeModel resultAttribute)
        {
            if (expectedAttribute.FamilyAttribute != resultAttribute.FamilyAttribute)
                return false;
            if (expectedAttribute.Value != resultAttribute.Value)
                return false;
            return true;
        }

        public static void AssertHeader(TagModel header, string contentHeader)
        {
            Assert.Equal(NameTagEnum.head, header.NameTag);
            Assert.Equal(contentHeader, header.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, header.TagFamily);
            Assert.Equal("<head>", header.TagStart);
            Assert.Equal("</head>", header.TagEnd);
            Assert.NotNull(header.Children);

            AssertHeaderChildren(header.Children);
        }

        public static void AssertHeaderChildren(List<TagModel> contentHeader)
        {
            AssertMetaCharsetChild(contentHeader[0]);

            AssertMetaViewPortChild(contentHeader[1]);

            AssertTitleDocumentChild(contentHeader[2]);

            AssertLinkCss(contentHeader[3]);
        }

        public static void AssertMetaCharsetChild(TagModel metaCharsetChild)
        {
            Assert.Equal(NameTagEnum.meta, metaCharsetChild.NameTag);
            Assert.Null(metaCharsetChild.Content);
            Assert.Equal(FamilyAttributeEnum.charset.ToString(), metaCharsetChild.Attributes[0].FamilyAttribute);
            Assert.Equal("UTF-8", metaCharsetChild.Attributes[0].Value);
            Assert.Equal(TagFamilyEnum.NoEnd, metaCharsetChild.TagFamily);
            Assert.Equal(HtmlPageSimpleData.GetHtml(TagHtmlSimple.metaCharset), metaCharsetChild.TagStart);
            Assert.Null(metaCharsetChild.TagEnd);
            Assert.Null(metaCharsetChild.Children);
        }

        public static void AssertMetaViewPortChild(TagModel metaViewPort)
        {
            Assert.Equal(NameTagEnum.meta, metaViewPort.NameTag);
            Assert.Null(metaViewPort.Content);
            Assert.Equal(FamilyAttributeEnum.name.ToString(), metaViewPort.Attributes[0].FamilyAttribute);
            Assert.Equal("viewport", metaViewPort.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.content.ToString(), metaViewPort.Attributes[1].FamilyAttribute);
            Assert.Equal("width=device-width, initial-scale=1.0", metaViewPort.Attributes[1].Value);
            Assert.Equal(TagFamilyEnum.NoEnd, metaViewPort.TagFamily);
            Assert.Equal(HtmlPageSimpleData.GetHtml(TagHtmlSimple.metaviewPort), metaViewPort.TagStart);
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

        public static void AssertLinkCss(TagModel linkCss)
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