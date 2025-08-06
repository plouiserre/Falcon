using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.Data
{
    //TODO delete
    public enum TagData
    {
        html, doctype, metaCharset, head
    }

    public class HtmlPageData
    {
        private static TagModel _doctypeTag;
        private static TagModel _htmlTag;
        private static TagModel _headTag;
        private static HtmlPage _htmlPage { get; set; }
        private static TagModel _metaCharsetTag { get; set; }

        public static TagModel GetTagModel(TagData tag)
        {
            InitHtmlPage();
            switch (tag)
            {
                case TagData.doctype:
                    return _doctypeTag;
                case TagData.head:
                    return _headTag;
                case TagData.metaCharset:
                    return _metaCharsetTag;
                default:
                    return _htmlTag;
            }
        }

        public static HtmlPage InitHtmlPage()
        {
            _doctypeTag = GetDoctypeTag();
            _htmlTag = GetTagHtml();
            _headTag = GetHeadTag();
            var body = GetBodyTag();
            var divContent = GetDivContent();
            var firstP = GetFirstPContent();
            var secondP = GetSecondPContent();
            var tags = new List<TagModel>() { _doctypeTag, _htmlTag, _headTag, body, divContent, firstP, secondP };
            _htmlPage = new HtmlPage() { Tags = tags };
            return _htmlPage;
        }

        private static TagModel GetDoctypeTag()
        {
            var doctypeTag = new TagModel()
            {
                NameTag = NameTagEnum.doctype,
                TagStart = "<!DOCTYPE html>",
                TagFamily = TagFamilyEnum.NoEnd
            };
            return doctypeTag;
        }

        private static TagModel GetTagHtml()
        {
            var attributLang = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.lang.ToString(), Value = "en" };
            var attributDir = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.dir.ToString(), Value = "auto" };
            var attributXmlns = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.xmlns.ToString(), Value = "http://www.w3.org/1999/xhtml" };
            var htmlTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributLang, attributDir, attributXmlns },
                NameTag = NameTagEnum.html,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlData.ContentHtmlSimpleWithSpace,
                TagStart = "<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">",
                TagEnd = "</html>"
            };
            return htmlTag;
        }

        private static TagModel GetHeadTag()
        {
            _metaCharsetTag = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() { new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.charset.ToString(), Value = "UTF-8" } },
                NameTag = NameTagEnum.meta,
                TagStart = HtmlData.MetaCharset
            };
            var metaViewPort = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.name.ToString(), Value = "viewport" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.content.ToString(), Value = "width=device-width, initial-scale=1.0" }
                },
                NameTag = NameTagEnum.meta,
                TagStart = HtmlData.MetaViewPort
            };
            var title = new TagModel()
            {
                TagFamily = TagFamilyEnum.WithEnd,
                NameTag = NameTagEnum.title,
                Content = "Document",
                TagStart = "<title>",
                TagEnd = "</title>"
            };
            var link = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.rel.ToString(), Value = "stylesheet" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.href.ToString(), Value = "main.css" },
                        new AttributeModel(){ FamilyAttribute = "data-preload", Value = "true"}
                },
                NameTag = NameTagEnum.link,
                TagStart = "<link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">",
            };
            var headTag = new TagModel()
            {
                NameTag = NameTagEnum.head,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlData.ContentHeadSimple,
                Children = new List<TagModel>() { _metaCharsetTag, metaViewPort, title, link },
                TagStart = "<head>",
                TagEnd = "</head>"
            };
            return headTag;
        }

        private static TagModel GetBodyTag()
        {
            var bodyTag = new TagModel()
            {
                NameTag = NameTagEnum.body,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlData.DivIdContent
            };
            return bodyTag;
        }

        private static TagModel GetDivContent()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.id.ToString(), Value = "content" };
            string content = HtmlData.FirstPHtmlSimple;
            var divTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributId },
                NameTag = NameTagEnum.div,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<div id=\"content\">",
                TagEnd = "</div>"
            };
            return divTag;
        }

        private static TagModel GetFirstPContent()
        {
            var attributClass = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "declarationText" };
            var child = GetSpanParagraph();
            var pTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributClass },
                NameTag = NameTagEnum.p,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlData.SecondPHtmlSimple,
                Children = new List<TagModel>() { child },
                TagStart = "<p class=\"declarationText\">",
                TagEnd = "</p>"
            };

            return pTag;
        }

        private static TagModel GetSecondPContent()
        {
            var secondP = new TagModel()
            {
                NameTag = NameTagEnum.p,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Allez-vous apprécier mon article?",
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            return secondP;
        }

        private static TagModel GetSpanParagraph()
        {
            string contentHtml = " <a href=\"declaration.html\"> paragraphe </a> ";

            var child = GetAParagraph();

            var spanTag = new TagModel()
            {
                NameTag = NameTagEnum.span,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = contentHtml,
                Children = new List<TagModel>() { child },
                TagStart = "<span>",
                TagEnd = "</span>"
            };

            return spanTag;
        }

        private static TagModel GetAParagraph()
        {
            var aTag = new TagModel()
            {
                NameTag = NameTagEnum.a,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = " paragraphe ",
                TagStart = "<a href=\"declaration.html\">",
                TagEnd = "</a>",
                Attributes = new List<AttributeModel>()
                {
                    new AttributeModel(){ FamilyAttribute = "href", Value="declaration.html"}
                }
            };

            return aTag;
        }
    }
}