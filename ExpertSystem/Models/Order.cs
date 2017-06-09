using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ExpertSystem.Enums;

namespace ExpertSystem.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        public int WarehouseID { get; set; }

        public int ProductID { get; set; }

        public int TruckID { get; set; }

        public int Capacity { get; set; }

        public DateTime DateTime { get; set; }

        public OrderStatus? Status { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual Product Product { get; set; }

        public virtual Truck Truck { get; set; }
    }
}