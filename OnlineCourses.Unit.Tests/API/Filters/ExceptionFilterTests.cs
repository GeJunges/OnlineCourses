using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NUnit.Framework;
using OnlineCourses.API.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace OnlineCourses.Unit.Tests.API.Filters {
    public class ExceptionFilterTests {

        private ExceptionFilter _exceptionFilter;

        public ExceptionContext _exceptionContext;
        
        [SetUp]
        public void SetUp() {
            
            _exceptionContext = new ExceptionContext(
            new ActionContext {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            },
            new List<IFilterMetadata>());

            _exceptionFilter = new ExceptionFilter();
        }

        [Test]
        public void OnException_ShoulNotChangeContextResultIfNoException() {

            _exceptionFilter.OnException(_exceptionContext);

            Assert.IsNull(_exceptionContext.Result);
        }

        [Test]
        public void OnException_ShouldSetResultToInternalServerErrorIfException() {
            _exceptionContext.Exception = new Exception("Error");

            _exceptionFilter.OnException(_exceptionContext);

            var actual = ((JsonResult)_exceptionContext.Result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, actual.StatusCode);
        }
    }
}
