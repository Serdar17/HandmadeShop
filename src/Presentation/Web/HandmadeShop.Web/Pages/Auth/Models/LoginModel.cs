﻿namespace HandmadeShop.Web.Pages.Auth.Models;

public class LoginModel
{
    public required string Email { get; set; }

    public required string Password { get; set; }

    public bool RememberMe { get; set; }
}