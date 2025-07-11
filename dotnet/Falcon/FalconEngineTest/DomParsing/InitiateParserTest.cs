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
        private IDeleteUselessSpace _deleteUselessSpace;
        private IdentifyTag _identifyTag;

        public InitiateParserTest()
        {
            _deleteUselessSpace = new DeleteUselessSpace();
            var attributeTagParser = new AttributeTagParser();
            var identifyTagName = new IdentifyTagName();
            var identifyTagFamily = new IdentifyTagFamily();
            _identifyTag = new IdentifyTag(_deleteUselessSpace, attributeTagParser, identifyTagName, identifyTagFamily);
        }

        [Fact]
        public void ShouldInitiateHtmlParser()
        {
            string html = HtmlData.HtmlSimple;
            var initiate = new InitiateParser(_deleteUselessSpace, _identifyTag);

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<HtmlTagParser>(parsers[0]);
        }

        [Fact]
        public void ShoudInitiateDoctypeParser()
        {
            string html = HtmlData.SimpleDoctype;
            var initiate = new InitiateParser(_deleteUselessSpace, _identifyTag);

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<DoctypeParser>(parsers[0]);
        }

        [Fact]
        public void ShoudInitiateHeadParser()
        {
            string html = HtmlData.HeadSimple;
            var initiate = new InitiateParser(_deleteUselessSpace, _identifyTag);

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<HeadParser>(parsers[0]);
        }

        [Theory]
        [InlineData("<meta charset=\"UTF-8\">")]
        [InlineData("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">")]
        public void ShoudInitiateMetaParser(string html)
        {
            var initiate = new InitiateParser(_deleteUselessSpace, _identifyTag);

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<MetaParser>(parsers[0]);
        }

        [Fact]
        public void ShoudInitiateLinkParser()
        {
            string html = HtmlData.LinkHead;
            var initiate = new InitiateParser(_deleteUselessSpace, _identifyTag);

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<LinkParser>(parsers[0]);
        }


        [Fact]
        public void ShoudInitiateTitleParser()
        {
            string html = HtmlData.TitleDocument;
            var initiate = new InitiateParser(_deleteUselessSpace, _identifyTag);

            var parsers = initiate.GetTagParsers(html);

            Assert.Single(parsers);
            Assert.IsType<TitleParser>(parsers[0]);
        }

        [Fact]
        public void ShouldInitiateAllHeadContentParsers()
        {
            string html = HtmlData.ContentHeadSimple;
            var initiate = new InitiateParser(_deleteUselessSpace, _identifyTag);

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
            var initiate = new InitiateParser(_deleteUselessSpace, _identifyTag);

            var parsers = initiate.GetTagParsers(html);

            Assert.Equal(4, parsers.Count);
            Assert.IsType<MetaParser>(parsers[0]);
            Assert.IsType<MetaParser>(parsers[1]);
            Assert.IsType<TitleParser>(parsers[2]);
            Assert.IsType<LinkParser>(parsers[3]);
        }

    }
}