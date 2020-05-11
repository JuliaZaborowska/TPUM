using System;
using DataLayer.Model;
using LogicLayer.DataTransferObjects;

namespace LogicLayer.Services.CartService
{
    public interface ICartService
    {
        CartDTO AddBookToCart(Guid bookId, Guid userId);
        CartDTO RemoveBookFromCart(Guid bookId, Guid userId);
        decimal CalculateTotalPrice(Guid userId, string code);
    }
}