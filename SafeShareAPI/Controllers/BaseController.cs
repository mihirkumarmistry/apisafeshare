using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafeShareAPI.Model;
using SafeShareAPI.Model.Common;
using SafeShareAPI.Provider;
using System.Text;

[assembly: ApiController]
namespace SafeShareAPI.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    public class BaseController : Controller
    {
        [NonAction]
        public static string GetMessage(Exceptions exceptions) { return FunctionProvider.AddSpaceBeforeCapital(Convert.ToString(exceptions)); }
        [NonAction]
        public ActionResult SendResponse(ApiResponse apiResponse) { return apiResponse.Status == (byte)Exceptions.Success ? Ok(apiResponse) : BadRequest(apiResponse); }
        [NonAction]
        public ActionResult SendResponse(Exceptions exception, object data = null)
        {
            ApiResponse response = new() { Data = data, Status = (byte)exception };
            return exception == Exceptions.Success ? Ok(response) : BadRequest(response);
        }
        [NonAction]
        public ActionResult SendResponseMessage(Exceptions exception, object data = null, string message = null)
        {
            ApiResponse response = new() { Data = data, Message = message ?? Convert.ToString(exception), Status = (byte)exception };
            return exception == Exceptions.Success ? Ok(response) : BadRequest(response);
        }

        [NonAction]
        public async Task<T> ParseRequestAsync<T>()
        {
            T reportRequest = default;
            using (StreamReader reader = new(Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false))
            { string request = await reader.ReadToEndAsync(); if (!string.IsNullOrWhiteSpace(request)) { reportRequest = JsonConvert.DeserializeObject<T>(request); } }
            return reportRequest;
        }
    }
}