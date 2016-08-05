using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.IO;
using System.Text;

namespace HttpRequestGrabber.Controllers
{
    [Route("api/grab")]
    public class RequestController : Controller
    {
        [Route("")]
        [HttpPost]
        [HttpPut]
        [HttpPatch]
        public Task<IActionResult> Grab()
        {
            RequestStore.Push(new RequestInfo
            {
                Received = DateTime.UtcNow,
                Method = Request.Method,
                RequestUri = Request.Path.Value,
                Headers = Request.Headers.Select(h => h.Key + " " + String.Join(" ", h.Value.ToArray())).ToArray(),
                Body = ReadBody(Request.Body),
            });
            return Task.FromResult<IActionResult>(Ok());
        }

        private string ReadBody(Stream body)
        {
            var sb = new StringBuilder();
            var sr = new StreamReader(body);
            while (!sr.EndOfStream)
                sb.AppendLine(sr.ReadLine());
            return sb.ToString();
        }
    }
}
