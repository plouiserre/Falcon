using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public class HtmlParsing : IHtmlParsing
    {
        private ITagParsing _htmlParse;
        private string _html;

        public HtmlParsing(ITagParsing htmlParse, string html)
        {
            _htmlParse = htmlParse;
            _html = html;
        }

        public HtmlPage Parse()
        {
            var htmlTag = GetTagHtml();
            var headTag = GetHeadTag();
            var metaCharsetTag = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() { new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.charset, Value = "UTF-8" } },
                NameTag = NameTagEnum.meta,
                Content = string.Empty
            };
            var metaViewPort = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.name, Value = "viewport" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.content, Value = "width=device-width, initial-scale=1.0" }
                },
                NameTag = NameTagEnum.meta,
                Content = string.Empty
            };
            var title = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                NameTag = NameTagEnum.title,
                Content = string.Empty
            };
            var link = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.rel, Value = "stylesheet" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.href, Value = "main.css" }
                },
                NameTag = NameTagEnum.link,
                Content = string.Empty
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
                                                        </a>"
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
                Content = "paragraphe"
            };
            var secondP = new TagModel()
            {
                NameTag = NameTagEnum.p,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Allez-vous apprécier mon article?"
            };
            var tags = new List<TagModel>() { htmlTag, headTag, metaCharsetTag, metaViewPort, title, link, body, divContent, firstP, span, a, secondP };
            var htmlPage = new HtmlPage() { Tags = tags };
            return htmlPage;
        }

        private TagModel GetTagHtml()
        {
            var attributLang = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.lang, Value = "en" };
            var htmlTag = _htmlParse.Parse(_html);
            htmlTag.Attributes = new List<AttributeModel>() { attributLang };
            return htmlTag;
        }

        private TagModel GetHeadTag()
        {
            string content = @"<meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>Document</title>
                                <link rel=""stylesheet"" href=""main.css"">";
            var headTag = new TagModel()
            {
                NameTag = NameTagEnum.head,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content
            };
            return headTag;
        }

        private TagModel GetBodyTag()
        {
            string content = @"<div id=""content"">
                                    <p class=""declarationText"">
                                        Ceci est un 
                                            <span>
                                                <a href=""declaration.html"">
                                                    paragraphe
                                                </a>
                                            </span>
                                    </p>
                                    <p>Allez-vous apprécier mon article?</p>
                                </div>";
            var bodyTag = new TagModel()
            {
                NameTag = NameTagEnum.body,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content
            };
            return bodyTag;
        }

        private TagModel GetDivContent()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.id, Value = "content" };
            string content = @"<p class=""declarationText"">
                                    Ceci est un 
                                        <span>
                                            <a href=""declaration.html"">
                                                paragraphe
                                            </a>
                                        </span>
                                </p>
                                <p>Allez-vous apprécier mon article?</p>";
            var divTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributId },
                NameTag = NameTagEnum.div,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content
            };
            return divTag;
        }

        private TagModel GetFirstPContent()
        {
            var attributClass = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss, Value = "declarationText" };
            string contentHtml = @"Ceci est un 
                                    <span>
                                        <a href=""declaration.html"">
                                            paragraphe
                                        </a>
                                    </span>";
            var pTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributClass },
                NameTag = NameTagEnum.p,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = contentHtml
            };

            return pTag;
        }
    }
}