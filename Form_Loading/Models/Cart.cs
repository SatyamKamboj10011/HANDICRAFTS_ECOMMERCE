using System;
using System.Collections.Generic;
using System.Linq;

namespace Form_Loading.Models
{
    public class CartItem
    {
        public Product Product { get; }
        public int Quantity { get; set; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal LineTotal
        {
            get { return Product.Price * Quantity; }
        }
    }

    /// <summary>
    /// Shopping cart shared by every form. Raises Changed whenever
    /// its contents change so the UI can refresh (e.g. the cart badge).
    /// </summary>
    public static class Cart
    {
        public const int MaxQuantity = 20;

        private static readonly List<CartItem> items = new List<CartItem>();

        public static event EventHandler Changed;

        public static IReadOnlyList<CartItem> Items
        {
            get { return items; }
        }

        public static int Count
        {
            get { return items.Sum(i => i.Quantity); }
        }

        public static decimal Total
        {
            get { return items.Sum(i => i.LineTotal); }
        }

        public static void Add(Product product, int quantity = 1)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity < 1) return;

            CartItem existing = items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (existing == null)
                items.Add(new CartItem(product, Math.Min(quantity, MaxQuantity)));
            else
                existing.Quantity = Math.Min(existing.Quantity + quantity, MaxQuantity);

            OnChanged();
        }

        public static void SetQuantity(string productId, int quantity)
        {
            CartItem item = items.FirstOrDefault(i => i.Product.Id == productId);
            if (item == null) return;

            if (quantity <= 0)
                items.Remove(item);
            else
                item.Quantity = Math.Min(quantity, MaxQuantity);

            OnChanged();
        }

        public static void Remove(string productId)
        {
            SetQuantity(productId, 0);
        }

        public static void Clear()
        {
            items.Clear();
            OnChanged();
        }

        private static void OnChanged()
        {
            Changed?.Invoke(null, EventArgs.Empty);
        }
    }
}
