using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private ITagParser _bodyParse;
        private ITagParser _pParse;
        private ITagParser _divParse;
        private IExtractHtmlRemaining _extractHtmlRemaining;
        private IAttributeTagManager _attributeTagManager;
        private string _html;

        public HtmlParsing(ITagParser doctypeParse, ITagParser htmlParse, ITagParser headParse,
            IExtractHtmlRemaining extractHtmlRemaining, IAttributeTagManager attributeTagManager,
            ITagParser bodyParse)
        {
            _doctypeParse = doctypeParse;
            _htmlParse = htmlParse;
            _extractHtmlRemaining = extractHtmlRemaining;
            _headParse = headParse;
            _attributeTagManager = attributeTagManager;
            _bodyParse = bodyParse;
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
            var tags = new List<TagModel>() { doctypeTag, htmlTag };

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
            var body = GetBodyTag();
            var htmlTag = _htmlParse.Parse(_html);
            string htmlInsideHtmlTag = _extractHtmlRemaining.Extract(htmlTag, _html, ExtractionMode.Inside);
            var headTag = GetHeadTag(htmlInsideHtmlTag);
            htmlTag.Children = new List<TagModel>() { headTag, body };
            bool isValid = true;
            if (!isValid)
                throw new Exception("Html tag is not valid!!!");
            return htmlTag;
        }

        private TagModel GetHeadTag(string htmlHeader)
        {
            var headTag = _headParse.Parse(htmlHeader);
            bool isValid = _headParse.IsValid();
            if (!isValid)
                throw new Exception("Head tag is not valid!!!");
            return headTag;
        }

        private TagModel GetBodyTag()
        {
            string content = "<div id=\"content\"><p class=\"declarationText\"> Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span><span class=\"red\">Et il raconte des supers trucs!!!</span></p><p>Allez-vous apprécier mon article?</p></div>";
            string html = string.Concat("<body>", content, "</body>");
            var bodyTag = _bodyParse.Parse(html);
            return bodyTag;
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