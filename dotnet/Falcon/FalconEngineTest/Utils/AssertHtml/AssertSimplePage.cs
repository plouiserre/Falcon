using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.Utils.AssertHtml
{
    public static class AssertSimplePage
    {
        public static void AssertBodyClassMain(TagModel body)
        {
            Assert.Equal(NameTagEnum.body, body.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, body.TagFamily);
            Assert.Equal("<body class=\"main\">", body.TagStart);
            Assert.Equal("</body>", body.TagEnd);
            Assert.Equal("classCss", body.Attributes[0].FamilyAttribute);
            Assert.Equal("main", body.Attributes[0].Value);
            Assert.Equal(HtmlPageSimpleData.GetHtml(TagHtmlSimple.divIdContent), body.Content);
            AssertDivContent(body.Children[0]);
        }

        public static void AssertDivContent(TagModel div)
        {
            string content = string.Concat(HtmlPageSimpleData.GetHtml(TagHtmlSimple.pDeclarationText), HtmlPageSimpleData.GetHtml(TagHtmlSimple.pQuestion));
            Assert.Equal(content, div.Content);
            Assert.Single(div.Attributes);
            Assert.Equal("id", div.Attributes[0].FamilyAttribute);
            Assert.Equal("content", div.Attributes[0].Value);
            Assert.Equal("<div id=\"content\">", div.TagStart);
            Assert.Equal("</div>", div.TagEnd);
            Assert.Equal(2, div.Children.Count);
            Assert.Equal(TagFamilyEnum.WithEnd, div.TagFamily);
            Assert.Equal(NameTagEnum.div, div.NameTag);

            AssertPDeclarationText(div.Children[0]);
            AssertPQuestion(div.Children[1]);
        }

        public static void AssertPDeclarationText(TagModel pDeclarationText)
        {
            Assert.Equal("<p class=\"declarationText\">", pDeclarationText.TagStart);
            Assert.Equal("</p>", pDeclarationText.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, pDeclarationText.TagFamily);
            Assert.Equal(NameTagEnum.p, pDeclarationText.NameTag);
            //Assert.Equal(" Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span>", pDeclarationText.Content);
            Assert.Equal(" Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span><span class=\"red\">Et il raconte des supers trucs!!!</span>", pDeclarationText.Content);
            Assert.Single(pDeclarationText.Attributes);
            Assert.Equal("classCss", pDeclarationText.Attributes[0].FamilyAttribute);
            Assert.Equal("declarationText", pDeclarationText.Attributes[0].Value);
            Assert.Equal(2, pDeclarationText.Children.Count);

            AssertSpanLinkParagraph(pDeclarationText.Children[0]);

        }

        public static void AssertSpanLinkParagraph(TagModel spanLink)
        {
            Assert.Equal("<span>", spanLink.TagStart);
            Assert.Equal("</span>", spanLink.TagEnd);
            Assert.Null(spanLink.Attributes);
            Assert.Equal("<a href=\"declaration.html\">paragraphe</a>", spanLink.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, spanLink.TagFamily);
            Assert.Equal(NameTagEnum.span, spanLink.NameTag);
            Assert.Single(spanLink.Children);

            AssertLinkDeclaration(spanLink.Children[0]);
        }

        public static void AssertLinkDeclaration(TagModel aLink)
        {
            Assert.Equal("<a href=\"declaration.html\">", aLink.TagStart);
            Assert.Equal("</a>", aLink.TagEnd);
            Assert.Equal("paragraphe", aLink.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, aLink.TagFamily);
            Assert.Equal(NameTagEnum.a, aLink.NameTag);
            Assert.Equal("href", aLink.Attributes[0].FamilyAttribute);
            Assert.Equal("declaration.html", aLink.Attributes[0].Value);
            Assert.Null(aLink.Children);
        }

        public static void AssertPQuestion(TagModel pQuestion)
        {
            Assert.Equal("<p>", pQuestion.TagStart);
            Assert.Equal("</p>", pQuestion.TagEnd);
            Assert.Equal(TagFamilyEnum.WithEnd, pQuestion.TagFamily);
            Assert.Equal(NameTagEnum.p, pQuestion.NameTag);
            Assert.Equal("Allez-vous apprécier mon article?", pQuestion.Content);
            Assert.Null(pQuestion.Attributes);
        }
    }
}