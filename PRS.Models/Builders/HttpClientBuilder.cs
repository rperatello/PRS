using PRS.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PRS.Models.Builders
{
    public class HttpClientBuilder : IBuilder<HttpClient>
    {
        private readonly HttpClient httpClient;

        public HttpClientBuilder()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("PRS6", "1.0"));
        }

        public HttpClientBuilder ClientIP(string host)
        {
            httpClient.DefaultRequestHeaders.Add("ClientIP", host);
            return this;
        }

        public HttpClientBuilder Host(string uri)
        {
            httpClient.DefaultRequestHeaders.Host = new Uri(uri).Host;
            return this;
        }

        public HttpClientBuilder Port(int port)
        {
            httpClient.BaseAddress = new Uri($"http://localhost:{port}/");
            return this;
        }

        public HttpClient Build()
        {
            return httpClient;
        }

    }
}
