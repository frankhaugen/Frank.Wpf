namespace Frank.Wpf.Tests.App.Models;
public class Community
{
    public List<Person> People { get; set; } = new();
    public List<Company> Companies { get; set; } = new();
    public List<House> Houses { get; set; } = new();
    public List<Car> Cars { get; set; } = new();
}

public class Person
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int Age => DateTime.Now.Year - BirthDate.Year;
    public string? Nationality { get; set; }
    
    public Guid AddressId { get; set; }  // ID reference to Address
    public List<Guid> FriendIds { get; set; } = new();  // List of Person IDs
    public List<Guid> CarIds { get; set; } = new();  // List of Car IDs
    public List<Guid> EmployerIds { get; set; } = new();  // List of Company IDs
    public List<Guid> HouseIds { get; set; } = new();  // List of House IDs owned or rented
}

public class Address
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

public class Company
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public Guid AddressId { get; set; }  // ID reference to Address
    
    public List<Guid> EmployeeIds { get; set; } = new();  // List of Person IDs
}

public class Car
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? LicensePlate { get; set; }
    public Guid OwnerId { get; set; }  // ID reference to Person
}

public class House
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AddressId { get; set; }  // ID reference to Address
    public List<Guid> ResidentIds { get; set; } = new();  // List of Person IDs
    public List<Guid> CarIds { get; set; } = new();  // List of Car IDs
}
