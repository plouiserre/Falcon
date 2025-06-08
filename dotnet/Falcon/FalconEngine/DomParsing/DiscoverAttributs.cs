using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public class DiscoverAttributs
    {
        public List<AttributeModel> Find(string tagStart)
        {
            string attributs = ExtractAttributs(tagStart);
            FamilyAttributeEnum attributeKey = FindKey(attributs);
            string valueAttribute = GetValueAttribute(attributs);
            var attribute = new AttributeModel()
            {
                FamilyAttribute = attributeKey,
                Value = valueAttribute
            };
            return new List<AttributeModel>() { attribute };
        }

        private string ExtractAttributs(string tagStart)
        {
            string withoutStartTagStart = tagStart.Split(' ')[1];
            string attributs = withoutStartTagStart.Replace(">", string.Empty);
            return attributs;
        }

        private FamilyAttributeEnum FindKey(string attributs)
        {
            string attribut = attributs.Split('=')[0];
            switch (attribut)
            {
                case "lang":
                    return FamilyAttributeEnum.lang;
                case "name":
                    return FamilyAttributeEnum.name;
                case "rel":
                    return FamilyAttributeEnum.rel;
                case "id":
                    return FamilyAttributeEnum.id;
                case "charset":
                    return FamilyAttributeEnum.charset;
                case "content":
                    return FamilyAttributeEnum.content;
                case "href":
                    return FamilyAttributeEnum.href;
                case "classCss":
                default:
                    return FamilyAttributeEnum.classCss;
            }
        }

        private string GetValueAttribute(string attributs)
        {
            string valueAttributeNotClean = attributs.Split('=')[1];
            string valueAttribute = valueAttributeNotClean.Replace("\"", string.Empty);
            return valueAttribute;
        }
    }
}