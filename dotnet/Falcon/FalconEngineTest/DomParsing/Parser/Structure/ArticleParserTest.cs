using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser.Structure
{
    public class ArticleParserTest
    {
        [Fact]
        public void ArticleParseAndValidateIt()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.articleTag);
            var articleParser = TestFactory.InitArticleParser();

            var tag = articleParser.Parse(html);
            var isValid = articleParser.IsValid();

            AssertTablePage.AssertArticle(tag);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseSectionAndNoValidateIt()
        {
            string html = "<article alt=\"title\"><section><h1>Hello world</h1></section></article>";
            var articleParser = TestFactory.InitArticleParser();

            articleParser.Parse(html);
            bool isValid = articleParser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void ParseSectionAndNoValidateItBecauseH1NotValid()
        {
            string html = "<article><section><h1 alt=\"title\">Hello world</h1></section></article>";
            var articleParser = TestFactory.InitArticleParser();

            articleParser.Parse(html);
            bool isValid = articleParser.IsValid();

            Assert.False(isValid);
        }
    }
}