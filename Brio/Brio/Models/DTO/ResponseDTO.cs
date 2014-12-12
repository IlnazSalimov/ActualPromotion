using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public class ResponseDTO
    {
        public object Object { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public static class ResponseProcessing
    {
        public static object Success(object responseObject, string message)
        {
            return new ResponseDTO { IsSuccess = true, Object = responseObject, Message = message };
        }

        public static object Error(string message)
        {
            return new ResponseDTO { IsSuccess = false, Object = null, Message = message };
        }
    }
}
