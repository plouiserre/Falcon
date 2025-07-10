using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class AttributeTagParserTest
    {
        [Fact]
        public void ParseOneAttribute()
        {
            string html = HtmlData.MetaCharset;
            var attributeTagParser = new AttributeTagParser();

            var attributs = attributeTagParser.Parse(html);

            Assert.Single(attributs);
            Assert.Equal(FamilyAttributeEnum.charset, attributs[0].FamilyAttribute);
            Assert.Equal("UTF-8", attributs[0].Value);
        }


        [Fact]
        public void ParseTwoAttributes()
        {
            string html = HtmlData.MetaViewPort;
            var attributeTagParser = new AttributeTagParser();

            var attributs = attributeTagParser.Parse(html);

            Assert.Equal(2, attributs.Count);
            Assert.Equal(FamilyAttributeEnum.name, attributs[0].FamilyAttribute);
            Assert.Equal("viewport", attributs[0].Value);
            Assert.Equal(FamilyAttributeEnum.content, attributs[1].FamilyAttribute);
            Assert.Equal("width=device-width, initial-scale=1.0", attributs[1].Value);
        }


        [Theory]
        [InlineData("<meta name=\"viewport\" wrong content=\"width=device-width, initial-scale=1.0\">")]
        [InlineData("<meta bigname=\"viewport\" content=\"width=device-width, initial-scale=1.0\">")]
        public void ParseFailesAttributes(string html)
        {
            var attributeTagParser = new AttributeTagParser();

            var exception = Assert.Throws<AttributeTagParserException>(() => attributeTagParser.Parse(html));

            Assert.Equal(ErrorTypeParsing.attributes, exception.ErrorType);
            Assert.Equal($"We fail to parse the attributes of {html}", exception.Message);
        }


        [Theory]
        [InlineData("<meta name=\"viewport\" wrong content=\"width=device-width, initial-scale=1.0\">")]
        [InlineData("<meta bigname=\"viewport\" content=\"width=device-width, initial-scale=1.0\">")]
        [InlineData("<title class=\"beautiful\">Document</title>")]
        public void ValidateAttributeIsHere(string html)
        {
            var attributeTagParser = new AttributeTagParser();

            bool isAttributeHere = attributeTagParser.IsAttributePresent(html);

            Assert.True(isAttributeHere);
        }


        [Theory]
        [InlineData("<!DOCTYPE html>")]
        [InlineData("<title>Document</title>")]
        public void NoAttributeIsHere(string html)
        {
            var attributeTagParser = new AttributeTagParser();

            bool isAttributeHere = attributeTagParser.IsAttributePresent(html);

            Assert.False(isAttributeHere);
        }
    }
}