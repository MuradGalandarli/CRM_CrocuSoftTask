using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shred.Layer.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Helper
{
    public static class ErrorManager
    {
       
       public static ErrorResponse ErrorHandling(int statusCode, List<string> propertyName = null)
        {
            switch(statusCode)
            {
                case 400: 
                    {
                        return new ErrorResponse
                        {   StatusCode = 400,
                            Error = "Bad Request",
                            Message = $"Missing required fields: {propertyName[0]}" 

                        };
                    }
                case 404:
                    {
                        return new ErrorResponse
                        {
                            StatusCode = 404,
                            Error = "Not Found",
                            Message = "Resource not found"

                        };
                    }
                case 401:
                    {
                        return new ErrorResponse
                        {
                            StatusCode = 401,
                            Error = "Unauthorized",
                            Message = "Invalid or missing JWT token."

                        };
                    }
                case 500:
                    {
                        return new ErrorResponse
                        {
                            StatusCode = 500,
                            Error = "Internal Server",
                            Message = "Server error"

                        };
                    }
            }
            return default;
        }
    }
}
