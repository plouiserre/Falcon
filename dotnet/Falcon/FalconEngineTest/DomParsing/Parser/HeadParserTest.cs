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

            var tagHtml = headParser.Parse(HtmlData.HeadSimple);
            var firstChild = tagHtml.Children[0];
            var secondChild = tagHtml.Children[1];
            var thirdChild = tagHtml.Children[2];
            var fourthChild = tagHtml.Children[3];

            Assert.Equal(NameTagEnum.head, tagHtml.NameTag);
            Assert.Equal(HtmlData.ContentHeadSimple, tagHtml.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
            Assert.Equal("<head>", tagHtml.TagStart);
            Assert.Equal("</head>", tagHtml.TagEnd);
            Assert.NotNull(tagHtml.Children);

            Assert.Equal(NameTagEnum.meta, firstChild.NameTag);
            Assert.Null(firstChild.Content);
            Assert.Equal(FamilyAttributeEnum.charset, firstChild.Attributes[0].FamilyAttribute);
            Assert.Equal("UTF-8", firstChild.Attributes[0].Value);
            Assert.Equal(HtmlData.MetaCharset, firstChild.TagStart);
            Assert.Null(firstChild.TagEnd);
            Assert.Null(firstChild.Children);


            //TODO remplacer cette fausse egalité par un test sur tous les enfants et voir si on peut en // factoriser les assert des tag
            //Assert.Equal(1, 2);
        }

        [Fact]
        public void IsHeadParseIsFailingBecauseNoTagEnd()
        {
            var headParser = new HeadParser();

            string badHtml = HtmlData.ContentHtmlSimpleWithSpace.Replace("</head>", string.Empty);
            var exception = Assert.Throws<HeadParsingException>(() => headParser.Parse(badHtml));

            Assert.Equal($"Une erreur a eu lieu lors du parsing de {badHtml}", exception.Message);
            Assert.Equal(ErrorTypeParsing.head, exception.ErrorType);
        }
    }
}