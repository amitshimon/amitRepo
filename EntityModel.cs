using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    public class EntityModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public System.Guid id { get; set; }

        [MinLength(6, ErrorMessage = "min length is 6 digits"), MaxLength(50, ErrorMessage = "Max length is 50 digits")]
        [Required(ErrorMessage = "name is required")]
        public string name { get; set; }

        [MinLength(6, ErrorMessage = "min length is 6 digits"), MaxLength(100, ErrorMessage = "Max length is 100 digits")]
        [Required(ErrorMessage = "description is required")]
        public string description { get; set; }

        [Range(0, 999999, ErrorMessage = "date must be between 10000000 and 50000000000")]
        [Required(ErrorMessage = "amount is required")]
        public int amount { get; set; }

        [Required(ErrorMessage = "date is required")]
        [Range(629378975999999999, 729378975999999999, ErrorMessage = "date must be between 10000000 and 3155378975999999999")]
        public long date { get; set; }

        [Required(ErrorMessage = "isPrivate is required")]
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
