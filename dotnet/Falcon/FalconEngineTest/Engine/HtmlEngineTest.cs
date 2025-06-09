using FalconEngine.DomParsing;
using FalconEngine.Engine;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using Xunit;

namespace FalconEngineTest.Engine
{
    public class HtmlEngineTest
    {

        private HtmlParsing _htmlParsing { get; set; }

        public HtmlEngineTest()
        {
            var htmlTagParse = new HtmlTagParse();
            _htmlParsing = new HtmlParsing(htmlTagParse, HtmlData.HtmlSimpleWithSpace);
        }

        [Fact]
        public void IsSimplePageIsAnalyzedCorrectly()
        {
            HtmlPage htmlPage = GetHtmlPage();
            var engine = new HtmlEngine(_htmlParsing);

            var engineResult = engine.Calculate(HtmlData.HtmlSimpleWithSpace);

            Assert.True(CompareTags(htmlPage.Tags, engineResult.Tags));
        }


        private HtmlPage GetHtmlPage()
        {
            var htmlTag = GetTagHtml();
            var headTag = GetHeadTag();
            var metaCharsetTag = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() { new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.charset, Value = "UTF-8" } },
                NameTag = NameTagEnum.meta,
                Content = string.Empty
            };
            var metaViewPort = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.name, Value = "viewport" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.content, Value = "width=device-width, initial-scale=1.0" }
                },
                NameTag = NameTagEnum.meta,
                Content = string.Empty
            };
            var title = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                NameTag = NameTagEnum.title,
                Content = string.Empty
            };
            var link = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.rel, Value = "stylesheet" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.href, Value = "main.css" }
                },
                NameTag = NameTagEnum.link,
                Content = string.Empty
            };
            var body = GetBodyTag();
            var divContent = GetDivContent();
            var firstP = GetFirstPContent();
            var span = new TagModel()
            {
                TagFamily = TagFamilyEnum.WithEnd,
                NameTag = NameTagEnum.span,
                Content = @"<a href=""declaration.html"">
                                                            paragraphe
                                                        </a>"
            };
            var a = new TagModel()
            {
                Attributes = new List<AttributeModel>(){
                    new AttributeModel()
                    {
                        FamilyAttribute = FamilyAttributeEnum.href,
                        Value = "declaration.html"
                    }
                },
                TagFamily = TagFamilyEnum.WithEnd,
                NameTag = NameTagEnum.a,
                Content = "paragraphe"
            };
            var secondP = new TagModel()
            {
                NameTag = NameTagEnum.p,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Allez-vous apprécier mon article?"
            };
            var tags = new List<TagModel>() { htmlTag, headTag, metaCharsetTag, metaViewPort, title, link, body, divContent, firstP, span, a, secondP };
            var htmlPage = new HtmlPage() { Tags = tags };
            return htmlPage;
        }

        private TagModel GetTagHtml()
        {
            var attributLang = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.lang, Value = "en" };
            var attributDir = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.dir, Value = "auto" };
            var htmlTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributLang, attributDir },
                NameTag = NameTagEnum.html,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlData.ContentHtmlSimpleWithSpace
            };
            return htmlTag;
        }

        private TagModel GetHeadTag()
        {
            var headTag = new TagModel()
            {
                NameTag = NameTagEnum.head,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlData.ContentHeadSimple
            };
            return headTag;
        }

        private TagModel GetBodyTag()
        {
            var bodyTag = new TagModel()
            {
                NameTag = NameTagEnum.body,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlData.BodyHtmlSimple
            };
            return bodyTag;
        }

        private TagModel GetDivContent()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.id, Value = "content" };
            string content = HtmlData.firstPHtmlSimple;
            var divTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributId },
                NameTag = NameTagEnum.div,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content
            };
            return divTag;
        }

        private TagModel GetFirstPContent()
        {
            var attributClass = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss, Value = "declarationText" };
            var pTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributClass },
                NameTag = NameTagEnum.p,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlData.secondPHtmlSimple
            };

            return pTag;
        }

        //TODO externalize in a new file
        private bool CompareTags(List<TagModel> allExpected, List<TagModel> results)
        {
            if (allExpected.Count != results.Count)
                return false;
            for (int i = 0; i < allExpected.Count; i++)
            {
                TagModel expected = allExpected[i];
                TagModel result = results[i];
                if (expected.Content != result.Content)
                    return false;
                if (expected.NameTag != result.NameTag)
                    return false;
                if (expected.TagFamily != result.TagFamily)
                    return false;
                if (expected.Attributes != null && result.Attributes != null)
                {
                    if (expected.Attributes.Count != result.Attributes.Count)
                        return false;
                    for (int j = 0; j < expected.Attributes.Count; j++)
                    {
                        var expectedAttribute = expected.Attributes[j];
                        var resultAttribute = result.Attributes[j];
                        if (expectedAttribute.FamilyAttribute != resultAttribute.FamilyAttribute)
                            return false;
                        if (expectedAttribute.Value != resultAttribute.Value)
                            return false;
                    }
                }
                else if ((expected.Attributes == null && result.Attributes != null) ||
                    (expected.Attributes != null && result.Attributes == null))
                    return false;
            }
            return true;
        }

    }
}