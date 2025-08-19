using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class HeadParserTest
    {

        [Fact]
        public void IsHeadParseIsExtract()
        {
            var headParser = TestFactory.InitHeadParser();

            var tagHtml = headParser.Parse(HtmlPageSimpleData.GetHtml(TagHtmlSimple.head));
            bool isValid = headParser.IsValid();

            string contentHeadSimple = HtmlPageSimpleData.GetHtml(TagHtmlSimple.head).Replace("<head>", string.Empty).Replace("</head>", string.Empty);
            AssertSimplePage.AssertHeader(tagHtml, contentHeadSimple);

            Assert.True(isValid);
        }

        [Fact]
        public void IsHeadParseIsExtractWithHtmlNotClean()
        {
            string htmlNotClean = "\r\n                        <head>\r\n                            <meta charset=\"UTF-8\">\r\n                            <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n                            <title>Document</title>\r\n                            <link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">\r\n                        </head>\r\n                        <body>\r\n                            <div id=\"content\">\r\n                                <p class=\"declarationText\">Ceci est un <span><a href=\"declaration.html\">paragraphe</a></span></p>\r\n                                <p>Allez-vous apprécier mon article?</p>\r\n                            </div>\r\n                        </body>\r\n                    ";
            string content = "                            <meta charset=\"UTF-8\">                            <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">                            <title>Document</title>                            <link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">                        ";
            var headParser = TestFactory.InitHeadParser();

            var tagHtml = headParser.Parse(htmlNotClean);
            bool isValid = headParser.IsValid();

            AssertSimplePage.AssertHeader(tagHtml, content);

            Assert.True(isValid);
        }


        [Fact]
        public void IsHeadParseIsFailingBecauseNoTagEnd()
        {
            var headParser = TestFactory.InitHeadParser();

            string htmlContent = HtmlPageSimpleData.GetHtml(TagHtmlSimple.htmlPage).Replace("<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">", string.Empty).Replace("</html>", string.Empty);
            string badHtml = htmlContent.Replace("</head>", string.Empty);
            var exception = Assert.Throws<HeadParsingException>(() => headParser.Parse(badHtml));

            Assert.Equal($"Une erreur a eu lieu lors du parsing de {badHtml}", exception.Message);
            Assert.Equal(ErrorTypeParsing.head, exception.ErrorType);
        }

        [Fact]
        public void HeadIsNotValidAfterParsing()
        {
            var html = "<head></head>";
            var headerParser = TestFactory.InitHeadParser();

            headerParser.Parse(html);
            bool isValid = headerParser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void ValidationThrowsException()
        {
            var headParser = TestFactory.InitHeadParser();

            var exception = Assert.Throws<ValidationParsingException>(() => headParser.IsValid());

            Assert.Equal("Header validation is failing because parsing is not did", exception.Message);
            Assert.Equal(ErrorTypeParsing.validation, exception.ErrorType);

        }

        [Fact]
        public void HeadValidationFailBecauseOneChildIsNotValid()
        {
            var headerParser = TestFactory.InitHeadParser();

            var html = "<head><meta charset=\"UTF-8\"><title class=\"thetitle\">My Website</title></head>";

            headerParser.Parse(html);
            bool isValid = headerParser.IsValid();

            Assert.False(isValid);
        }
    }
}