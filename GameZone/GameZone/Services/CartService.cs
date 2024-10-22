using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
       
     
        public CartService(ApplicationDbContext context)
        {
            _context = context;
          
        }

        public  IEnumerable<CartItem> GetAll()
        {
            var cartItems = _context.CartItems.Include(c => c.Game).ToList();


            if (cartItems == null)
            {
                cartItems = new List<CartItem>();
            }

            return cartItems;
        }

        public IEnumerable<CartItem> AddToCart(int id)
        {
            // Find the game by id
            var game = _context.Games.Find(id);

            if (game == null)
            {
                return new List<CartItem>();
            }

            // Check if the game is already in the cart
            var existingCartItem = _context.CartItems.FirstOrDefault(c => c.GameId == id);

            // If the game is already in the cart, increase the quantity
            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                // If the game is not in the cart, add a new CartItem
                CartItem newItem = new CartItem
                {
                    GameId = game.Id,
                    Quantity = 1
                };

                _context.CartItems.Add(newItem);
            }

            // Save changes to the database
            _context.SaveChanges();

            // Return the updated list of cart items
            return _context.CartItems.Include(c => c.Game).ToList();



        }
        public bool Delete(int id)
        {
            var isDeleted = false;


            // Find the cart item by its primary key
            var cartItem = _context.CartItems.FirstOrDefault(c => c.Id == id);

            if (cartItem == null)
            {
                return isDeleted; 
            }

           
            _context.CartItems.Remove(cartItem);
            var affectedRows = _context.SaveChanges();

            if (affectedRows > 0)
            {
                isDeleted = true; // Mark as deleted if there are affected rows
            }
        
        
            return isDeleted; // Return the deletion status

        }

        public IEnumerable<CartItem> DisplayCart()
        {
            var cart = _context.CartItems.ToList();
            return cart;
        }
    }
}
