using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetmarMVCApp.Models
{
    public class OrderCart
    {
        MetmarModel metmarDB = new MetmarModel();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static OrderCart GetCart(HttpContextBase context)
        {
            var cart = new OrderCart();
            cart.ShoppingCartId = cart.GetCartId(context);

            return cart;
        }

        public static OrderCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Przedmioty item, List<Order> order)
        {
            var cartItem = order.SingleOrDefault(c => c.Item.Id == item.Id);

            if (cartItem == null)
            {
                cartItem = new Order
                {
                    Item = item,
                    DateCreated = DateTime.Now,
                    Count = 1
                };
            order.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
        }

        public int RemoveFromCart(int id, List<Order> order)
        {
            var cartItem = order.Single(c => c.Item.Id == id);

            var itemCount = 0;

            if (cartItem.Count > 1)
            {
                cartItem.Count--;
                itemCount = cartItem.Count;
            }
            else
            {
                order.Remove(cartItem);
            }
            return itemCount;
        }

        public void EmptyCart(List<Order> order)
        {
            foreach (var item in order)
            {
                order.Remove(item);
            }
        }

        public List<Order> GetCartItems(List<Order> order)
        {
            return order;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
    }
}