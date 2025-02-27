﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManage.Helper
{
    public class EmployeeAPI
    {
        public HttpClient Initial() 
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            var client = new HttpClient(handler);

            client.BaseAddress = new Uri("https://192.168.100.18/");
            return client;
        
        }
    }
}
