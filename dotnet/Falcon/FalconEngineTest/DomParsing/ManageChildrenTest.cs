using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing
{
    public class ManageChildrenTest
    {
        [Fact]
        public void FindHeaderChildren()
        {
            var determinateChildren = TestFactory.InitDeterminateChildren();
            var parent = new TagModel();

            string contentHeadSimple = HtmlPageSimpleData.GetHtml(TagHtmlSimple.head).Replace("<head>", string.Empty).Replace("</head>", string.Empty);
            var headerChildren = determinateChildren.Identify(parent, contentHeadSimple);
            bool areValid = determinateChildren.ValidateChildren(parent);

            Assert.True(areValid);

            AssertCommon.AssertHeaderChildren(headerChildren);
        }

        [Fact]
        public void HeaderValidationFailedBecauseTitleIsNotValid()
        {

            var html = "<head><meta charset=\"UTF-8\"><title class=\"thetitle\">My Website</title></head>";
            var determinateChildren = TestFactory.InitDeterminateChildren();
            var parent = new TagModel();

            determinateChildren.Identify(parent, html);
            bool isValid = determinateChildren.ValidateChildren(parent);

            Assert.False(isValid);
        }

        [Fact]
        public void NoChildrenPresents()
        {
            var html = "A simple text";
            var determinateChildren = TestFactory.InitDeterminateChildren();

            var children = determinateChildren.Identify(new TagModel(), html);

            Assert.Null(children);
        }

    }
}