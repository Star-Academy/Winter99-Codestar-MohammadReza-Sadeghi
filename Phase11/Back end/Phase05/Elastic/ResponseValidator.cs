using System;
using Nest;
using Phase05.IO;

namespace Phase05.Elastic
{
    public static class ResponseValidator
    {
        public static void Validate(this IResponse response)
        {
            if (response.IsValid)
                return;
            CheckOriginalException(response);
            CheckClientException(response);
            CheckServerError(response);
            throw new NotSupportedException();
        }

        private static void CheckServerError(IResponse response)
        {
            if (response.ServerError != null)
            {
                Output.PrintString("An error occurred inside of Elasticsearch server...");
                //Should we throw new exception?
            }
        }

        private static void CheckOriginalException(IResponse response)
        {
            if (response.ApiCall.OriginalException != null)
            {
                switch (response.ApiCall.HttpStatusCode)
                {
                    case 400:
                        Output.PrintString("Error 400: Bad Request\nYou probably sent an Invalid query.");
                        break;
                    case 401:
                        Output.PrintString("Error 401: Unauthorized\nThe authorization token was invalid.");
                        break;
                    case 403:
                        Output.PrintString("Error 403: Forbidden\nYou tried to access a resource you don't have access to.");
                        break;
                    case 404:
                        Output.PrintString("Error 404: Not Found\nThe requested resource or endpoint could not be found.");
                        break;
                    case 409:
                        Output.PrintString("Error 409: Conflict\nyou're trying to update an existing asset, entry or content type, and you didn't specify the current version of the object or specified an outdated version.");
                        break;
                    case 422:
                        Output.PrintString("Error 422: Unprocessable Entity\nPerhaps the entered value is invalid or the query refrences an unknown field.");
                        break;
                }
                throw response.ApiCall.OriginalException;
            }
        }

        private static void CheckClientException(IResponse response)
        {
            if (response.OriginalException != null)
            {
                Output.PrintString("An exception occurred in the request pipeline (such as max retries or timeout reached, bad authentication, etc...) or Elasticsearch itself returned an error (could not parse the request, bad query, missing field, etc...).");
                throw response.OriginalException;
            }
        }
    }
}
