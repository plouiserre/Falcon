using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser.Attribute
{
    public class AttributeTagParser : IAttributeTagParser
    {
        private string _html { get; set; }
        private string _startTag { get; set; }
        private List<AttributeModel> _attributes { get; set; }
        private IIdentifyStartTagEndTag _identifyStartTagEndTag;
        private IAnalyzeAttributes _analyzeAttributes;

        public AttributeTagParser(IIdentifyStartTagEndTag identifyStartTagEndTag, IAnalyzeAttributes analyzeAttributes)
        {
            _identifyStartTagEndTag = identifyStartTagEndTag;
            _analyzeAttributes = analyzeAttributes;
        }

        public List<AttributeModel> Parse(string html)
        {
            _attributes = new List<AttributeModel>();
            _html = html;
            _identifyStartTagEndTag.DetermineStartEndTags(_html);
            _startTag = _identifyStartTagEndTag.StartTag;
            GetAttributes();
            return _attributes;
        }

        private void GetAttributes()
        {
            var attributes = _analyzeAttributes.Study(_startTag);
            for (int i = 0; i < attributes.Count; i++)
            {
                var candidate = CleanCandidateAttribute(attributes[i]);
                var attribute = GetAttribute(candidate);
                _attributes.Add(attribute);
            }
        }

        private string CleanCandidateAttribute(string candidateAttribute)
        {
            string cleanCandidate = string.Empty;
            if (candidateAttribute.Contains('<'))
            {
                var elements = candidateAttribute.Split(' ');
                cleanCandidate = elements[1].Replace("\"", string.Empty);
            }
            else
            {
                cleanCandidate = candidateAttribute.Replace("/>", string.Empty);
            }
            return cleanCandidate;
        }

        private AttributeModel GetAttribute(string candidate)
        {
            string separator = "=";
            var elements = candidate.Split(separator);
            string key = elements[0];
            FamilyAttributeEnum familyAttribute = GetFamilyAttributeEnum(key);
            string firstElementToMove = string.Concat(key, separator);
            return new AttributeModel()
            {
                FamilyAttribute = familyAttribute,
                Value = candidate.Contains("=") ? CalculateValueAttribute(candidate, firstElementToMove) : null
            };
        }

        private string CalculateValueAttribute(string candidate, string firstElementToMove)
        {
            string value = candidate.Replace(firstElementToMove, string.Empty);
            value = value.Replace("\"", string.Empty);
            value = value.Replace(">", string.Empty);
            return value;
        }

        private FamilyAttributeEnum GetFamilyAttributeEnum(string key)
        {
            bool parseIsOk = true;
            FamilyAttributeEnum familyAttribute = FamilyAttributeEnum.lang;
            switch (key)
            {
                case "accesskey":
                    familyAttribute = FamilyAttributeEnum.accesskey;
                    break;
                case "as":
                    familyAttribute = FamilyAttributeEnum.asAttr;
                    break;
                case "charset":
                    familyAttribute = FamilyAttributeEnum.charset;
                    break;
                case "class":
                    familyAttribute = FamilyAttributeEnum.classCss;
                    break;
                case "content":
                    familyAttribute = FamilyAttributeEnum.content;
                    break;
                case "contenteditable":
                    familyAttribute = FamilyAttributeEnum.contenteditable;
                    break;
                case "data-preload":
                    familyAttribute = FamilyAttributeEnum.datapreload;
                    break;
                case "data-purpose":
                    familyAttribute = FamilyAttributeEnum.datapurpose;
                    break;
                case "data-user":
                    familyAttribute = FamilyAttributeEnum.datauser;
                    break;
                case "data-theme":
                    familyAttribute = FamilyAttributeEnum.datatheme;
                    break;
                case "data-page":
                    familyAttribute = FamilyAttributeEnum.datapage;
                    break;
                case "dir":
                    familyAttribute = FamilyAttributeEnum.dir;
                    break;
                case "disabled":
                    familyAttribute = FamilyAttributeEnum.disabled;
                    break;
                case "draggable":
                    familyAttribute = FamilyAttributeEnum.draggable;
                    break;
                case "href":
                    familyAttribute = FamilyAttributeEnum.href;
                    break;
                case "http-equiv":
                    familyAttribute = FamilyAttributeEnum.httpequiv;
                    break;
                case "id":
                    familyAttribute = FamilyAttributeEnum.id;
                    break;
                case "integrity":
                    familyAttribute = FamilyAttributeEnum.integrity;
                    break;
                case "lang":
                    familyAttribute = FamilyAttributeEnum.lang;
                    break;
                case "manifest":
                    familyAttribute = FamilyAttributeEnum.manifest;
                    break;
                case "name":
                    familyAttribute = FamilyAttributeEnum.name;
                    break;
                case "referrerpolicy":
                    familyAttribute = FamilyAttributeEnum.referrerpolicy;
                    break;
                case "rel":
                    familyAttribute = FamilyAttributeEnum.rel;
                    break;
                case "role":
                    familyAttribute = FamilyAttributeEnum.role;
                    break;
                case "sizes":
                    familyAttribute = FamilyAttributeEnum.sizes;
                    break;
                case "spellcheck":
                    familyAttribute = FamilyAttributeEnum.spellcheck;
                    break;
                case "style":
                    familyAttribute = FamilyAttributeEnum.style;
                    break;
                case "tabindex":
                    familyAttribute = FamilyAttributeEnum.tabindex;
                    break;
                case "translate":
                    familyAttribute = FamilyAttributeEnum.translate;
                    break;
                case "xmlns":
                    familyAttribute = FamilyAttributeEnum.xmlns;
                    break;
                case "scheme":
                    familyAttribute = FamilyAttributeEnum.scheme;
                    break;
                case "title":
                    familyAttribute = FamilyAttributeEnum.title;
                    break;
                case "type":
                    familyAttribute = FamilyAttributeEnum.type;
                    break;
                case "media":
                    familyAttribute = FamilyAttributeEnum.media;
                    break;
                case "hreflang":
                    familyAttribute = FamilyAttributeEnum.hreflang;
                    break;
                case "crossorigin":
                    familyAttribute = FamilyAttributeEnum.crossorigin;
                    break;
                default:
                    parseIsOk = false;
                    break;
            }
            if (!parseIsOk)
                throw new AttributeTagParserException(ErrorTypeParsing.attributes, $"We fail to parse the attributes of {_html}");

            return familyAttribute;
        }

        public bool IsAttributePresent(string html)
        {
            _html = html;
            _identifyStartTagEndTag.DetermineStartEndTags(html);
            _startTag = _identifyStartTagEndTag.StartTag;
            bool isDoctype = _startTag.ToLower().Contains("doctype");
            bool isAttributeHere = false;

            if (!isDoctype)
            {
                string startTagWithoutBracket = _startTag.Replace("<", string.Empty).Replace(">", string.Empty);
                var partStartTagWithoutBracket = startTagWithoutBracket.Split("=");

                isAttributeHere = partStartTagWithoutBracket.Length > 1;
            }

            if (isDoctype)
                return false;
            else
                return isAttributeHere;
        }
    }
}