using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDoServices
{
    public class Result<T>        
    {     
        public int ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public T Data { get; set; }
    }

}
