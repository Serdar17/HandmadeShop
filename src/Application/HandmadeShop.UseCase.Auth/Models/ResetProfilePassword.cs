﻿namespace HandmadeShop.UseCase.Auth.Models;

public class ResetProfilePasswordModel
{
    public string UserId { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
}