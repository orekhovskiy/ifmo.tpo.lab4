using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Commons
{
    public class Result
    {
        public bool Success { get; }
        public object Value { get; }

        public Result() { }

        public Result(bool success)
        {
            Success = success;
            Value = null;
        }

        public Result(bool success, object value)
        {
            Success = success;
            Value = value;
        }
    }
}
