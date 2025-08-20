using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngineTest.Utils.AssertHtml
{
    public static class AssertFormPage
    {
        public static void AssertInputSubmit(TagModel tag)
        {
            Assert.Equal(NameTagEnum.input, tag.NameTag);
            Assert.Null(tag.Content);
            Assert.Equal(FamilyAttributeEnum.type.ToString(), tag.Attributes[0].FamilyAttribute);
            Assert.Equal("Submit", tag.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.value.ToString(), tag.Attributes[1].FamilyAttribute);
            Assert.Equal("Submit", tag.Attributes[1].Value);
            Assert.Equal(TagFamilyEnum.NoEnd, tag.TagFamily);
            Assert.Equal("<input type=\"Submit\" value=\"Submit\"/>", tag.TagStart);
            Assert.Null(tag.TagEnd);
            Assert.Null(tag.Children);
        }

        public static void AssertInputFile(TagModel tag)
        {
            Assert.Equal(NameTagEnum.input, tag.NameTag);
            Assert.Null(tag.Content);
            Assert.Equal(FamilyAttributeEnum.type.ToString(), tag.Attributes[0].FamilyAttribute);
            Assert.Equal("file", tag.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.id.ToString(), tag.Attributes[1].FamilyAttribute);
            Assert.Equal("avatar", tag.Attributes[1].Value);
            Assert.Equal(FamilyAttributeEnum.name.ToString(), tag.Attributes[2].FamilyAttribute);
            Assert.Equal("avatar", tag.Attributes[1].Value);
            Assert.Equal(FamilyAttributeEnum.accept.ToString(), tag.Attributes[3].FamilyAttribute);
            Assert.Equal(".doc,.docx,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document", tag.Attributes[3].Value);
            Assert.Equal(TagFamilyEnum.NoEnd, tag.TagFamily);
            Assert.Equal("<input type=\"file\" id=\"avatar\" name=\"avatar\" accept=\".doc,.docx,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document\">", tag.TagStart);
            Assert.Null(tag.TagEnd);
            Assert.Null(tag.Children);
        }

        public static void AssertRadioUndefined(TagModel tag)
        {
            Assert.Equal(NameTagEnum.input, tag.NameTag);
            Assert.Null(tag.Content);
            Assert.Equal(FamilyAttributeEnum.type.ToString(), tag.Attributes[0].FamilyAttribute);
            Assert.Equal("radio", tag.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.id.ToString(), tag.Attributes[1].FamilyAttribute);
            Assert.Equal("rgender", tag.Attributes[1].Value);
            Assert.Equal(FamilyAttributeEnum.name.ToString(), tag.Attributes[2].FamilyAttribute);
            Assert.Equal("rgender", tag.Attributes[1].Value);
            Assert.Equal(FamilyAttributeEnum.value.ToString(), tag.Attributes[3].FamilyAttribute);
            Assert.Equal("undefined", tag.Attributes[3].Value);
            Assert.Equal(FamilyAttributeEnum.checkedAttr.ToString(), tag.Attributes[4].FamilyAttribute);
            Assert.Null(tag.Attributes[4].Value);
            Assert.Equal(TagFamilyEnum.NoEnd, tag.TagFamily);
            Assert.Equal("<input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"undefined\" checked/>", tag.TagStart);
            Assert.Null(tag.TagEnd);
            Assert.Null(tag.Children);
        }

        public static void AssertInputFirstName(TagModel tag)
        {
            Assert.Equal(NameTagEnum.input, tag.NameTag);
            Assert.Null(tag.Content);
            Assert.Equal(FamilyAttributeEnum.type.ToString(), tag.Attributes[0].FamilyAttribute);
            Assert.Equal("text", tag.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.placeholder.ToString(), tag.Attributes[1].FamilyAttribute);
            Assert.Equal("FirstName", tag.Attributes[1].Value);
            Assert.Equal(TagFamilyEnum.NoEnd, tag.TagFamily);
            Assert.Equal("<input type=\"text\" placeholder=\"FirstName\">", tag.TagStart);
            Assert.Null(tag.TagEnd);
            Assert.Null(tag.Children);
        }

        public static void AssertSelectSituation(TagModel tag)
        {
            Assert.Equal(NameTagEnum.select, tag.NameTag);
            Assert.Equal("<option>No Job</option><option>Job in a company</option><option>Entrepreneur</option>", tag.Content);
            Assert.Equal(FamilyAttributeEnum.name.ToString(), tag.Attributes[0].FamilyAttribute);
            Assert.Equal("sSituation", tag.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.id.ToString(), tag.Attributes[1].FamilyAttribute);
            Assert.Equal("sSituation", tag.Attributes[1].Value);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("<select name=\"sSituation\" id=\"sSituation\">", tag.TagStart);
            Assert.Equal("</select>", tag.TagEnd);
            Assert.Equal(3, tag.Children.Count);
            AssertFirstOption(tag.Children[0]);
            AssertSecondOption(tag.Children[1]);
            AssertThirdOption(tag.Children[2]);
        }

        private static void AssertFirstOption(TagModel tag)
        {
            Assert.Equal(NameTagEnum.option, tag.NameTag);
            Assert.Equal("No Job", tag.Content);
            Assert.Null(tag.Attributes);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("<option>", tag.TagStart);
            Assert.Equal("</option>", tag.TagEnd);
            Assert.Null(tag.Children);
        }

        private static void AssertSecondOption(TagModel tag)
        {
            Assert.Equal(NameTagEnum.option, tag.NameTag);
            Assert.Equal("Job in a company", tag.Content);
            Assert.Null(tag.Attributes);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("<option>", tag.TagStart);
            Assert.Equal("</option>", tag.TagEnd);
            Assert.Null(tag.Children);
        }

        private static void AssertThirdOption(TagModel tag)
        {
            Assert.Equal(NameTagEnum.option, tag.NameTag);
            Assert.Equal("Entrepreneur", tag.Content);
            Assert.Null(tag.Attributes);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("<option>", tag.TagStart);
            Assert.Equal("</option>", tag.TagEnd);
            Assert.Null(tag.Children);
        }
    }
}