using SimpleShop.Core.Entites;

namespace SimpleShop
{
    public class ShopService
    {
        // Beräknar priset för en produkt
        public decimal CalculatePrice(Product product, int quantity)
        {
            return product.Price * quantity;
        }

        // Kontrollerar om en produkt har giltigt pris
        public bool IsValidPrice(decimal price)
        {
            return price > 0;
        }

        // Kontrollerar om det finns tillräckligt många produkter i lager
        public bool IsInStock(Product product, int quantity)
        {
            return product.Stock >= quantity;
        }

        // Beräknar rabatt
        public decimal ApplyDiscount(decimal total, int discountPercent)
        {
            if (discountPercent < 0 || discountPercent > 100)
            {
                return total;
            }

            return total - (total * discountPercent / 100);
        }

        // Beräknar fraktkostnad
        public decimal CalculateShipping(decimal total)
        {
            if (total >= 500)
            {
                return 0;
            }

            return 49;
        }

        // Beräknar totalsumman för kundvagnen
        public decimal CalculateCartTotal(List<CartItem> items)
        {
            decimal total = 0;

            foreach (var item in items)
            {
                total += CalculatePrice(item.Product, item.Quantity);
            }

            return total;
        }

        // Kontrollerar om en beställning är giltig
        public bool IsValidOrder(List<CartItem> items)
        {
            if (items == null || items.Count == 0)
            {
                return false;
            }

            foreach (var item in items)
            {
                if (item.Quantity <= 0)
                {
                    return false;
                }

                if (!IsInStock(item.Product, item.Quantity))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
