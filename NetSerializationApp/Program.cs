using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

Company yandex = new Company()
{
    Title = "Yandex",
    City = "Moscow"
};

Company piterSoft = new Company()
{
    Title = "Piter Soft",
    City = "St. Peterburg"
};

Employee employee = new Employee()
{
    Name = "Bobby",
    Birthdate = new(2000, 5, 11),
    Company = yandex
};

List<Employee> employees = new()
{
    new()
    {
        Name = "Sammy",
        Birthdate = new(1998, 2, 21),
        Company = yandex
    },
    new()
    {
        Name = "Jimmy",
        Birthdate = new(2001, 11, 5),
        Company = piterSoft
    },
    new()
    {
        Name = "Tommy",
        Birthdate = new(1989, 3, 17),
        Company = yandex
    },
};

JsonSerializerOptions options = new JsonSerializerOptions()
{
    WriteIndented = true,
    AllowTrailingCommas = true,
};

string jsonString = JsonSerializer.Serialize(employee, options);
Console.WriteLine(jsonString);

using(FileStream stream = new("employees.json", FileMode.OpenOrCreate))
{
    JsonSerializer.Serialize(stream, employee, options);
}

using (FileStream stream = new("employees.json", FileMode.Append))
{
    JsonSerializer.Serialize(stream, employees, options);
}

//var employeesRestore = JsonSerializer.Deserialize <List<Employee>>(jsonString);
//foreach(var e in employeesRestore)
//    Console.WriteLine(e);


class Company
{
    //[JsonPropertyName("CompanyName")]
    public string Title { get; set; } = "";
    //[JsonIgnore]
    public string City { get; set; } = "";

    public override string ToString()
    {
        return $"Company title: {Title}, city: {City}";
    }
}

class Employee
{
    public string Name { set; get; } = "";
    public DateOnly Birthdate { get; set; }
    public Company? Company { get; set; }
    public override string ToString()
    {
        return $"Employee name: {Name}, age: {DateTime.Now.Year - Birthdate.Year}, {Company}";
    }
}