using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Demos
{
    public static class DefaultBinding
    {
        [FunctionName("SaveDefault")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Blob("messages/{sys.randguid}.txt", FileAccess.Write)] out string blob,
            ILogger log)
        {
            string message = req.Query["message"];
            blob = $"Default binding: {message}";

            return new OkResult();
        }
    }
}
