using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineCourses.API.Filters {
    public class ExceptionFilter : ExceptionFilterAttribute {

        public override void OnException(ExceptionContext context) {
            if (context.Exception != null) {

                var message = context.Exception.InnerException != null ?
                    context.Exception.InnerException.Message :
                    context.Exception.Message;

                var jsonResult = new JsonResult(new { error = message }) {
                    StatusCode = (int)System.Net.HttpStatusCode.InternalServerError
                };

                context.Result = jsonResult;
            }
        }
    }
}
