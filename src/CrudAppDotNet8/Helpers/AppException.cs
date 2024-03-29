﻿using System.Globalization;

namespace CrudAppDotNet8.Helpers
{
    public class AppException : Exception
    {
        public AppException() : base() { }
        public AppException(string message) : base(message) { }
        public AppException(string message, Exception innerException) : base(message, innerException) { }

        public AppException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
