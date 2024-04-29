using HandmadeShop.Domain;
using HandmadeShop.Domain.Enums;

namespace HandmadeShop.Context.Seeder.Seeds;

public static class SeedData
{
    private static readonly Guid SellerId = Guid.Parse("8fda8e1b-6605-4dcf-b2af-85e574870e23");
    private static readonly Guid BuyerId = Guid.Parse("953592dc-0198-486d-b2d3-04b7deb37e95");
    
    public static User Seller => new()
    {
        Id = SellerId,
        Name = "Seller",
        Status = UserStatus.Active,
        Gender = Gender.Male,
        Email = "seller@mail.com",
        EmailConfirmed = true,
        PasswordHash = "AQAAAAIAAYagAAAAEB8kAAxVFnEyo3DnfmjkVgBDt/06SqL0zbDpCs0ckvahI9PHswQ/qlvCwAyOEFDEDw==",
        AvatarUrl = "/users/avatars/24ebc6a2-0f24-440a-9094-60500001dca4/wiit1koc.4k4.jpg",
        BirthDate = new DateTime(2002, 02, 02)
    };
    
    public static User Buyer => new()
    {
        Id = BuyerId,
        Name = "Buyer",
        Status = UserStatus.Active,
        Gender = Gender.Male,
        Email = "buyer@mail.com",
        EmailConfirmed = true,
        PasswordHash = "AQAAAAIAAYagAAAAEB8kAAxVFnEyo3DnfmjkVgBDt/06SqL0zbDpCs0ckvahI9PHswQ/qlvCwAyOEFDEDw==",
        AvatarUrl = "/users/avatars/24ebc6a2-0f24-440a-9094-60500001dca4/wiit1koc.4k4.jpg",
        BirthDate = new DateTime(2002, 02, 02)
    };

    public static List<Catalog> TestCatalogs =>
    [
        new Catalog("Ручная вышивка"),
        new Catalog("Деревянные изделия"),
        new Catalog("Посуда"),
        new Catalog("Выпечка и сладости")
    ];

    public static List<Product> TestProducts =
    [
        new Product
        {
            Name = "Платье талисман",
            Description = "Платье вышитое руками",
            Quantity = 10,
            Price = 4000,
            HasDiscount = false,
            Images = ["/products/images/198cfe6b-3cce-467c-8080-4900875fa3bb/vsdzlgrt.pke.jpg"],
            Catalog = TestCatalogs[0],
            User = Seller,
        },
        
        new Product
        {
            Name = "Вышика лентами",
            Description = "Платье вышитое лентами",
            Quantity = 5,
            Price = 3000,
            HasDiscount = true,
            DiscountPrice = 2699,
            Images = ["/products/images/198cfe6b-3cce-467c-8080-4900875fa3bb/cwbnsgvj.tep.jpg"],
            Catalog = TestCatalogs[0],
            User = Seller,
        },
        
        new Product
        {
            Name = "Вышивка на рубашке",
            Description = "Рубашка с вышивкой",
            Quantity = 10,
            Price = 1500,
            HasDiscount = false,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/u4xtmmti.wnu.jfif"],
            Catalog = TestCatalogs[0],
            User = Seller,
        },
        
        new Product
        {
            Name = "Кофта с эскизами",
            Description = "Кофта с эскизами вышитами своими руками",
            Quantity = 10,
            Price = 4000,
            HasDiscount = false,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/euu5exw1.wa5.jfif"],
            Catalog = TestCatalogs[0],
            User = Seller,
        },
        
        new Product
        {
            Name = "Вышивка",
            Description = "Ручная вышивка",
            Quantity = 10,
            Price = 4000,
            HasDiscount = false,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/euu5exw1.wa5.jfif"],
            Catalog = TestCatalogs[0],
            User = Seller,
        },
        
        new Product
        {
            Name = "Разделочный доски",
            Description = "Доска разделочная деревянная",
            Quantity = 30,
            Price = 500,
            HasDiscount = false,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/el32dwhg.vx2.jfif"],
            Catalog = TestCatalogs[1],
            User = Seller,
        },
        
        new Product
        {
            Name = "Менажницы",
            Description = "Доска менажница деревянная",
            Quantity = 20,
            Price = 650,
            HasDiscount = true,
            DiscountPrice = 599,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/atnjfyh4.vyj.jfif"],
            Catalog = TestCatalogs[1],
            User = Seller,
        },
        
        new Product
        {
            Name = "Винные столики",
            Description = "Винные столики из дерева",
            Quantity = 15,
            Price = 5000,
            HasDiscount = false,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/p0arubzg.zuh.jfif"],
            Catalog = TestCatalogs[1],
            User = Seller,
        },
        
        new Product
        {
            Name = "Кулинарные ложки",
            Description = "Кулинарные ложки из дерева",
            Quantity = 32,
            Price = 400,
            HasDiscount = true,
            DiscountPrice = 299,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/2zchduip.4kj.jfif"],
            Catalog = TestCatalogs[1],
            User = Seller,
        },
        
        new Product
        {
            Name = "Шкатулки",
            Description = "Шкатулки деревянные",
            Quantity = 20,
            Price = 1500,
            HasDiscount = false,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/nbd0jgq3.dry.jfif"],
            Catalog = TestCatalogs[1],
            User = Seller,
        },
        
        new Product
        {
            Name = "Керамические чашки",
            Description = "Керамические чашки сделанные своими руками!",
            Quantity = 15,
            Price = 1500,
            HasDiscount = false,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/ft4ghg34.cty.jfif"],
            Catalog = TestCatalogs[2],
            User = Seller,
        },
        
        new Product
        {
            Name = "Набор чайной посуды",
            Description = "Набор чайной пасоды из чашек и чайников",
            Quantity = 28,
            Price = 6000,
            HasDiscount = false,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/hwrxqq2p.g2c.jfif"],
            Catalog = TestCatalogs[2],
            User = Seller,
        },
        
        new Product
        {
            Name = "Набор древнерусской посуды",
            Description = "Набор древнерусской посуды",
            Quantity = 19,
            Price = 1000,
            HasDiscount = true,
            DiscountPrice = 899,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/ldmf3xvx.egu.jfif"],
            Catalog = TestCatalogs[2],
            User = Seller,
        },
        
        new Product
        {
            Name = "Шоколад ручной работы",
            Description = "Шоколад ручной работы с плиткой и медиантами",
            Quantity = 10,
            Price = 5000,
            HasDiscount = false,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/3mqei1it.mi0.webp"],
            Catalog = TestCatalogs[3],
            User = Seller,
        },
        
        new Product
        {
            Name = "Шоколад конфеты ручной работы",
            Description = "Шоколад ручной работы \'я тебя люблю\'",
            Quantity = 23,
            Price = 800,
            HasDiscount = false,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/dbar5jww.xep.webp"],
            Catalog = TestCatalogs[3],
            User = Seller,
        },
        
        new Product
        {
            Name = "Шоколадные плитки ручной работы",
            Description = "Шоколад плитки ручной работы",
            Quantity = 12,
            Price = 500,
            HasDiscount = false,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/rqpnhmiy.0qy.jfif"],
            Catalog = TestCatalogs[3],
            User = Seller,
        },
        
        new Product
        {
            Name = "Шоколадный микс",
            Description = "Шоколадный микс ручной работы",
            Quantity = 20,
            Price = 1500,
            HasDiscount = true,
            DiscountPrice = 1299,
            Images = ["/products/images/de014a0d-1448-4f74-bc59-94599302ae7e/wvce3lee.uql.jfif"],
            Catalog = TestCatalogs[3],
            User = Seller,
        },
    ];
}