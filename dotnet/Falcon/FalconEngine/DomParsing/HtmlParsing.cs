using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public class HtmlParsing : IHtmlParsing
    {
        private ITagParser _doctypeParse;
        private ITagParser _htmlParse;
        private ITagParser _headParse;
        private IExtractHtmlRemaining _extractHtmlRemaining;
        private IAttributeTagManager _attributeTagManager;
        private string _html;

        public HtmlParsing(ITagParser doctypeParse, ITagParser htmlParse, ITagParser headParse,
            IExtractHtmlRemaining extractHtmlRemaining, IAttributeTagManager attributeTagManager)
        {
            _doctypeParse = doctypeParse;
            _htmlParse = htmlParse;
            _extractHtmlRemaining = extractHtmlRemaining;
            _headParse = headParse;
            _attributeTagManager = attributeTagManager;
        }

        public HtmlPage Parse(string html)
        {
            _html = html;
            _attributeTagManager.SetAttributes();
            var doctypeTag = GetDoctypeTag();
            RemoveUselessHtml(doctypeTag);
            var htmlTag = GetTagHtml();
            RemoveTagOpenCloed(htmlTag);
            var headTag = GetHeadTag();
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
            var tags = new List<TagModel>() { doctypeTag, htmlTag, headTag, headTag.Children[0], headTag.Children[1],
                    headTag.Children[2], headTag.Children[3], body, divContent, firstP, span, a, secondP };
            var htmlPage = new HtmlPage() { Tags = tags };
            return htmlPage;
        }

        //TODO ajouter des tests
        //TODO c'est ici qu'il faut modifier pour gérer la validation!!!!
        private TagModel GetDoctypeTag()
        {
            var doctypeTag = _doctypeParse.Parse(_html);
            //bool isValid = _doctypeParse.IsValid(doctypeTag);
            bool isValid = true;
            if (!isValid)
                throw new Exception("Doctype tag is not valid!!!");
            return doctypeTag;
        }

        //TODO ajouter des tests
        //TODO c'est ici qu'il faut modifier pour gérer la validation!!!!
        private TagModel GetTagHtml()
        {
            var htmlTag = _htmlParse.Parse(_html);
            // bool isValid = _htmlParse.IsValid(htmlTag);
            bool isValid = true;
            if (!isValid)
                throw new Exception("Html tag is not valid!!!");
            return htmlTag;
        }

        private TagModel GetHeadTag()
        {
            var headTag = _headParse.Parse(_html);
            // bool isValid = _headParse.IsValid(headTag);
            bool isValid = true;
            if (!isValid)
                throw new Exception("Head tag is not valid!!!");
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
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.id.ToString(), Value = "content" };
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
            var attributClass = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "declarationText" };
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

        private void RemoveUselessHtml(TagModel tag)
        {
            _html = _extractHtmlRemaining.Extract(tag, _html, ExtractionMode.ASide);
        }

        private void RemoveTagOpenCloed(TagModel tag)
        {
            _html = _extractHtmlRemaining.Extract(tag, _html, ExtractionMode.Inside);
        }
    }
}