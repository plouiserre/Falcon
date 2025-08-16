using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class BodyParserTest
    {
        [Fact]
        public void ParseBodyDivContent()
        {
            string body = HtmlPageSimpleData.GetBodySimple();
            var bodyParser = TestFactory.InitBodyParser();

            var html = bodyParser.Parse(body);
            bool isValid = bodyParser.IsValid();

            AssertHtml.AssertBodyClassMain(html);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseBodyWithIsAttributAndValidate()
        {
            string body = "<body is=\"world-count\">Hello World!!!</body>";
            var bodyParser = TestFactory.InitBodyParser();

            var html = bodyParser.Parse(body);
            bool isValid = bodyParser.IsValid();

            Assert.Equal("<body is=\"world-count\">", html.TagStart);
            Assert.Equal("</body>", html.TagEnd);
            Assert.Equal("Hello World!!!", html.Content);
            Assert.Equal("isAttr", html.Attributes[0].FamilyAttribute);
            Assert.Equal("world-count", html.Attributes[0].Value);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseBodyParseAndFailValidation()
        {
            string body = "<body contenteditable=\"false\">Hello World!!!</body>";
            var bodyParser = TestFactory.InitBodyParser();

            bodyParser.Parse(body);
            bool isValid = bodyParser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void ParseBodyParseAndFailValidationBecauseChildrenNotValid()
        {
            string body = "<body><div inputmode=\"false\">Hello World!!!</div></body>";
            var bodyParser = TestFactory.InitBodyParser();

            bodyParser.Parse(body);
            bool isValid = bodyParser.IsValid();

            Assert.False(isValid);
        }



    }
}