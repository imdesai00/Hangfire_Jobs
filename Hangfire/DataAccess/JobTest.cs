using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Hangfire.InterFace;
using System;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hangfire.DataAccess
{
    public class JobTest: IJobTest
    {
        private readonly string? _connectionString;

        public JobTest(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("HangfireConnection");
        }
        public void FireAndForgetJob()
        {
            Console.WriteLine("Hello from a Fire and Forget job!");
        }

        public void ReccuringJob()
        {
            Console.WriteLine("Hello from Reccuring Job!");
        }

        public void Schedulejob()
        {
            Console.WriteLine("Hello from schedule Job!");
        }

    }
}
