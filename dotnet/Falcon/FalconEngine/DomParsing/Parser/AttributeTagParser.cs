using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    //TODO add exception
    public class AttributeTagParser : IAttributeTagParser
    {
        private string _html { get; set; }
        private string _startTag { get; set; }
        private List<AttributeModel> _attributes { get; set; }

        public List<AttributeModel> Parse(string html)
        {
            _attributes = new List<AttributeModel>();
            _html = html;
            GetStartTag();
            GetAttributes();
            return _attributes;
        }

        private void GetStartTag()
        {

            int startStartTag = 0;
            int finishStartTag = 0;
            for (int i = 0; i < _html.Length; i++)
            {
                char caracter = _html[i];
                if (caracter == '<')
                    startStartTag = i;
                else if (caracter == '>')
                {
                    finishStartTag = i;
                    break;
                }
            }
            _startTag = _html.Substring(startStartTag, finishStartTag - startStartTag + 1);

        }

        private void GetAttributes()
        {
            var attributes = _startTag.Split("\" ");
            for (int i = 0; i < attributes.Length; i++)
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
                Value = CalculateValueAttribute(candidate, firstElementToMove)
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
                case "lang":
                    familyAttribute = FamilyAttributeEnum.lang;
                    break;
                case "name":
                    familyAttribute = FamilyAttributeEnum.name;
                    break;
                case "rel":
                    familyAttribute = FamilyAttributeEnum.rel;
                    break;
                case "id":
                    familyAttribute = FamilyAttributeEnum.id;
                    break;
                case "charset":
                    familyAttribute = FamilyAttributeEnum.charset;
                    break;
                case "content":
                    familyAttribute = FamilyAttributeEnum.content;
                    break;
                case "href":
                    familyAttribute = FamilyAttributeEnum.href;
                    break;
                case "classCss":
                    familyAttribute = FamilyAttributeEnum.classCss;
                    break;
                case "dir":
                    familyAttribute = FamilyAttributeEnum.dir;
                    break;
                case "xmlns":
                    familyAttribute = FamilyAttributeEnum.xmlns;
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
            GetStartTag();
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