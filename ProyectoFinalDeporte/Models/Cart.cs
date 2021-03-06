﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinalDeporte.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }


        public string UserId { get; set; }
        public int ProductId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}