namespace BoNuong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        [Key]
        public int MaBinhLuan { get; set; }

        [StringLength(255)]
        public string NoiDung { get; set; }

        public int? MaSP { get; set; }

        [StringLength(128)]
        public string MaKH { get; set; }

        public DateTime? NgayTao { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
