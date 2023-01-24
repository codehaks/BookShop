using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Infrastructure.DataModels;

public class OrderData
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

public enum OrderState
{
    New=0,
    Confirmed=1,
    Canceled=2
}
