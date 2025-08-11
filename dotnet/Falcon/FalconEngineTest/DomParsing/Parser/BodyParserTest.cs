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
            string body = HtmlData.BodySimple;
            var bodyParser = TestFactory.InitBodyParser();

            var html = bodyParser.Parse(body);
            bool isValid = bodyParser.IsValid();

            AssertHtml.AssertBodyClassMain(html);
            Assert.True(isValid);
        }
    }
}