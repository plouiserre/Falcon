using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing
{
    public class InitiateParserTest
    {
        private IdentifyTag _identifyTag;

        public InitiateParserTest()
        {
            _identifyTag = TestFactory.InitIdentifyTag();
        }

        [Fact]
        public void ShouldInitiateHtmlParser()
        {
            string? html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.htmlPageWithDoctype);
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Equal(2, parsers.Count);
            Assert.IsType<DoctypeParser>(parsers[0]);
            Assert.IsType<HtmlTagParser>(parsers[1]);
        }

        [Fact]
        public void ShoudInitiateDoctypeParser()
        {
            string? html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.doctype);
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<DoctypeParser>(parsers[0]);
        }

        [Fact]
        public void ShoudInitiateHeadParser()
        {
            string? html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.head);
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<HeadParser>(parsers[0]);
        }

        [Theory]
        [InlineData("<meta charset=\"UTF-8\">")]
        [InlineData("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">")]
        public void ShoudInitiateMetaParser(string html)
        {
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<MetaParser>(parsers[0]);
        }

        [Fact]
        public void ShoudInitiateLinkParser()
        {
            string html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.link);
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<LinkParser>(parsers[0]);
        }


        [Fact]
        public void ShoudInitiateTitleParser()
        {
            string html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.title);
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<TitleParser>(parsers[0]);
        }

        [Fact]
        public void ShouldInitiateAllHeadContentParsers()
        {
            string html = HtmlPageSimpleData.GetHtml(TagHtmlSimple.head).Replace("<head>", string.Empty).Replace("</head>", string.Empty);
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Equal(4, parsers.Count);
            Assert.IsType<MetaParser>(parsers[0]);
            Assert.IsType<MetaParser>(parsers[1]);
            Assert.IsType<TitleParser>(parsers[2]);
            Assert.IsType<LinkParser>(parsers[3]);
        }

        [Fact]
        public void ShouldInitiateAllHeadContentParsersWithHtmlNotClean()
        {
            string html = "                                                    <meta charset=\"UTF-8\">                            <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">                            <title>Document</title>                            <link rel=\"stylesheet\" href=\"main.css\">                        ";
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Equal(4, parsers.Count);
            Assert.IsType<MetaParser>(parsers[0]);
            Assert.IsType<MetaParser>(parsers[1]);
            Assert.IsType<TitleParser>(parsers[2]);
            Assert.IsType<LinkParser>(parsers[3]);
        }


        [Fact]
        public void ShouldInitiateParserWithSomeBadTextBeforeFirstTag()
        {
            string html = " Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span>";
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<SpanParser>(parsers[0]);
        }

        [Fact]
        public void ShouldInitiateTwoParagraphs()
        {
            string html = "<p class=\"declarationText\"> Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span></p><p>Allez-vous apprécier mon article?</p>";
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Equal(2, parsers.Count);
            Assert.IsType<PParser>(parsers[0]);
            Assert.IsType<PParser>(parsers[1]);
        }

        [Fact]
        public void ShouldInitiateH1()
        {
            string html = "<h1>Present your candidature</h1>";
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<H1Parser>(parsers[0]);
        }

        [Fact]
        public void ThrowsExceptionBecauseOfUnknownTag()
        {
            string html = "<declaration>Hello world</declaration>";
            var initiate = TestFactory.InitInitiateParser();

            var exception = Assert.Throws<ParserNotFoundException>(() => initiate.GetTagParsers(html));

            Assert.Equal(ErrorTypeParsing.parserNotFoundException, exception.ErrorType);
            Assert.Equal("We cannot find a parser for <declaration> Tag", exception.Message);
            Assert.Equal("<declaration>", exception.NameTag);
        }
    }
}