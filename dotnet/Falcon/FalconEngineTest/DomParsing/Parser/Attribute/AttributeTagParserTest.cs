using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser.Attribute
{
    public class AttributeTagParserTest
    {
        [Fact]
        public void ParseOneAttribute()
        {
            string html = HtmlPageSimpleData.GetMetaCharset();
            var attributeTagParser = TestFactory.InitAttributeTagParser();

            var attributs = attributeTagParser.Parse(html);

            Assert.Single(attributs);
            Assert.Equal(FamilyAttributeEnum.charset.ToString(), attributs[0].FamilyAttribute);
            Assert.Equal("UTF-8", attributs[0].Value);
        }


        [Fact]
        public void ParseTwoAttributes()
        {
            string html = HtmlPageSimpleData.GetMetaViewPort();
            var attributeTagParser = TestFactory.InitAttributeTagParser();

            var attributs = attributeTagParser.Parse(html);

            Assert.Equal(2, attributs.Count);
            Assert.Equal(FamilyAttributeEnum.name.ToString(), attributs[0].FamilyAttribute);
            Assert.Equal("viewport", attributs[0].Value);
            Assert.Equal(FamilyAttributeEnum.content.ToString(), attributs[1].FamilyAttribute);
            Assert.Equal("width=device-width, initial-scale=1.0", attributs[1].Value);
        }

        [Fact]
        public void HtmlAttribute()
        {
            string html = "<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><link rel=\"stylesheet\" href=\"main.css\"></head><body><div id=\"content\">\n                                    <p class=\"declarationText\">\n                                        Ceci est un \n                                            <span>\n                                                <a href=\"declaration.html\">\n                                                    paragraphe\n                                                </a>\n                                            </span>\n                                    </p>\n                                    <p>Allez-vous apprécier mon article?</p>\n                                </div></body></html>";

            var attributeTagParser = TestFactory.InitAttributeTagParser();

            var attributs = attributeTagParser.Parse(html);

            Assert.Equal(3, attributs.Count);
            Assert.Equal(FamilyAttributeEnum.lang.ToString(), attributs[0].FamilyAttribute);
            Assert.Equal("en", attributs[0].Value);
            Assert.Equal(FamilyAttributeEnum.dir.ToString(), attributs[1].FamilyAttribute);
            Assert.Equal("auto", attributs[1].Value);
            Assert.Equal(FamilyAttributeEnum.xmlns.ToString(), attributs[2].FamilyAttribute);
            Assert.Equal("http://www.w3.org/1999/xhtml", attributs[2].Value);
        }


        [Fact]
        public void LinkAttributesParsingComplexe()
        {
            string html = "<link rel=\"manifest\" href=\" / site.webmanifest\" type=\"application / manifest + json\" crossorigin=\"use - credentials\" referrerpolicy=\"origin - when - cross - origin\" id=\"web - app - manifest\">";

            var attributeTagParser = TestFactory.InitAttributeTagParser();

            var attributs = attributeTagParser.Parse(html);

            Assert.Equal(6, attributs.Count);
            Assert.Equal(FamilyAttributeEnum.rel.ToString(), attributs[0].FamilyAttribute);
            Assert.Equal("manifest", attributs[0].Value);
            Assert.Equal(FamilyAttributeEnum.href.ToString(), attributs[1].FamilyAttribute);
            Assert.Equal(" / site.webmanifest", attributs[1].Value);
            Assert.Equal(FamilyAttributeEnum.type.ToString(), attributs[2].FamilyAttribute);
            Assert.Equal("application / manifest + json", attributs[2].Value);
            Assert.Equal(FamilyAttributeEnum.crossorigin.ToString(), attributs[3].FamilyAttribute);
            Assert.Equal("use - credentials", attributs[3].Value);
            Assert.Equal(FamilyAttributeEnum.referrerpolicy.ToString(), attributs[4].FamilyAttribute);
            Assert.Equal("origin - when - cross - origin", attributs[4].Value);
            Assert.Equal(FamilyAttributeEnum.id.ToString(), attributs[5].FamilyAttribute);
            Assert.Equal("web - app - manifest", attributs[5].Value);
        }


        [Theory]
        [InlineData("<meta name=\"viewport\" wrong content=\"width=device-width, initial-scale=1.0\">")]
        [InlineData("<meta bigname=\"viewport\" content=\"width=device-width, initial-scale=1.0\">")]
        public void ParseFailesAttributes(string html)
        {
            var attributeTagParser = TestFactory.InitAttributeTagParser();

            var exception = Assert.Throws<AttributeTagParserException>(() => attributeTagParser.Parse(html));

            Assert.Equal(ErrorTypeParsing.attributes, exception.ErrorType);
            Assert.Equal($"We fail to parse the attributes of {html}", exception.Message);
        }


        [Theory]
        [InlineData("<meta name=\"viewport\" wrong content=\"width=device-width, initial-scale=1.0\">")]
        [InlineData("<meta bigname=\"viewport\" content=\"width=device-width, initial-scale=1.0\">")]
        [InlineData("<title class=\"beautiful\">Document</title>")]
        [InlineData("<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">")]
        public void ValidateAttributeIsHere(string html)
        {
            var attributeTagParser = TestFactory.InitAttributeTagParser();

            bool isAttributeHere = attributeTagParser.IsAttributePresent(html);

            Assert.True(isAttributeHere);
        }


        [Theory]
        [InlineData("<!DOCTYPE html>")]
        [InlineData("<title>Document</title>")]
        public void NoAttributeIsHere(string html)
        {
            var attributeTagParser = TestFactory.InitAttributeTagParser();

            bool isAttributeHere = attributeTagParser.IsAttributePresent(html);

            Assert.False(isAttributeHere);
        }
    }
}