using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FlickrClone.Models
{
    [Table("Pictures")]
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
