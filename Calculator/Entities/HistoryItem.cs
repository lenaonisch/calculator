using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Entities
{
    public class HistoryItem
    {
        public int Id { get; set; }
        public char Operation { get; set; }
        public float Argument1 { get; set; }
        public float Argument2 { get; set; }
        public float? Result { get; set; }
        public DateTime DateTime { get; set; }
        public string Expression { get; set; }
    }
}
