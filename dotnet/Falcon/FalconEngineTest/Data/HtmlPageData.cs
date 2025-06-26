using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.Data
{
    public enum TagData
    {
        html, doctype
    }

    public class HtmlPageData
    {
        private static TagModel _doctypeTag;
        private static TagModel _htmlTag;
        private static HtmlPage _htmlPage { get; set; }

        public static TagModel GetTagModel(TagData tag)
        {
            InitHtmlPage();
            if (tag == TagData.doctype)
                return _doctypeTag;
            else
                return _htmlTag;
        }

        public static HtmlPage InitHtmlPage()
        {
            _doctypeTag = GetDoctypeTag();
            _htmlTag = GetTagHtml();
            var headTag = GetHeadTag();
            var metaCharsetTag = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() { new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.charset, Value = "UTF-8" } },
                NameTag = NameTagEnum.meta,
                Content = string.Empty,
                IsValid = true
            };
            var metaViewPort = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.name, Value = "viewport" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.content, Value = "width=device-width, initial-scale=1.0" }
                },
                NameTag = NameTagEnum.meta,
                Content = string.Empty,
                IsValid = true
            };
            var title = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                NameTag = NameTagEnum.title,
                Content = string.Empty,
                IsValid = true
            };
            var link = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.rel, Value = "stylesheet" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.href, Value = "main.css" }
                },
                NameTag = NameTagEnum.link,
                Content = string.Empty,
                IsValid = true
            };
            var body = GetBodyTag();
            var divContent = GetDivContent();
            var firstP = GetFirstPContent();
            var span = new TagModel()
            {
                TagFamily = TagFamilyEnum.WithEnd,
                NameTag = NameTagEnum.span,
                Content = @"<a href=""declaration.html"">
                                                            paragraphe
                                                        </a>",
                IsValid = true
            };
            var a = new TagModel()
            {
                Attributes = new List<AttributeModel>(){
                    new AttributeModel()
                    {
                        FamilyAttribute = FamilyAttributeEnum.href,
                        Value = "declaration.html"
                    }
                },
                TagFamily = TagFamilyEnum.WithEnd,
                NameTag = NameTagEnum.a,
                Content = "paragraphe",
                IsValid = true
            };
            var secondP = new TagModel()
            {
                NameTag = NameTagEnum.p,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Allez-vous apprécier mon article?",
                IsValid = true
            };
            var tags = new List<TagModel>() { _doctypeTag, _htmlTag, headTag, metaCharsetTag, metaViewPort, title, link, body, divContent, firstP, span, a, secondP };
            _htmlPage = new HtmlPage() { Tags = tags };
            return _htmlPage;
        }

        private static TagModel GetDoctypeTag()
        {
            var doctypeTag = new TagModel()
            {
                NameTag = NameTagEnum.doctype,
                TagStart = "<!DOCTYPE html>",
                TagEnd = string.Empty,
                Attributes = null,
                Content = string.Empty,
                TagFamily = TagFamilyEnum.NoEnd,
                IsValid = true
            };
            return doctypeTag;
        }

        private static TagModel GetTagHtml()
        {
            var attributLang = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.lang, Value = "en" };
            var attributDir = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.dir, Value = "auto" };
            var attributXmlns = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.xmlns, Value = "http://www.w3.org/1999/xhtml" };
            var htmlTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributLang, attributDir, attributXmlns },
                NameTag = NameTagEnum.html,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlData.ContentHtmlSimpleWithSpace,
                IsValid = true
            };
            return htmlTag;
        }

        private static TagModel GetHeadTag()
        {
            var headTag = new TagModel()
            {
                NameTag = NameTagEnum.head,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlData.ContentHeadSimple,
                IsValid = true
            };
            return headTag;
        }

        private static TagModel GetBodyTag()
        {
            var bodyTag = new TagModel()
            {
                NameTag = NameTagEnum.body,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlData.BodyHtmlSimple,
                IsValid = true
            };
            return bodyTag;
        }

        private static TagModel GetDivContent()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.id, Value = "content" };
            string content = HtmlData.firstPHtmlSimple;
            var divTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributId },
                NameTag = NameTagEnum.div,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                IsValid = true
            };
            return divTag;
        }

        private static TagModel GetFirstPContent()
        {
            var attributClass = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss, Value = "declarationText" };
            var pTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributClass },
                NameTag = NameTagEnum.p,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlData.secondPHtmlSimple,
                IsValid = true
            };

            return pTag;
        }
    }
}