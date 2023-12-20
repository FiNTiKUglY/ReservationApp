using Microsoft.AspNetCore.Identity;
using ReservationApp.Entities;

namespace ReservationApp.Data
{
    public interface IDbInitializer
    {
        void Initialize();
    }

    public class DbInitializer(ApplicationContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        : IDbInitializer
    {
        public async void Initialize()
        {
            await context.Database.EnsureCreatedAsync();

            var roles = new List<IdentityRole>() { new IdentityRole("Admin"), new IdentityRole("Manager"), new IdentityRole("User") };

            await roleManager.CreateAsync(roles[0]);
            await roleManager.CreateAsync(roles[1]);
            await roleManager.CreateAsync(roles[2]);

            User userAdmin = new User { Email = "admin@gmail.com", UserName = "admin@gmail.com", Name = "Admin", Surname = "Admin" };
            await userManager.CreateAsync(userAdmin, "admin1234");
            await userManager.AddToRoleAsync(userAdmin, "Admin");
            User userManager1 = new User { Email = "manager@gmail.com", UserName = "manager@gmail.com", Name = "Manager", Surname = "Manager" };
            await userManager.CreateAsync(userManager1, "admin1234");
            await userManager.AddToRoleAsync(userManager1, "Manager");

            var rooms = new List<Room>()
            {
                new Room
                {
                    Id = new Guid(),
                    Description = "Люкс, балкон",
                    CostPerDay = 70.50m,
                    Number = 101,
                    AdultAmount = 2,
                    KidAmount = 1
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Люкс, балкон",
                    CostPerDay = 70.50m,
                    Number = 102,
                    AdultAmount = 2,
                    KidAmount = 1
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Люкс, балкон",
                    CostPerDay = 70.50m,
                    Number = 103,
                    AdultAmount = 2,
                    KidAmount = 1
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Люкс, балкон",
                    CostPerDay = 70.50m,
                    Number = 104,
                    AdultAmount = 2,
                    KidAmount = 1
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Эконом",
                    CostPerDay = 40.50m,
                    Number = 201,
                    AdultAmount = 2,
                    KidAmount = 0
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Эконом",
                    CostPerDay = 40.50m,
                    Number = 202,
                    AdultAmount = 2,
                    KidAmount = 0
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Люкс, балкон",
                    CostPerDay = 70.50m,
                    Number = 101,
                    AdultAmount = 2,
                    KidAmount = 1
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Люкс, балкон",
                    CostPerDay = 70.50m,
                    Number = 102,
                    AdultAmount = 2,
                    KidAmount = 1
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Люкс, балкон",
                    CostPerDay = 70.50m,
                    Number = 103,
                    AdultAmount = 2,
                    KidAmount = 1
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Люкс, балкон",
                    CostPerDay = 70.50m,
                    Number = 104,
                    AdultAmount = 2,
                    KidAmount = 1
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Эконом",
                    CostPerDay = 40.50m,
                    Number = 201,
                    AdultAmount = 2,
                    KidAmount = 0
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Эконом",
                    CostPerDay = 40.50m,
                    Number = 202,
                    AdultAmount = 2,
                    KidAmount = 0
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Люкс, балкон",
                    CostPerDay = 70.50m,
                    Number = 101,
                    AdultAmount = 2,
                    KidAmount = 1
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Люкс, балкон",
                    CostPerDay = 70.50m,
                    Number = 102,
                    AdultAmount = 2,
                    KidAmount = 1
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Люкс, балкон",
                    CostPerDay = 70.50m,
                    Number = 103,
                    AdultAmount = 2,
                    KidAmount = 1
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Люкс, балкон",
                    CostPerDay = 70.50m,
                    Number = 104,
                    AdultAmount = 2,
                    KidAmount = 1
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Эконом",
                    CostPerDay = 40.50m,
                    Number = 201,
                    AdultAmount = 2,
                    KidAmount = 0
                },
                new Room
                {
                    Id = new Guid(),
                    Description = "Эконом",
                    CostPerDay = 40.50m,
                    Number = 202,
                    AdultAmount = 2,
                    KidAmount = 0
                }
            };
            await context.Rooms.AddRangeAsync(rooms);

            var tags = new List<Tag>()
            {
                new Tag
                {
                    Id = new Guid(),
                    Title = "Можно с питомцами",
                    Description = "Можно с питомцами"
                },
                new Tag
                {
                    Id = new Guid(),
                    Title = "Вид на город",
                    Description = "Вид на город"
                },
                new Tag
                {
                    Id = new Guid(),
                    Title = "Бесплатный Wi-Fi",
                    Description = "Бесплатный Wi-Fi"
                },
                new Tag
                {
                    Id = new Guid(),
                    Title = "Балкон",
                    Description = "Балкон"
                },
                new Tag
                {
                    Id = new Guid(),
                    Title = "Кондиционер",
                    Description = "Кондиционер"
                },
                new Tag
                {
                    Id = new Guid(),
                    Title = "Активный отдых",
                    Description = "Активный отдых"
                },
                new Tag
                {
                    Id = new Guid(),
                    Title = "Прачечная",
                    Description = "Прачечная"
                }
            };
            await context.Tags.AddRangeAsync(tags);

            var regions = new List<Region>()
            {
                new Region
                {
                    Id= new Guid(),
                    Country = "Польша",
                    City = "Варшава"
                },
                new Region
                {
                    Id= new Guid(),
                    Country = "Венгрия",
                    City = "Будапешт"
                },
                new Region
                {
                    Id= new Guid(),
                    Country = "Греция",
                    City = "Афины"
                },
                new Region
                {
                    Id= new Guid(),
                    Country = "Чехия",
                    City = "Прага"
                },
                new Region
                {
                    Id= new Guid(),
                    Country = "Австрия",
                    City = "Вена"
                }
            };
            await context.Regions.AddRangeAsync(regions);

            var services = new List<Service>()
            {
                new Service
                {
                    Id= new Guid(),
                    Title = "Завтрак",
                    CostPerDay = 12.50m,
                    Description = "Завтрак"
                },
                new Service
                {
                    Id= new Guid(),
                    Title = "Обед",
                    CostPerDay = 15.50m,
                    Description = "Обед"
                },
                new Service
                {
                    Id= new Guid(),
                    Title = "Ужин",
                    CostPerDay = 12.50m,
                    Description = "Ужин"
                },
                new Service
                {
                    Id= new Guid(),
                    Title = "Утюг",
                    CostPerDay = 1.50m,
                    Description = "Утюг"
                },
                new Service
                {
                    Id= new Guid(),
                    Title = "Сейф",
                    CostPerDay = 4.50m,
                    Description = "Сейф"
                },
                new Service
                {
                    Id= new Guid(),
                    Title = "Завтрак",
                    CostPerDay = 12.50m,
                    Description = "Завтрак"
                },
                new Service
                {
                    Id= new Guid(),
                    Title = "Обед",
                    CostPerDay = 15.50m,
                    Description = "Обед"
                },
                new Service
                {
                    Id= new Guid(),
                    Title = "Ужин",
                    CostPerDay = 12.50m,
                    Description = "Ужин"
                },
                new Service
                {
                    Id= new Guid(),
                    Title = "Утюг",
                    CostPerDay = 1.50m,
                    Description = "Утюг"
                },
                new Service
                {
                    Id= new Guid(),
                    Title = "Сейф",
                    CostPerDay = 4.50m,
                    Description = "Сейф"
                },
                new Service
                {
                    Id= new Guid(),
                    Title = "Завтрак",
                    CostPerDay = 12.50m,
                    Description = "Завтрак"
                },
                new Service
                {
                    Id= new Guid(),
                    Title = "Обед",
                    CostPerDay = 15.50m,
                    Description = "Обед"
                },
                new Service
                {
                    Id= new Guid(),
                    Title = "Ужин",
                    CostPerDay = 12.50m,
                    Description = "Ужин"
                }
            };
            await context.Services.AddRangeAsync(services);

            var holdings = new List<Holding>()
            {
                new Holding
                {
                    Id= new Guid(),
                    Title = "Random Holding",
                    PhoneNumber = "+3213213123",
                    WebSite = "www.asdasd.by",
                    Moderator = userManager1
                },
                new Holding
                {
                    Id= new Guid(),
                    Title = "Random Holding 2",
                    PhoneNumber = "+3213213123",
                    WebSite = "www.asdassd.by",
                    Moderator = userManager1
                }
            };
            await context.Holdings.AddRangeAsync(holdings);

            await context.Hotels.AddRangeAsync(new List<Hotel>
            {
                new Hotel
                {
                    Id = Guid.NewGuid(),
                    Title = "Leda Apartments",
                    Description = "Апарт-отель Leda расположен в самом центре Будапешта. Он находится на тихой улице рядом с проспектом Андраши и оперным театр. К услугам гостей проживание по умеренным цен.\r\n\r\nПросторные апартаменты в элегантном стиле являются хорошей альтернативой для дорогих гостиниц. В апартаментах гостям предоставляется 4-звездочный комфорт. В апартаментах также есть полностью оборудованная кухня, кондиционер, балкон и бесплатный беспроводной доступ в Интернет.\r\n\r\nВ непосредственной близости гости имеется много отличных ресторанов с разнообразной кухней, а также кафе, дизайнерских галерей, магазинов и мест, где жизнь не останавливается даже ночью.\r\n\r\nВ апарт-отеле есть парковка.\r\n\r\nБлижайшая станция метро Opera находится на проспекте Андраши (линия M1). На станции метро Deak square останавливаются линии метро M1, M2, M3. Там же располагаются остановки нескольких трамваев и автобусов.\r\n\r\nИз апарт-отеля можно дойти пешком до городских достопримечательностей. К ним относятся Базилика св. Иштвана, Синагога и площадь Героев, на которой размещается Музей Изобразительных Искусств.",
                    IsPremium = true,
                    Tags = new List<Tag> { tags[0], tags[1], tags[2], tags[3] },
                    Services = new List<Service> { services[5], services[6], services[7], services[9] },
                    Region = regions[1],
                    Owner = holdings[0],
                    Rooms = new List<Room> { rooms[0], rooms[1], rooms[2], rooms[3], rooms[4], rooms[5] }
                },
                new Hotel
                {
                    Id = Guid.NewGuid(),
                    Title = "Warsaw Apartments Bliska Wola",
                    Description = "Апартаменты Warsaw Bliska Wola расположены в Варшаве, в 2,1 км от Музея Варшавского восстания, в 2,4 км от торгового центра Blue City и в 2,8 км от железнодорожного вокзала Варшава-Западная. К услугам гостей апартаменты с кондиционером. Предоставляется бесплатный Wi-Fi на всей территории.\r\n\r\nВ числе удобств некоторых апартаментов телевизор с плоским экраном и кабельными каналами, полностью оборудованная кухня с посудомоечной машиной и собственная ванная комната с ванной или душем и феном.\r\n\r\nЖелезнодорожный вокзал Варшава-Центральная и торговый центр Złote Tarasy находятся в 3,9 км. Расстояние от апартаментов Warsaw Bliska Wola до международного аэропорта Варшава имени Фредерика Шопена составляет 6 км. За дополнительную плату организуется трансфер от/до аэропорта.",
                    IsPremium = false,
                    Tags = new List<Tag> { tags[0], tags[1], tags[2], tags[3], tags[4] },
                    Services = new List<Service> { services[0], services[1], services[2], services[4], services[3] },
                    Region = regions[0],
                    Owner = holdings[1],
                    Rooms = new List<Room> { rooms[6], rooms[7], rooms[8], rooms[9], rooms[10], rooms[11] }
                },
                new Hotel
                {
                    Id = Guid.NewGuid(),
                    Title = "Theasis Athens",
                    Description = "Отель Theasis Athens расположен в центре Афин, в нескольких шагах от блошиного рынка Монастираки и в 200 м от площади Монастираки. К услугам гостей номера с бесплатным Wi-Fi и балконом.\r\n\r\nВ числе удобств кондиционер, полностью оборудованная кухня с обеденной зоной, телевизор с плоским экраном и собственная ванная комната с душем, феном и бесплатными туалетно-косметическими принадлежностями. В числе удобств холодильник, мини-бар, тостер, чайник и кофемашина.\r\n\r\nЕжедневно в отеле типа «постель и завтрак» сервируется континентальный завтрак.\r\n\r\nВ распоряжении гостей отеля Theasis Athens терраса.\r\n\r\nВ отеле работает пункт проката автомобилей.\r\n\r\nСреди популярных достопримечательностей рядом с отелем Theasis Athens - Храм Хефесте, древняя Афинская агора и Римская Агора. Расстояние до аэропорта имени Элефтериоса Венизелоса составляет 20 км.",
                    IsPremium = false,
                    Tags = new List<Tag> { tags[0], tags[1], tags[2], tags[3], tags[4] },
                    Services = new List<Service> { services[10], services[11], services[12] },
                    Region = regions[2],
                    Owner = holdings[1],
                    Rooms = new List<Room> { rooms[12], rooms[13], rooms[14], rooms[15], rooms[16], rooms[17] }
                }
            });

            await context.SaveChangesAsync();
        }
    }
}
