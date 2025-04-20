namespace Badminton.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Order()
        {
            tb_OrderDetail = new HashSet<tb_OrderDetail>();
        }

        [DisplayName("Mã đơn hàng")]

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Tên người nhận hàng")]
        public string fullname { get; set; }

        [Required]
        [StringLength(15)]
        [DisplayName("SĐT người nhận hàng")]
        public string sdt { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Địa chỉ người nhận hàng")]

        public string addr { get; set; }

        [StringLength(500)]
        [DisplayName("Ghi chú")]

        public string note { get; set; }

        [DisplayName("Ngày đặt hàng")]

        public DateTime? orderdate { get; set; }

        [StringLength(50)]
        [DisplayName("Tình trạng đơn hàng")]

        public string stt { get; set; }

        [DisplayName("Mã người đặt hàng")]

        public int? userid { get; set; }

        public virtual tb_User tb_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_OrderDetail> tb_OrderDetail { get; set; }
    }
}
