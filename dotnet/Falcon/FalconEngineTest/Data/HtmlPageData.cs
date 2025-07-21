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
        html, doctype, metaCharset, head
    }

    public class HtmlPageData
    {
        private static TagModel _metaCharsetTag;
        private static TagModel _metaViewPort;
        private static TagModel _title;
        private static TagModel _link;
        private static TagModel _doctypeTag;
        private static TagModel _htmlTag;
        private static TagModel _headTag;
        private static HtmlPage _htmlPage { get; set; }

        public static TagModel GetTagModel(TagData tag)
        {
            InitHtmlPage();
            switch (tag)
            {
                case TagData.doctype:
                    return _doctypeTag;
                case TagData.metaCharset:
                    return _metaCharsetTag;
                case TagData.head:
                    return _headTag;
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
            var span = new TagModel()
            {
                TagFamily = TagFamilyEnum.WithEnd,
                NameTag = NameTagEnum.span,
                Content = @"<a href=""declaration.html"">
                                                            paragraphe
                                                        </a>"
            };
            var a = new TagModel()
            {
                Attributes = new List<AttributeModel>(){
                    new AttributeModel()
                    {
                        FamilyAttribute = FamilyAttributeEnum.href.ToString(),
                        Value = "declaration.html"
                    }
                },
                TagFamily = TagFamilyEnum.WithEnd,
                NameTag = NameTagEnum.a,
                Content = "paragraphe"
            };
            var secondP = new TagModel()
            {
                NameTag = NameTagEnum.p,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Allez-vous apprécier mon article?"
            };
            var tags = new List<TagModel>() { _doctypeTag, _htmlTag, _headTag, _metaCharsetTag, _metaViewPort, _title, _link, body, divContent, firstP, span, a, secondP };
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
            _metaViewPort = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.name.ToString(), Value = "viewport" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.content.ToString(), Value = "width=device-width, initial-scale=1.0" }
                },
                NameTag = NameTagEnum.meta,
                TagStart = HtmlData.MetaViewPort
            };
            _title = new TagModel()
            {
                TagFamily = TagFamilyEnum.WithEnd,
                NameTag = NameTagEnum.title,
                Content = "Document",
                TagStart = "<title>",
                TagEnd = "</title>"
            };
            _link = new TagModel()
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
                Children = new List<TagModel>() { _metaCharsetTag, _metaViewPort, _title, _link },
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
                Content = HtmlData.ContentBodyHtmlSimple
            };
            return bodyTag;
        }

        private static TagModel GetDivContent()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.id.ToString(), Value = "content" };
            string content = HtmlData.firstPHtmlSimple;
            var divTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributId },
                NameTag = NameTagEnum.div,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content
            };
            return divTag;
        }

        private static TagModel GetFirstPContent()
        {
            var attributClass = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "declarationText" };
            var pTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributClass },
                NameTag = NameTagEnum.p,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlData.secondPHtmlSimple
            };

            return pTag;
        }
    }
}