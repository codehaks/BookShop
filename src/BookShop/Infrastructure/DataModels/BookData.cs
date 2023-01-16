﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Infrastructure.DataModels;

public class BookData
{
    public int Id { get; set; }

    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(500)]
    public required string Description { get; set; }

    public int Price { get; set; }

    [MaxLength(250)]
    public required string Author { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }
    
}
