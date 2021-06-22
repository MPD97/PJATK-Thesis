using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis.Application.Common.Models
{
    public class ApiResult<T>
    {
        public T Result { get; init; }

        public bool IsSuccess { get; init; }

        public string Message { get; init; } = "";

        public IDictionary<string, string[]> Errors { get; init; }

        private ApiResult(IDictionary<string, string[]> errors)
        {
            IsSuccess = false;
            Errors = errors;

            foreach (var kvp in errors)
            {
                foreach (var msg in kvp.Value)
                {
                    Message += msg + " ";
                }
            }
        }
        private ApiResult(T result)
        {
            IsSuccess = true;
            Result = result;
        }
        public ApiResult()
        {

        }

        public static ApiResult<T> Success(T result)
        {
            return new ApiResult<T>(result);
        }

        public static ApiResult<T> Error(IDictionary<string, string[]> errors)
        {
            return new ApiResult<T>(errors);
        }
    }
}
