﻿using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Picture
    {
        //Author Gustaf & Seb
        public int Id { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public virtual Estate? Estate { get; set; }
    }
}
