using System.Globalization;
using Frank.Wpf.Tests.App.Models;

namespace Frank.Wpf.Tests.App.Factories;

public static class TestDataFactory
{
    private static readonly List<string> StaticPersonNames = new()
    {
        "Alice", "Robert", "Emily", "David"
    };

    private static readonly List<string> StaticCompanyNames = new()
    {
        "TechCorp", "InnovateX", "GreenEnergy", "HealthSolutions"
    };

    private static readonly List<string> StaticStreetNames = new()
    {
        "Oak Street", "Maple Avenue", "Pine Lane", "Cedar Road", "Willow Drive"
    };

    private static readonly List<string> StaticCarBrands = new()
    {
        "Toyota", "Honda", "Ford", "BMW", "Mercedes"
    };

    private static readonly List<string> StaticCarModels = new()
    {
        "Model S", "Accord", "Civic", "Mustang"
    };

    private static readonly List<string> StaticCityNames = new()
    {
        "Springfield", "Rivertown"
    };

    public static Community CreateCommunity()
    {
        return new Community
        {
            People = new List<Person>
            {
                new()
                {
                    Id = new Guid("cd207097-92bd-4280-854e-328b32b9df38"),
                    Name = "Alice",
                    BirthDate = DateTime.ParseExact("1980-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                    Nationality = "Sample Nationality",
                    AddressId = new Guid("b7bd89dd-3492-4e3a-b93c-8aa3ef89ee0e"),
                    FriendIds = new List<Guid>
                    {
                        new("5a1ca3ca-dc10-45b6-86c4-8ecb1b8301e3"),
                        new("384374c6-6b6a-41ec-a8b3-cf88281f4d45"),
                        new("384374c6-6b6a-41ec-a8b3-cf88281f4d45"),
                        new("379c37a0-6897-45cb-b0f4-052e25677b76")
                    },
                    CarIds = new List<Guid>
                    {
                        new("ecff8026-9016-4796-bfac-c6f9b18e38e9")
                    },
                    EmployerIds = new List<Guid>
                    {
                        new("8385eb9b-bd01-405e-8c46-f0e4a9006c8d")
                    },
                    HouseIds = new List<Guid>()
                },
                new()
                {
                    Id = new Guid("5a1ca3ca-dc10-45b6-86c4-8ecb1b8301e3"),
                    Name = "Robert",
                    BirthDate = DateTime.ParseExact("1981-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                    Nationality = "Sample Nationality",
                    AddressId = new Guid("dfccc727-07ba-4cc2-a25b-be5b5a9cd338"),
                    FriendIds = new List<Guid>
                    {
                        new("cd207097-92bd-4280-854e-328b32b9df38"),
                        new("384374c6-6b6a-41ec-a8b3-cf88281f4d45"),
                        new("379c37a0-6897-45cb-b0f4-052e25677b76"),
                        new("379c37a0-6897-45cb-b0f4-052e25677b76")
                    },
                    CarIds = new List<Guid>
                    {
                        new("1d172c5f-998b-448b-972f-0b1f1e4891ed")
                    },
                    EmployerIds = new List<Guid>
                    {
                        new("d8ea3bd3-9aa1-4dfb-8cf1-430d11d74512")
                    },
                    HouseIds = new List<Guid>()
                },
                new()
                {
                    Id = new Guid("384374c6-6b6a-41ec-a8b3-cf88281f4d45"),
                    Name = "Emily",
                    BirthDate = DateTime.ParseExact("1982-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                    Nationality = "Sample Nationality",
                    AddressId = new Guid("e2f6d9e4-3906-41e5-a61b-80b792af7fb6"),
                    FriendIds = new List<Guid>
                    {
                        new("cd207097-92bd-4280-854e-328b32b9df38"),
                        new("5a1ca3ca-dc10-45b6-86c4-8ecb1b8301e3"),
                        new("379c37a0-6897-45cb-b0f4-052e25677b76"),
                        new("cd207097-92bd-4280-854e-328b32b9df38")
                    },
                    CarIds = new List<Guid>
                    {
                        new("f7149d56-fb1a-4366-bef6-d48d2ef79254")
                    },
                    EmployerIds = new List<Guid>
                    {
                        new("ca5b4996-23f7-4419-883f-fbf7ba77deb6")
                    },
                    HouseIds = new List<Guid>()
                },
                new()
                {
                    Id = new Guid("379c37a0-6897-45cb-b0f4-052e25677b76"),
                    Name = "David",
                    BirthDate = DateTime.ParseExact("1983-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                    Nationality = "Sample Nationality",
                    AddressId = new Guid("b7bd89dd-3492-4e3a-b93c-8aa3ef89ee0e"),
                    FriendIds = new List<Guid>
                    {
                        new("5a1ca3ca-dc10-45b6-86c4-8ecb1b8301e3"),
                        new("384374c6-6b6a-41ec-a8b3-cf88281f4d45"),
                        new("cd207097-92bd-4280-854e-328b32b9df38"),
                        new("5a1ca3ca-dc10-45b6-86c4-8ecb1b8301e3")
                    },
                    CarIds = new List<Guid>
                    {
                        new("001962fc-3da8-4124-9e1a-44c9acc38375")
                    },
                    EmployerIds = new List<Guid>
                    {
                        new("d8abf140-a7b9-474e-a805-695af4011ae5")
                    },
                    HouseIds = new List<Guid>()
                }
            },
            Companies = new List<Company>
            {
                new()
                {
                    Id = new Guid("8385eb9b-bd01-405e-8c46-f0e4a9006c8d"),
                    Name = "TechCorp",
                    AddressId = new Guid("b7bd89dd-3492-4e3a-b93c-8aa3ef89ee0e"),
                    EmployeeIds = new List<Guid>
                    {
                        new("cd207097-92bd-4280-854e-328b32b9df38")
                    }
                },
                new()
                {
                    Id = new Guid("d8ea3bd3-9aa1-4dfb-8cf1-430d11d74512"),
                    Name = "InnovateX",
                    AddressId = new Guid("dfccc727-07ba-4cc2-a25b-be5b5a9cd338"),
                    EmployeeIds = new List<Guid>
                    {
                        new("5a1ca3ca-dc10-45b6-86c4-8ecb1b8301e3")
                    }
                },
                new()
                {
                    Id = new Guid("ca5b4996-23f7-4419-883f-fbf7ba77deb6"),
                    Name = "GreenEnergy",
                    AddressId = new Guid("e2f6d9e4-3906-41e5-a61b-80b792af7fb6"),
                    EmployeeIds = new List<Guid>
                    {
                        new("384374c6-6b6a-41ec-a8b3-cf88281f4d45")
                    }
                },
                new()
                {
                    Id = new Guid("d8abf140-a7b9-474e-a805-695af4011ae5"),
                    Name = "HealthSolutions",
                    AddressId = new Guid("b7bd89dd-3492-4e3a-b93c-8aa3ef89ee0e"),
                    EmployeeIds = new List<Guid>
                    {
                        new("379c37a0-6897-45cb-b0f4-052e25677b76")
                    }
                }
            },
            Houses = new List<House>
            {
                new()
                {
                    Id = new Guid("e5aa937c-2476-40a4-a00d-279ea7004c0c"),
                    AddressId = new Guid("b7bd89dd-3492-4e3a-b93c-8aa3ef89ee0e"),
                    ResidentIds = new List<Guid>
                    {
                        new("cd207097-92bd-4280-854e-328b32b9df38"),
                        new("5a1ca3ca-dc10-45b6-86c4-8ecb1b8301e3")
                    },
                    CarIds = new List<Guid>()
                },
                new()
                {
                    Id = new Guid("e6119c21-6629-4bdd-ba18-e1085866828d"),
                    AddressId = new Guid("dfccc727-07ba-4cc2-a25b-be5b5a9cd338"),
                    ResidentIds = new List<Guid>
                    {
                        new("384374c6-6b6a-41ec-a8b3-cf88281f4d45"),
                        new("379c37a0-6897-45cb-b0f4-052e25677b76")
                    },
                    CarIds = new List<Guid>()
                },
                new()
                {
                    Id = new Guid("01ad76eb-9d24-422a-9d24-4228175dfcad"),
                    AddressId = new Guid("e2f6d9e4-3906-41e5-a61b-80b792af7fb6"),
                    ResidentIds = new List<Guid>(),
                    CarIds = new List<Guid>()
                }
            },
            Cars = new List<Car>
            {
                new()
                {
                    Id = new Guid("ecff8026-9016-4796-bfac-c6f9b18e38e9"),
                    Brand = "Toyota",
                    Model = "Model S",
                    Year = 2000,
                    LicensePlate = "ABC-1000",
                    OwnerId = new Guid("cd207097-92bd-4280-854e-328b32b9df38")
                },
                new()
                {
                    Id = new Guid("1d172c5f-998b-448b-972f-0b1f1e4891ed"),
                    Brand = "Honda",
                    Model = "Accord",
                    Year = 2001,
                    LicensePlate = "ABC-1001",
                    OwnerId = new Guid("5a1ca3ca-dc10-45b6-86c4-8ecb1b8301e3")
                },
                new()
                {
                    Id = new Guid("f7149d56-fb1a-4366-bef6-d48d2ef79254"),
                    Brand = "Ford",
                    Model = "Civic",
                    Year = 2002,
                    LicensePlate = "ABC-1002",
                    OwnerId = new Guid("384374c6-6b6a-41ec-a8b3-cf88281f4d45")
                },
                new()
                {
                    Id = new Guid("001962fc-3da8-4124-9e1a-44c9acc38375"),
                    Brand = "BMW",
                    Model = "Mustang",
                    Year = 2003,
                    LicensePlate = "ABC-1003",
                    OwnerId = new Guid("379c37a0-6897-45cb-b0f4-052e25677b76")
                }
            }
        };
    }

    public static Community CreateNewCommunity()
    {
        var community = new Community();

        var addresses = CreateStaticAddresses();
        var people = CreateStaticPeople(addresses);
        var companies = CreateStaticCompanies(addresses, people);
        var houses = CreateStaticHouses(people, addresses);
        var cars = CreateStaticCars(people);

        AssignStaticEmployers(people, companies);
        AssignStaticFriends(people);

        community.People = people;
        community.Companies = companies;
        community.Houses = houses;
        community.Cars = cars;

        return community;
    }

    private static List<Address> CreateStaticAddresses()
    {
        var addresses = new List<Address>();

        for (var i = 0; i < 3; i++)
        {
            var address = new Address
            {
                Street = StaticStreetNames[i % StaticStreetNames.Count],
                City = StaticCityNames[i % StaticCityNames.Count],
                Country = "Sample Country"
            };
            addresses.Add(address);
        }

        return addresses;
    }

    private static List<Person> CreateStaticPeople(List<Address> addresses)
    {
        var people = new List<Person>();

        for (var i = 0; i < StaticPersonNames.Count; i++)
        {
            var person = new Person
            {
                Name = StaticPersonNames[i],
                BirthDate = new DateTime(1980 + i % 20, 1, 1),
                Nationality = "Sample Nationality",
                AddressId = addresses[i % addresses.Count].Id
            };

            people.Add(person);
        }

        return people;
    }

    private static List<Company> CreateStaticCompanies(List<Address> addresses, List<Person> people)
    {
        var companies = new List<Company>();

        for (var i = 0; i < StaticCompanyNames.Count; i++)
        {
            var company = new Company
            {
                Name = StaticCompanyNames[i],
                AddressId = addresses[i % addresses.Count].Id
            };
            companies.Add(company);
        }

        return companies;
    }

    private static List<House> CreateStaticHouses(List<Person> people, List<Address> addresses)
    {
        var houses = new List<House>();

        for (var i = 0; i < 3; i++)
        {
            var house = new House
            {
                AddressId = addresses[i % addresses.Count].Id
            };

            house.ResidentIds.AddRange(people.Skip(i * 2).Take(2).Select(p => p.Id));
            houses.Add(house);
        }

        return houses;
    }

    private static List<Car> CreateStaticCars(List<Person> owners)
    {
        var cars = new List<Car>();

        for (var i = 0; i < owners.Count; i++)
        {
            var car = new Car
            {
                Brand = StaticCarBrands[i % StaticCarBrands.Count],
                Model = StaticCarModels[i % StaticCarModels.Count],
                Year = 2000 + i % 10,
                LicensePlate = $"ABC-{1000 + i}",
                OwnerId = owners[i].Id
            };

            owners[i].CarIds.Add(car.Id);
            cars.Add(car);
        }

        return cars;
    }

    private static void AssignStaticEmployers(List<Person> people, List<Company> companies)
    {
        for (var i = 0; i < people.Count; i++)
        {
            var person = people[i];
            var employer = companies[i % companies.Count];

            person.EmployerIds.Add(employer.Id);
            employer.EmployeeIds.Add(person.Id);
        }
    }

    private static void AssignStaticFriends(List<Person> people)
    {
        for (var i = 0; i < people.Count; i++)
        {
            var person = people[i];

            // Define fixed friends as next 2 people in the list (circular)
            var friend1 = people[(i + 1) % people.Count];
            var friend2 = people[(i + 2) % people.Count];

            person.FriendIds.Add(friend1.Id);
            person.FriendIds.Add(friend2.Id);

            friend1.FriendIds.Add(person.Id);
            friend2.FriendIds.Add(person.Id);
        }
    }
}