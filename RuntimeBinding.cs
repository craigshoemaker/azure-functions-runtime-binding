using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Demos
{
    public static class RuntimeBinding
    {
        [FunctionName("SaveCustom")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            IBinder binder,
            ILogger log)
        {
            string message = req.Query["message"];

            var attribute = new BlobAttribute("messages/{sys.randguid}.txt", FileAccess.Write);
            attribute.Connection = "AzureStorageConnectionString";

            using(var blob = await binder.BindAsync<TextWriter>(attribute))
            {
                blob.Write($"Runtime binding: {message}");
            }

            return new OkResult();
        }
    }
}
