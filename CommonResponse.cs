using System;

namespace Shinetech.Common
{
    public class CommonResponse
    {
        public int code { get; set; } = (int)ResponseCode.OK;
        private string _message;
        public string message
        {
            get
            {
                if (string.IsNullOrEmpty(_message))
                {
                    return Enum.GetName(typeof(ResponseCode), code);
                }
                return _message;
            }
            set
            {
                _message = value;
            }
        }
        public object data { get; set; }
    }
}
