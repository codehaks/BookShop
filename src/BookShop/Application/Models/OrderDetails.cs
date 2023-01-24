using BookShop.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Application.Models;

public class OrderDetails
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int BookId { get; set; }
    public BookData Book { get; set; }

    public int Amount { get; set; }

    public OrderState State { get; set; }

    public DateTime TimeCreated { get; set; }
}
