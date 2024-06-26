﻿namespace HandmadeShop.SharedModel.Orders.Models;

public class AddressModel
{
    public string Country { get; set; }
    public string City { get; set; }
    public string ExactAddress { get; set; }

    public override string ToString()
    {
        return $"{Country}, {City}\n" +
               $"{ExactAddress}";
    }
}