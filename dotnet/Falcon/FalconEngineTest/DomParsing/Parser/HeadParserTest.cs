using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class HeadParserTest
    {

        public HeadParserTest()
        {
        }

        [Fact]
        public void IsHeadParseIsExtract()
        {
            var headParser = new HeadParser();

            var tagHtml = headParser.Parse(HtmlData.ContentHtmlSimpleWithSpace);

            Assert.Equal(HtmlData.ContentHeadSimple, tagHtml.Content);
            Assert.Equal(NameTagEnum.head, tagHtml.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
            Assert.Equal("<head>", tagHtml.TagStart);
            Assert.Equal("</head>", tagHtml.TagEnd);
        }

        [Fact]
        public void IsHeadParseIsFailingBecauseNoTagEnd()
        {
            var headParser = new HeadParser();

            string badHtml = HtmlData.ContentHtmlSimpleWithSpace.Replace("</head>", string.Empty);
            var exception = Assert.Throws<HeadParsingException>(() => headParser.Parse(badHtml));

            Assert.Equal($"Une erreur a eu lieu lors du parsing de {badHtml}", exception.Message);
            Assert.Equal(ErrorType.head, exception.ErrorType);
        }
    }
}