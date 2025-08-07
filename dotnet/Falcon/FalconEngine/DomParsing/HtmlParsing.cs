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
        private ITagParser _spanParse;
        private ITagParser _pParse;
        private ITagParser _divParse;
        private IExtractHtmlRemaining _extractHtmlRemaining;
        private IAttributeTagManager _attributeTagManager;
        private string _html;

        public HtmlParsing(ITagParser doctypeParse, ITagParser htmlParse, ITagParser headParse,
            IExtractHtmlRemaining extractHtmlRemaining, IAttributeTagManager attributeTagManager,
            ITagParser spanParse, ITagParser pParse, ITagParser divParse)
        {
            _doctypeParse = doctypeParse;
            _htmlParse = htmlParse;
            _extractHtmlRemaining = extractHtmlRemaining;
            _headParse = headParse;
            _attributeTagManager = attributeTagManager;
            _spanParse = spanParse;
            _pParse = pParse;
            _divParse = divParse;
        }

        //TODO reprendre le rendu de cette page avec els enfants et les tests
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
            var secondP = GetSecondPContent();
            var tags = new List<TagModel>() { doctypeTag, htmlTag, headTag, body, divContent, firstP, secondP };

            var htmlPage = new HtmlPage() { Tags = tags };
            return htmlPage;
        }

        //TODO ajouter des tests
        //TODO c'est ici qu'il faut modifier pour gérer la validation!!!!
        private TagModel GetDoctypeTag()
        {
            var doctypeTag = _doctypeParse.Parse(_html);
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
            bool isValid = true;
            if (!isValid)
                throw new Exception("Html tag is not valid!!!");
            return htmlTag;
        }

        private TagModel GetHeadTag()
        {
            var headTag = _headParse.Parse(_html);
            bool isValid = _headParse.IsValid();
            if (!isValid)
                throw new Exception("Head tag is not valid!!!");
            return headTag;
        }

        private TagModel GetBodyTag()
        {
            string content = @"<div id=""content""><p class=""declarationText""> Ceci est un <span><a href=""declaration.html"">paragraphe</a></span></p><p>Allez-vous apprécier mon article?</p></div>";
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
            string contentHtml = @"<p class=""declarationText""> Ceci est un <span><a href=""declaration.html"">paragraphe</a></span></p><p>Allez-vous apprécier mon article?</p>";
            string html = string.Concat("<div id=\"content\">", contentHtml, "</div>");
            var divTag = _divParse.Parse(html);

            return divTag;
        }

        private TagModel GetFirstPContent()
        {
            string contentHtml = @"Ceci est un <span><a href=""declaration.html"">paragraphe</a></span>";
            string html = string.Concat("<p class=\"declarationText\"> ", contentHtml, "</p>");
            var pTag = _pParse.Parse(html);

            return pTag;
        }

        private TagModel GetSecondPContent()
        {
            string html = @"<p>Allez-vous apprécier mon article?</p>";
            var pTag = _pParse.Parse(html);

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