using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharepointecs.Models
{
    public class DataDTO<T>
    {
        public DataDTO() { }
        public T Data { get; set; }
    }
}
