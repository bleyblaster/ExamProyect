using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
   public class Product: BaseEntity
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
