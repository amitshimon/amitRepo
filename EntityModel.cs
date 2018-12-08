using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    public class EntityModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public long date { get; set; }
        public bool isPrivate { get; set; }
       
    }
    public class WebEntityModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public DateTime date { get; set; }
        public bool isPrivate { get; set; }

    }
}