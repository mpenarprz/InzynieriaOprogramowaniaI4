using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmTest.Model
{
    [Alias("Addresses")]
    public class Address
    {
        [AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
