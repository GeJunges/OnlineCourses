using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using OnlineCourses.API.Filters;
using System.Collections.Generic;
using System.Net;

namespace OnlineCourses.Unit.Tests.API.Filters {
    public class ValidateModelAttributeTests {
               
        private ActionExecutingContext _actionContextMock;
        private ValidateModelAttribute _validateModelAttribute;

        [SetUp]
        public void SetUp() {
        
            _actionContextMock = new ActionExecutingContext(
            new ActionContext {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            },
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            new Mock<Controller>().Object);

            _validateModelAttribute = new ValidateModelAttribute();
        }

        [Test]
        public void OnActionExecuting_ShouldNotChangeContextResultIfModelStateIsValid() {
            _validateModelAttribute.OnActionExecuting(_actionContextMock);

            Assert.IsNull(_actionContextMock.Result);
        }

        [Test]
        public void OnActionExecuting_ShouldSetResultToInternalServerErrorIfModelStateIsNotValid() {
            _actionContextMock.ModelState.AddModelError("error", "error");

            _validateModelAttribute.OnActionExecuting(_actionContextMock);

            var actual = ((JsonResult)_actionContextMock.Result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, actual.StatusCode);
        }
    }
}