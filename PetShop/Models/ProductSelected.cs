using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Badminton.Models
{
    public class ProductSelected
    {
        public int id { get; set; }
        public int productID { get; set; }
        public string productName { get; set; }
        public string productSize { get; set; }
        public float productPrice { get; set; }
        public int productQuantity { get; set; }
        public string productImage { get; set; }

        public ProductSelected()
        {
            
        }

        public ProductSelected(int id)
        {
            this.id = id;
        }

        public ProductSelected(int id, int productID, string productName, string productSize, float productPrice, int productQuantity, string productImage) : this(id)
        {
            this.productID = productID;
            this.productName = productName;
            this.productSize = productSize;
            this.productPrice = productPrice;
            this.productQuantity = productQuantity;
            this.productImage = productImage;
        }

        public override bool Equals(object obj)
        {
            return obj is ProductSelected selected &&
                   productID == selected.productID &&
                   productSize == selected.productSize;
        }

        public override int GetHashCode()
        {
            int hashCode = -1546166226;
            hashCode = hashCode * -1521134295 + productID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(productSize);
            return hashCode;
        }
    }
}