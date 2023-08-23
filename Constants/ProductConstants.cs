using AuthJwt.Models;

namespace AuthJwt.Constants
{
    public class ProductConstants
    {
        public static List<ProductModel> Products = new()
        {
            new ProductModel() { Name = "Coca Cola", Description = "Bebida con gas" },
            new ProductModel() { Name = "Agua Villavicencio", Description = "Agua mineral de 2L" },
        };
    }
}
