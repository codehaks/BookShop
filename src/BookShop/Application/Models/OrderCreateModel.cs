using BookShop.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Application.Models;

public class OrderCreateModel
{
    public string UserId { get; set; }
    public int BookId { get; set; }
    public int Amount { get; set; }
}
