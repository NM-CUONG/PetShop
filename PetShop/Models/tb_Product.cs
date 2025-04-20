namespace Badminton.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Product()
        {
            tb_OrderDetail = new HashSet<tb_OrderDetail>();
        }

        [DisplayName("Mã sản phẩm")]
        public int id { get; set; }

        [Required]
        [DisplayName("Tên sản phẩm")]
        public string title { get; set; }

        [DisplayName("Giá tiền cũ")]
        public double price { get; set; }

        [DisplayName("Giá tiền mới")]
        public double price_new { get; set; }

        [DisplayName("Ảnh")]

        public string img { get; set; }

        [DisplayName("Mô tả")]

        public string descript { get; set; }

        [StringLength(10)]

        [DisplayName("Kích thước")]

        public string size { get; set; }

        [DisplayName("Tình trạng")]

        public bool? stt { get; set; }

        [DisplayName("Mã dah mục")]
        public int? category_id { get; set; }

        [DisplayName("Mã thương hiệu")]
        public int? brand_id { get; set; }

        public virtual tb_Brand tb_Brand { get; set; }

        public virtual tb_Category tb_Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_OrderDetail> tb_OrderDetail { get; set; }
    }
}
