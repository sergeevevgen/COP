using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicDB.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string CustomerFIO { get; set; }
        [Required]
        public byte[] Image { get; set; }
        [Required]
        public string Product { get; set; }
        [Required]
        public string Mail { get; set; }
    }
}
