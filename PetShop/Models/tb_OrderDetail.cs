namespace Badminton.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_OrderDetail
    {
        [DisplayName("Mã người đặt hàng")]

        public int id { get; set; }
        [DisplayName("Mã đơn hàng")]

        public int? order_id { get; set; }

        [DisplayName("Mã hàng")]

        public int? product_id { get; set; }

        [DisplayName("Số lượng")]

        public int num { get; set; }

        public virtual tb_Order tb_Order { get; set; }

        public virtual tb_Product tb_Product { get; set; }
    }
}
