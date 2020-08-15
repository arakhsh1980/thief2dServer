using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace thief2dServer.Models.blocks
{
    [Serializable]
    public class ShipBase
    {
        [Key]
        public int id { get; set; }

        public string BaseString { get; set; }

    }
}