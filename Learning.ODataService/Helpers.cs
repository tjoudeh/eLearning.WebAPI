using Microsoft.Data.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Learning.ODataService
{
    public static class Helpers
    {
        public static string RandomString(int size)
        {
            Random _rng = new Random((int)DateTime.Now.Ticks);
            string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }

        public static HttpResponseException ResourceNotFoundError(HttpRequestMessage request)
        {
            HttpResponseException httpException;
            HttpResponseMessage response;
            ODataError error;
            
            error = new ODataError
            {
                Message = "Resource Not Found - 404",
                ErrorCode = "NotFound"
            };

            response = request.CreateResponse(HttpStatusCode.NotFound, error);

            httpException = new HttpResponseException(response);

            return httpException;
        }
    }
}