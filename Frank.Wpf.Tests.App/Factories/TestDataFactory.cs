using Frank.Wpf.Tests.App.Models;

namespace Frank.Wpf.Tests.App.Factories;

public static class TestDataFactory
{
    private static readonly List<string> StaticPersonNames = new()
    {
        "John", "Alice", "Robert", "Emily", "David"
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

        for (int i = 0; i < 3; i++)
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

        for (int i = 0; i < StaticPersonNames.Count; i++)
        {
            var person = new Person
            {
                Name = StaticPersonNames[i],
                BirthDate = new DateTime(1980 + (i % 20), 1, 1),
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

        for (int i = 0; i < StaticCompanyNames.Count; i++)
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

        for (int i = 0; i < 12; i++)
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

        for (int i = 0; i < owners.Count; i++)
        {
            var car = new Car
            {
                Brand = StaticCarBrands[i % StaticCarBrands.Count],
                Model = StaticCarModels[i % StaticCarModels.Count],
                Year = 2000 + (i % 10),
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
        for (int i = 0; i < people.Count; i++)
        {
            var person = people[i];
            var employer = companies[i % companies.Count];

            person.EmployerIds.Add(employer.Id);
            employer.EmployeeIds.Add(person.Id);
        }
    }

    private static void AssignStaticFriends(List<Person> people)
    {
        for (int i = 0; i < people.Count; i++)
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
