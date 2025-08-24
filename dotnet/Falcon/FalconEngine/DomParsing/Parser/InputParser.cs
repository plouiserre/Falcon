using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class InputParser : TagParser, ITagParser
    {
        private IIdentifyTag _identifyTag;

        public InputParser(IIdentifyTag identifyTag, IAttributeTagManager attributeTagManager) : base(attributeTagManager, NameTagEnum.input)
        {
            _identifyTag = identifyTag;
        }

        public override TagModel Parse(string html)
        {
            _tag = _identifyTag.Analyze(html);
            return _tag;
        }

        public override bool IsValid()
        {
            bool basiqueValidation = base.IsValid();
            bool typeValueValidation = ValidateValueType();
            bool acceptRuleValidation = AcceptAttributeValidOnlyWithFileType();
            bool altRuleValidation = AltAttributeValidOnlyWithImageType();
            bool autocapitalizeValidation = AutoCapitalizeAttributeValidAllTypeExceptEmailPasswordUrl();
            bool autocompleteValidation = AutocompleteAttributeValidAllTypeExceptButtonCheckboxRadio();
            bool captureValidation = CaptureAttributeValidWithFileType();
            bool checkedValidation = CheckedAttributeValidWithCheckboxRadioType();
            bool dirnameValidation = DirnameAttributeValidWithItsAttribute();
            bool formactionValidation = FormActionAttributeValidWithImageSubmit();
            bool formenctypeValidation = FormenctypeAttributeValidWithImageSubmit();
            bool formMethodValidation = FormMethodAttributeValidWithImageSubmit();
            bool formNoValidateValidation = FormNoValidateAttributeValidWithImageSubmit();
            bool formtargetValidation = FormTargetAttributeValidWithImageSubmit();
            bool heightValidation = HeigthAttributeValidWithImage();
            bool listValidation = ListAttributeValidWithImage();
            bool maxValidation = MaxAttributeValidation();
            bool maxLengthValidation = MaxLengthAttributeValidation();
            bool minValidation = MinAttributeValidation();
            bool minLengthValidation = MinLengthAttributeValidation();
            bool multipleValidation = MultipleAttributeValidation();
            bool patternValidation = MultiplePatternValidation();
            bool placeholderValidation = MultiplePlaceholderValidation();
            return basiqueValidation && typeValueValidation && acceptRuleValidation && altRuleValidation
                && autocapitalizeValidation && autocompleteValidation && captureValidation &&
                checkedValidation && dirnameValidation && formactionValidation && formenctypeValidation &&
                formMethodValidation && formNoValidateValidation && formtargetValidation && heightValidation
                && listValidation && maxValidation && maxLengthValidation && minValidation && minLengthValidation
                && multipleValidation && patternValidation && placeholderValidation;
        }

        private bool ValidateValueType()
        {
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            if (!isTypeAttributePresente)
                return true;
            else
            {
                string[] valueTypeOk = new string[] { "button", "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "image", "month", "number", "password", "radio", "range", "reset", "search", "submit", "tel", "text", "time", "url", "week" };
                string valueType = _tag.Attributes.First(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString()).Value.ToLower();
                return valueTypeOk.Contains(valueType);
            }

        }

        private bool AcceptAttributeValidOnlyWithFileType()
        {
            string[] acceptedTypeValues = new string[] { "file" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.accept.ToString(), acceptedTypeValues);
        }

        private bool AltAttributeValidOnlyWithImageType()
        {
            string[] acceptedTypeValues = new string[] { "image" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.alt.ToString(), acceptedTypeValues);
        }

        private bool AutoCapitalizeAttributeValidAllTypeExceptEmailPasswordUrl()
        {
            string[] notAcceptedTypeValue = new string[] { "email", "password", "url" };
            return CheckIfAttributesWithNotThisTypes(FamilyAttributeEnum.autocapitalize.ToString(), notAcceptedTypeValue);
        }

        private bool AutocompleteAttributeValidAllTypeExceptButtonCheckboxRadio()
        {
            string[] notAcceptedTypeValue = new string[] { "button", "checkbox", "radio" };
            return CheckIfAttributesWithNotThisTypes(FamilyAttributeEnum.autocomplete.ToString(), notAcceptedTypeValue);
        }

        private bool CaptureAttributeValidWithFileType()
        {
            string[] acceptedTypeValues = new string[] { "file" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.capture.ToString(), acceptedTypeValues);
        }

        private bool CheckedAttributeValidWithCheckboxRadioType()
        {
            string[] acceptedTypeValues = new string[] { "checkbox", "radio" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.checkedAttr.ToString(), acceptedTypeValues);
        }

        private bool DirnameAttributeValidWithItsAttribute()
        {
            string[] acceptedTypeValues = new string[] { "hidden", "text", "search", "url", "tel", "email" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.dirname.ToString(), acceptedTypeValues);
        }

        private bool FormActionAttributeValidWithImageSubmit()
        {
            string[] acceptedTypeValues = new string[] { "image", "submit" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.formaction.ToString(), acceptedTypeValues);
        }

        private bool FormenctypeAttributeValidWithImageSubmit()
        {
            string[] acceptedTypeValues = new string[] { "image", "submit" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.formenctype.ToString(), acceptedTypeValues);
        }

        private bool FormMethodAttributeValidWithImageSubmit()
        {
            string[] acceptedTypeValues = new string[] { "image", "submit" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.formmethod.ToString(), acceptedTypeValues);
        }

        private bool FormNoValidateAttributeValidWithImageSubmit()
        {
            string[] acceptedTypeValues = new string[] { "image", "submit" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.formnovalidate.ToString(), acceptedTypeValues);
        }

        private bool FormTargetAttributeValidWithImageSubmit()
        {
            string[] acceptedTypeValues = new string[] { "image", "submit" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.formtarget.ToString(), acceptedTypeValues);
        }

        private bool HeigthAttributeValidWithImage()
        {
            string[] acceptedTypeValues = new string[] { "image" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.height.ToString(), acceptedTypeValues);
        }

        private bool ListAttributeValidWithImage()
        {
            string[] notAcceptedTypeValue = new string[] { "button", "checkbox", "hidden", "password", "radio" };
            return CheckIfAttributesWithNotThisTypes(FamilyAttributeEnum.list.ToString(), notAcceptedTypeValue);
        }

        private bool MaxAttributeValidation()
        {
            string[] acceptedTypeValues = new string[] { "date", "datetime-local", "month", "number", "range", "time", "week" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.max.ToString(), acceptedTypeValues);
        }

        private bool MaxLengthAttributeValidation()
        {
            string[] acceptedTypeValues = new string[] { "email", "password", "search", "url", "tel", "text" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.maxlength.ToString(), acceptedTypeValues);
        }

        private bool MinAttributeValidation()
        {
            string[] acceptedTypeValues = new string[] { "date", "datetime-local", "month", "number", "range", "time", "week" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.min.ToString(), acceptedTypeValues);
        }

        private bool MinLengthAttributeValidation()
        {
            string[] acceptedTypeValues = new string[] { "email", "password", "search", "url", "tel", "text" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.minlength.ToString(), acceptedTypeValues);
        }

        private bool MultipleAttributeValidation()
        {
            string[] acceptedTypeValues = new string[] { "email", "file" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.multiple.ToString(), acceptedTypeValues);
        }

        private bool MultiplePatternValidation()
        {
            string[] acceptedTypeValues = new string[] { "email", "password", "search", "tel", "text", "url" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.pattern.ToString(), acceptedTypeValues);
        }

        private bool MultiplePlaceholderValidation()
        {
            string[] acceptedTypeValues = new string[] { "email", "number", "password", "search", "tel", "text", "url" };
            return CheckIfAttributesWithThisTypes(FamilyAttributeEnum.placeholder.ToString(), acceptedTypeValues);
        }


        private bool CheckIfAttributesWithThisTypes(string type, string[] typesOK)
        {
            bool isCheckedAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == type.ToString());
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty;
            if (!isCheckedAttributePresente)
                return true;
            else
            {
                if (typesOK.Contains(typeValue))
                    return true;
                else
                    return false;
            }
        }

        private bool CheckIfAttributesWithNotThisTypes(string type, string[] typesForbidden)
        {
            bool isCheckedAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == type);
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty;
            if (!isCheckedAttributePresente)
                return true;
            else
            {
                if (typesForbidden.Contains(typeValue))
                    return false;
                else
                {
                    return true;
                }
            }
        }
    }
}