using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngineTest.Utils;

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
            string html = HtmlData.HtmlSimpleWithSpaceDoctype;
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Equal(2, parsers.Count);
            Assert.IsType<DoctypeParser>(parsers[0]);
            Assert.IsType<HtmlTagParser>(parsers[1]);
        }

        [Fact]
        public void ShoudInitiateDoctypeParser()
        {
            string html = HtmlData.SimpleDoctype;
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<DoctypeParser>(parsers[0]);
        }

        [Fact]
        public void ShoudInitiateHeadParser()
        {
            string html = HtmlData.HeadSimple;
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
            string html = HtmlData.LinkHead;
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<LinkParser>(parsers[0]);
        }


        [Fact]
        public void ShoudInitiateTitleParser()
        {
            string html = HtmlData.TitleDocument;
            var initiate = TestFactory.InitInitiateParser();

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<TitleParser>(parsers[0]);
        }

        [Fact]
        public void ShouldInitiateAllHeadContentParsers()
        {
            string html = HtmlData.ContentHeadSimple;
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
    }
}