using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class HeadParser : ITagParser
    {

        private string _html;
        private string _tagStart;
        private string _tagEnd;

        public HeadParser()
        {
            _tagStart = "<head>";
            _tagEnd = "</head>";
        }

        public string CleanHtml(TagModel tag, string html)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(TagModel tag)
        {
            return tag.TagStart == _tagStart && tag.TagEnd == _tagEnd;
        }

        public TagModel Parse(string html)
        {
            try
            {
                _html = html;
                string content = GetContent();
                return new TagModel()
                {
                    Content = content,
                    NameTag = NameTagEnum.head,
                    TagFamily = TagFamilyEnum.WithEnd,
                    TagEnd = _tagEnd,
                    TagStart = _tagStart
                };
            }
            catch (Exception ex)
            {
                string message = $"Une erreur a eu lieu lors du parsing de {html}";
                throw new HeadParsingException(ErrorType.head, message);
            }
        }

        //TODO check présence des tags start and end
        private string GetContent()
        {
            int count = 0;
            for (int i = 0; i < _html.Length; i++)
            {
                string word = _html.Substring(i, _tagEnd.Length);
                if (word == _tagEnd)
                {
                    count = i;
                    break;
                }
            }
            string contentNotClean = _html.Substring(0, count);
            string content = contentNotClean.Replace(_tagStart, string.Empty);
            return content;
            //faire une exception si on parse mal
        }

        public List<TagModel> DeterminateChildren(string html)
        {
            //TODO call here the InitiateParser
            var initiateParse = new InitiateParser();
            var parsers = initiateParse.GetTagParsers(html);
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
            var children = new List<TagModel>() { metaCharsetTag, metaViewPort, title, link };
            return children;
        }

    }
}