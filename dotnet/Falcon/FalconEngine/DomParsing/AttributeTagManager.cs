using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{

    public class AttributeTagManager : IAttributeTagManager
    {
        private Dictionary<NameTagEnum, List<FamilyAttributeEnum>> _allAttributesAutorizedByTag;

        public AttributeTagManager()
        {
            _allAttributesAutorizedByTag = new Dictionary<NameTagEnum, List<FamilyAttributeEnum>>();
            _allAttributesAutorizedByTag[NameTagEnum.html] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.meta] = new List<FamilyAttributeEnum>();
        }

        public List<FamilyAttributeEnum> GetAttributes(NameTagEnum tag)
        {
            return _allAttributesAutorizedByTag[tag];
        }

        public void SetAttributes()
        {
            SetHtmlAttributesAutorized();
            SetMetaAttributesAutorized();
        }

        private void SetHtmlAttributesAutorized()
        {
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.lang);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.lang);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.dir);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.xmlns);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.manifest);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.style);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.id);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.classCss);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.datapage);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.datatheme);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.datauser);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.accesskey);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.tabindex);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.contenteditable);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.draggable);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.spellcheck);
        }

        private void SetMetaAttributesAutorized()
        {
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.charset);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.name);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.content);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.httpequiv);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.scheme);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.id);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.classCss);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.datapage);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.datatheme);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.datauser);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.lang);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.dir);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.title);
        }
    }
}