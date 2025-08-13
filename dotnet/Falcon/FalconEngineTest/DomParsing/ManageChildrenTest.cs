using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing
{
    public class ManageChildrenTest
    {
        [Fact]
        public void FindHeaderChildren()
        {
            var determinateChildren = TestFactory.InitDeterminateChildren();
            var parent = new TagModel();

            var headerChildren = determinateChildren.Identify(parent, HtmlData.ContentHeadSimple);
            bool areValid = determinateChildren.ValidateChildren(parent);

            Assert.True(areValid);

            AssertHtml.AssertHeaderChildren(headerChildren);
        }

        [Fact]
        public void ThrowsChildrenExceptionWhenParseChildrenGoesWrong()
        {
            string html = "<head><test<title>Document</title></head>";

            var determinateChildren = TestFactory.InitDeterminateChildren();
            var error = Assert.Throws<DeterminateChildrenException>(() => determinateChildren.Identify(new TagModel(), html));

            Assert.Equal(error.Message, $"Error parsing for the children of  {html}");
            Assert.Equal(ErrorTypeParsing.children, error.ErrorType);
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