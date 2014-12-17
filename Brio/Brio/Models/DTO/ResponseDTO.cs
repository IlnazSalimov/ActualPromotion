using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    /// <summary>
    /// Объект передачи данных с результатами запроса (ответ)
    /// </summary>
    public class ResponseDTO
    {
        public object Object { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// Предоставляет методы установки и конфигурирования ответа на запрос
    /// </summary>
    public static class ResponseProcessing
    {
        public static object Success(object responseObject, string message = "")
        {
            return new ResponseDTO { IsSuccess = true, Object = responseObject, Message = message };
        }

        public static object Error(string message)
        {
            return new ResponseDTO { IsSuccess = false, Object = null, Message = message };
        }
    }
}
