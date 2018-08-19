using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace OnlineCourses.API.Filters {
    public class ValidateModelAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext context) {
            if (!context.ModelState.IsValid) {
                var errors = context.ModelState.ToDictionary(e => e.Key, e => e.Value.Errors);

                var jsonResult = new JsonResult(new { error = errors }) {
                    StatusCode = (int)System.Net.HttpStatusCode.InternalServerError
                };
                context.Result = jsonResult;
            }
        }
    }
}