using System;

namespace PresentationLayer
{
    public enum EndpointAction
    {
        GET_BOOKS = 0,
        GET_USERS = 1,
        GET_DISCOUNT_CODES = 2

    }

    static class EndpointActionMethods
    {

        public static string GetString(this EndpointAction endpointAction)
        {
            return Enum.GetName(typeof(EndpointAction), endpointAction);
        }
    }
}