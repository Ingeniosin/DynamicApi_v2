﻿using System.Net;

namespace DynamicApi.Exceptions;

public class ApiException : Exception {
    
    public HttpStatusCode StatusCode { get; set; }
    
    public ApiException(string message, HttpStatusCode statusCode) : base(message) {
        StatusCode = statusCode;
    }

    

}