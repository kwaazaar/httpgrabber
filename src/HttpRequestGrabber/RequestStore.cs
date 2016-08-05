using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpRequestGrabber
{
    public class RequestInfo
    {
        public DateTime Received { get; set; }
        public string Method { get; set; }
        public string RequestUri { get; set; }
        public IEnumerable<string> Headers { get; set; }
        public string Body { get; set; }
    }

    public static class RequestStore
    {
        private static ConcurrentStack<RequestInfo> Requests { get; } = new ConcurrentStack<RequestInfo>();

        public static void Push(RequestInfo ri)
        {
            Requests.Push(ri);
            if (Requests.Count > 10)
            {
                RequestInfo riOut;
                Requests.TryPop(out riOut);
            }
        }

        public static IEnumerable<RequestInfo> GetAll()
        {
            RequestInfo ri;
            while (Requests.TryPop(out ri))
                yield return ri;
        }

        //public static void Clear()
        //{
        //    RequestInfo ri;
        //    while (Requests.TryTake(out ri));
        //}
    }
}
