using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using thief2dServer.Models.utilities;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace thief2dServer.Models.blocks
{
    [Serializable]
    public class StoryLevel
    {
        [Key]
        public int level { get; set; }

        public int shipBase { get; set; }

        public string levelString { get; set; }
    }

}