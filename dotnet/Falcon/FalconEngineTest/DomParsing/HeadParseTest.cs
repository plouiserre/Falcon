using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing
{
    public class HeadParseTest
    {

        public HeadParseTest()
        {
        }

        [Fact]
        public void IsHeadParseIsExtract()
        {
            var headParse = new HeadParse();

            var tagHtml = headParse.Parse(HtmlData.ContentHtmlSimpleWithSpace);

            Assert.Equal(HtmlData.ContentHeadSimple, tagHtml.Content);
            Assert.Equal(NameTagEnum.head, tagHtml.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
            Assert.Equal("<head>", tagHtml.TagStart);
            Assert.Equal("</head>", tagHtml.TagEnd);
        }

        [Fact]
        public void IsHeadParseIsFailingBecauseNoTagEnd()
        {
            var headParse = new HeadParse();

            string badHtml = HtmlData.ContentHtmlSimpleWithSpace.Replace("</head>", string.Empty);
            var exception = Assert.Throws<HeadParsingException>(() => headParse.Parse(badHtml));

            Assert.Equal($"Une erreur a eu lieu lors du parsing de {badHtml}", exception.Message);
            Assert.Equal(ErrorType.head, exception.ErrorType);
        }
    }
}