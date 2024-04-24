using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class County
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [DisplayName("County")]
        public string CountyName { get; set; } = string.Empty;

    }
}
