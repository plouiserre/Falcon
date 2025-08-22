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
            return basiqueValidation && typeValueValidation && acceptRuleValidation && altRuleValidation
                && autocapitalizeValidation && autocompleteValidation && captureValidation &&
                checkedValidation && dirnameValidation && formactionValidation && formenctypeValidation &&
                formMethodValidation && formNoValidateValidation && formtargetValidation && heightValidation
                && listValidation;
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
            bool isAcceptAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.accept.ToString());
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty;
            if (!isAcceptAttributePresente)
                return true;
            else
            {
                if (isTypeAttributePresente && typeValue == "file")
                    return true;
                else
                    return false;
            }
        }

        private bool AltAttributeValidOnlyWithImageType()
        {
            bool isAltAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.alt.ToString());
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty;
            if (!isAltAttributePresente)
                return true;
            else
            {
                if (isTypeAttributePresente && typeValue == "image")
                    return true;
                else
                    return false;
            }
        }

        private bool AutoCapitalizeAttributeValidAllTypeExceptEmailPasswordUrl()
        {
            bool isAutocapitalizeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.autocapitalize.ToString());
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty;
            if (!isAutocapitalizeAttributePresente)
                return true;
            else
            {
                string[] notAcceptedTypeValue = new string[] { "email", "password", "url" };
                if (isTypeAttributePresente && typeValue == "autocapitalize")
                    return true;
                else
                {
                    bool isTypeIsOk = !notAcceptedTypeValue.Contains(typeValue);
                    return isTypeIsOk;
                }
            }
        }

        private bool AutocompleteAttributeValidAllTypeExceptButtonCheckboxRadio()
        {
            bool isAutocompleteAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.autocomplete.ToString());
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty;
            if (!isAutocompleteAttributePresente)
                return true;
            else
            {
                string[] notAcceptedTypeValue = new string[] { "button", "checkbox", "radio" };
                if (isTypeAttributePresente && typeValue == "autocomplete")
                    return true;
                else
                {
                    bool isTypeIsOk = !notAcceptedTypeValue.Contains(typeValue);
                    return isTypeIsOk;
                }
            }
        }

        private bool CaptureAttributeValidWithFileType()
        {
            bool isCaptureAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.capture.ToString());
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty; if (!isCaptureAttributePresente)
                return true;
            else
            {
                if (isTypeAttributePresente && typeValue == "file")
                    return true;
                else
                    return false;
            }
        }

        private bool CheckedAttributeValidWithCheckboxRadioType()
        {
            bool isCheckedAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.checkedAttr.ToString());
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty;
            if (!isCheckedAttributePresente)
                return true;
            else
            {
                string[] acceptedTypeValue = new string[] { "checkbox", "radio" };
                if (acceptedTypeValue.Contains(typeValue))
                    return true;
                else
                {
                    return false;
                }
            }
        }

        private bool DirnameAttributeValidWithItsAttribute()
        {
            bool isCheckedAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.dirname.ToString());
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty;
            if (!isCheckedAttributePresente)
                return true;
            else
            {
                string[] acceptedTypeValue = new string[] { "hidden", "text", "search", "url", "tel", "email" };
                if (acceptedTypeValue.Contains(typeValue))
                    return true;
                else
                {
                    return false;
                }
            }
        }

        private bool FormActionAttributeValidWithImageSubmit()
        {
            bool isCheckedAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.formaction.ToString());
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty;
            if (!isCheckedAttributePresente)
                return true;
            else
            {
                string[] acceptedTypeValue = new string[] { "image", "submit" };
                if (acceptedTypeValue.Contains(typeValue))
                    return true;
                else
                {
                    return false;
                }
            }
        }

        private bool FormenctypeAttributeValidWithImageSubmit()
        {
            bool isCheckedAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.formenctype.ToString());
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty;
            if (!isCheckedAttributePresente)
                return true;
            else
            {
                string[] acceptedTypeValue = new string[] { "image", "submit" };
                if (acceptedTypeValue.Contains(typeValue))
                    return true;
                else
                {
                    return false;
                }
            }
        }

        private bool FormMethodAttributeValidWithImageSubmit()
        {
            bool isCheckedAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.formmethod.ToString());
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty;
            if (!isCheckedAttributePresente)
                return true;
            else
            {
                string[] acceptedTypeValue = new string[] { "image", "submit" };
                if (acceptedTypeValue.Contains(typeValue))
                    return true;
                else
                {
                    return false;
                }
            }
        }

        private bool FormNoValidateAttributeValidWithImageSubmit()
        {
            bool isCheckedAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.formnovalidate.ToString());
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty;
            if (!isCheckedAttributePresente)
                return true;
            else
            {
                string[] acceptedTypeValue = new string[] { "image", "submit" };
                if (acceptedTypeValue.Contains(typeValue))
                    return true;
                else
                {
                    return false;
                }
            }
        }

        private bool FormTargetAttributeValidWithImageSubmit()
        {
            bool isCheckedAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.formtarget.ToString());
            bool isTypeAttributePresente = _tag.Attributes.Any(o => o.FamilyAttribute == FamilyAttributeEnum.type.ToString());
            string typeValue = isTypeAttributePresente ? _tag.Attributes.First(o => o.FamilyAttribute ==
                            FamilyAttributeEnum.type.ToString()).Value : string.Empty;
            if (!isCheckedAttributePresente)
                return true;
            else
            {
                string[] acceptedTypeValue = new string[] { "image", "submit" };
                if (acceptedTypeValue.Contains(typeValue))
                    return true;
                else
                {
                    return false;
                }
            }
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
                {
                    return false;
                }
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