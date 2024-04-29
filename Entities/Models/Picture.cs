using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Picture
    {
        //Author Gustaf & Seb
        public int Id { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public virtual Estate? Estate { get; set; }
    }
}
