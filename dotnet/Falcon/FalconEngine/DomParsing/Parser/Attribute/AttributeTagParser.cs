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
            string familyAttribute = GetFamilyAttributeEnum(key);
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

        private string GetFamilyAttributeEnum(string key)
        {
            FamilyAttributeEnum familyAttribute = FamilyAttributeEnum.lang;
            key = ManageAttributsCssNeedTransformationBeforeConversationEnum(key);
            bool parseIsOk = Enum.TryParse(key, out familyAttribute);
            string familyAttributeValue = string.Empty;
            if (!parseIsOk)
            {
                if (key.Contains("data"))
                    familyAttributeValue = key;
                else
                    throw new AttributeTagParserException(ErrorTypeParsing.attributes, $"We fail to parse the attributes of {_html}");
            }

            familyAttributeValue = familyAttributeValue == string.Empty ? familyAttribute.ToString() : familyAttributeValue;
            return familyAttributeValue;
        }

        private string ManageAttributsCssNeedTransformationBeforeConversationEnum(string key)
        {
            if (key == "class")
                return "classCss";
            else if (key == "as")
                return "asAttr";
            else if (key == "http-equiv")
                return "httpequiv";
            else if (key == "is")
                return "isAttr";
            else if (key == "checked")
                return "checkedAttr";
            else
                return key;
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