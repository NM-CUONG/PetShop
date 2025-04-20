namespace Badminton.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_User()
        {
            tb_Order = new HashSet<tb_Order>();
        }

        [DisplayName("Mã người dùng")]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Tên người dùng")]

        public string fullname { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Tên tài khoản")]

        public string username { get; set; }

        [StringLength(15)]
        [DisplayName("SĐT")]

        public string phonenumber { get; set; }

        [StringLength(200)]
        [DisplayName("Địa chỉ")]

        public string addr { get; set; }

        [Required]
        [StringLength(32)]
        [DisplayName("Mật khẩu")]

        public string passwd { get; set; }

        [DisplayName("Quyền người dùng")]

        public int? role_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_Order> tb_Order { get; set; }

        public virtual tb_Role tb_Role { get; set; }
    }
}
