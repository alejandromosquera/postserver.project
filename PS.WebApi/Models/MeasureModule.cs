using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Hosting;

namespace PS.WebApi.Models
{
    public class MeasureModule : IHttpModule
    {
        private static readonly ReaderWriterLockSlim Gateway = new ReaderWriterLockSlim();

        public void Dispose()
        {            
        }

        public void Init(HttpApplication context)
        {          
            context.BeginRequest += (sender, e) =>
            {
                var httpApplication = (sender as HttpApplication);

                var stopwatch = Stopwatch.StartNew();

                if (httpApplication != null) 
                    httpApplication.Context.Items["watch"] = stopwatch;
            };

            context.EndRequest += (sender, e) =>
            {
                var httpApplication = (sender as HttpApplication);

                if (httpApplication != null)
                {
                    var stopwatch = httpApplication.Context.Items["watch"] as Stopwatch;

                    if (stopwatch != null)
                    {
                        stopwatch.Stop();
                        var url = httpApplication.Context.Request.Url.AbsoluteUri;
                        var message = string.Format("url: {0}, time: {1}ms", url, stopwatch.ElapsedMilliseconds);
                        var log = HostingEnvironment.MapPath("~/RequestLog.txt");
                        Gateway.EnterWriteLock();
                        try
                        {
                            if (log != null) 
                                File.AppendAllLines(log, new[] { message });
                        }
                        finally
                        {
                            Gateway.ExitWriteLock();
                        }
                    }
                }
            };
        }
    }
}