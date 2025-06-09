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
            var attributs = ExtractAttributs(tagStart);
            List<FamilyAttributeEnum> attributesKey = FindKey(attributs);
            List<string> valueAttributes = GetValueAttributs(attributs);
            var attributsModel = new List<AttributeModel>();
            for (int i = 0; i < attributs.Count; i++)
            {
                var attribute = new AttributeModel()
                {
                    FamilyAttribute = attributesKey[i],
                    Value = valueAttributes[i]
                };
                attributsModel.Add(attribute);
            }
            return attributsModel;
        }

        private List<string> ExtractAttributs(string tagStart)
        {
            var attributs = new List<string>();
            var tagsElement = tagStart.Split(' ');
            for (int i = 0; i < tagsElement.Count(); i++)
            {
                if (i == 0)
                    continue;
                string element = tagsElement[i];
                string elementClean = element.Replace(">", string.Empty);
                attributs.Add(elementClean);
            }
            return attributs;
        }

        private List<FamilyAttributeEnum> FindKey(List<string> attributs)
        {
            var keys = new List<FamilyAttributeEnum>();
            foreach (var attribut in attributs)
            {
                string attributKey = attribut.Split('=')[0];
                switch (attributKey)
                {
                    case "lang":
                        keys.Add(FamilyAttributeEnum.lang);
                        break;
                    case "name":
                        keys.Add(FamilyAttributeEnum.name);
                        break;
                    case "rel":
                        keys.Add(FamilyAttributeEnum.rel);
                        break;
                    case "id":
                        keys.Add(FamilyAttributeEnum.id);
                        break;
                    case "charset":
                        keys.Add(FamilyAttributeEnum.charset);
                        break;
                    case "content":
                        keys.Add(FamilyAttributeEnum.content);
                        break;
                    case "href":
                        keys.Add(FamilyAttributeEnum.href);
                        break;
                    case "dir":
                        keys.Add(FamilyAttributeEnum.dir);
                        break;
                    case "xmlns":
                        keys.Add(FamilyAttributeEnum.xmlns);
                        break;
                    case "classCss":
                    default:
                        keys.Add(FamilyAttributeEnum.classCss);
                        break;
                }
            }
            return keys;
        }

        private List<string> GetValueAttributs(List<string> attributs)
        {
            List<string> valueAttributes = new List<string>();
            foreach (var attribut in attributs)
            {
                string valueAttributeNotClean = attribut.Split('=')[1];
                string valueAttribute = valueAttributeNotClean.Replace("\"", string.Empty);
                valueAttributes.Add(valueAttribute);
            }
            return valueAttributes;
        }
    }
}