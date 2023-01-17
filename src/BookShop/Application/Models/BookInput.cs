﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Application.Models;
public class BookCreateModel
{
    public  string Name { get; set; }

    public  string Description { get; set; }

    public int Price { get; set; }

    public  string Author { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }

    public byte[]? CoverImage { get; set; }
}
