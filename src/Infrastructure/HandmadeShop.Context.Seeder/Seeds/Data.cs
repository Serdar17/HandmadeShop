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
        UserName = "seller@mail.com", 
        Email = "seller@mail.com",
        EmailConfirmed = true,
        PasswordHash = "1234",
        AvatarUrl = "/users/avatars/24ebc6a2-0f24-440a-9094-60500001dca4/wiit1koc.4k4.jpg",
        BirthDate = new DateTime(2002, 02, 02)
    };
    
    public static User Buyer => new()
    {
        Id = BuyerId,
        Name = "Buyer",
        Status = UserStatus.Active,
        Gender = Gender.Male,
        UserName = "buyer@mail.com",
        Email = "buyer@mail.com",
        EmailConfirmed = true,
        PasswordHash = "1234",  
        AvatarUrl = "/users/avatars/24ebc6a2-0f24-440a-9094-60500001dca4/wiit1koc.4k4.jpg",
        BirthDate = new DateTime(2002, 02, 02)
    };

    public static List<Product> TestProducts => [..TestProduct1, ..TestProduct2, ..TestProduct3, ..TestProduct4];

    public static List<Catalog> TestCatalogs =>
    [
        new Catalog("Ручная вышивка"),
        new Catalog("Деревянные изделия"),
        new Catalog("Посуда"),
        new Catalog("Выпечка и сладости")
    ];

    public static List<Product> TestProduct1 =
    [
        new Product
        {
            Name = "Платье талисман",
            Description = "Платье вышитое руками",
            Quantity = 10,
            Price = 4000,
            HasDiscount = false,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/k5vsma1d.m4b.jfif"],
        },
        
        new Product
        {
            Name = "Вышика лентами",
            Description = "Платье вышитое лентами",
            Quantity = 5,
            Price = 3000,
            HasDiscount = true,
            DiscountPrice = 2699,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/gdd5adyv.sim.jfif"],
        },
        
        new Product
        {
            Name = "Вышивка на рубашке",
            Description = "Рубашка с вышивкой",
            Quantity = 10,
            Price = 1500,
            HasDiscount = false,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/ni5sjtcr.t3b.jfif"],
        },
        
        new Product
        {
            Name = "Кофта с эскизами",
            Description = "Кофта с эскизами вышитами своими руками",
            Quantity = 10,
            Price = 4000,
            HasDiscount = false,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/34yqi4xn.wfz.jfif"],
        },
        
        new Product
        {
            Name = "Вышивка",
            Description = "Ручная вышивка",
            Quantity = 10,
            Price = 4000,
            HasDiscount = false,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/nqaiihjd.rii.jfif"],
        },
    ];

    public static List<Product> TestProduct2 = 
    [
        new Product
        {
            Name = "Разделочный доски",
            Description = "Доска разделочная деревянная",
            Quantity = 30,
            Price = 500,
            HasDiscount = false,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/kpneopcr.djt.jfif"],
        },
        
        new Product
        {
            Name = "Менажницы",
            Description = "Доска менажница деревянная",
            Quantity = 20,
            Price = 650,
            HasDiscount = true,
            DiscountPrice = 599,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/23gtepj4.5uo.jfif"],
        },
        
        new Product
        {
            Name = "Винные столики",
            Description = "Винные столики из дерева",
            Quantity = 15,
            Price = 5000,
            HasDiscount = false,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/4ljuh53o.nt5.jfif"],
        },
        
        new Product
        {
            Name = "Кулинарные ложки",
            Description = "Кулинарные ложки из дерева",
            Quantity = 32,
            Price = 400,
            HasDiscount = true,
            DiscountPrice = 299,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/yybj1rg0.zn2.jfif"],
        },
        
        new Product
        {
            Name = "Шкатулки",
            Description = "Шкатулки деревянные",
            Quantity = 20,
            Price = 1500,
            HasDiscount = false,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/okk141fe.14h.jfif"],
        },
    ];

    public static List<Product> TestProduct3 = 
    [
        new Product
        {
            Name = "Керамические чашки",
            Description = "Керамические чашки сделанные своими руками!",
            Quantity = 15,
            Price = 1500,
            HasDiscount = false,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/tip1wisz.p4p.jfif"],
        },
        
        new Product
        {
            Name = "Набор чайной посуды",
            Description = "Набор чайной пасоды из чашек и чайников",
            Quantity = 28,
            Price = 6000,
            HasDiscount = false,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/sjn4nqqx.5jd.jfif"],
        },
        
        new Product
        {
            Name = "Набор древнерусской посуды",
            Description = "Набор древнерусской посуды",
            Quantity = 19,
            Price = 1000,
            HasDiscount = true,
            DiscountPrice = 899,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/5l55vv1s.j0r.jfif"],
        },
    ];

    public static List<Product> TestProduct4 =
    [
        new Product
        {
            Name = "Шоколад ручной работы",
            Description = "Шоколад ручной работы с плиткой и медиантами",
            Quantity = 10,
            Price = 5000,
            HasDiscount = false,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/g5kz3abv.jtk.jfif"],
            Catalog = TestCatalogs[3],
        },
        
        new Product
        {
            Name = "Шоколад конфеты ручной работы",
            Description = "Шоколад ручной работы \'я тебя люблю\'",
            Quantity = 23,
            Price = 800,
            HasDiscount = false,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/soix1h5u.4ac.jfif"],
            Catalog = TestCatalogs[3],
        },
        
        new Product
        {
            Name = "Шоколадные плитки ручной работы",
            Description = "Шоколад плитки ручной работы",
            Quantity = 12,
            Price = 500,
            HasDiscount = false,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/hspht3na.xo0.jfif"],
            Catalog = TestCatalogs[3],
        },
        
        new Product
        {
            Name = "Шоколадный микс",
            Description = "Шоколадный микс ручной работы",
            Quantity = 20,
            Price = 1500,
            HasDiscount = true,
            DiscountPrice = 1299,
            Images = ["/products/images/d53c8eb8-304a-4974-96f3-f490a548ca55/kf3ppx51.bev.jfif"],
            Catalog = TestCatalogs[3],
        },
    ];
}