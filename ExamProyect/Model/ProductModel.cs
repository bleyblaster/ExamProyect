using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProyect.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
