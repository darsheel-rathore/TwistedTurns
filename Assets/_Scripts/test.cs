object?.Member   // Accessing a member of an object if the object is not null.
object?.Method() // Invoking a method on an object if the object is not null.


using System;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

class Program
{
    static void Main()
    {
        Person person = null;

        // Without null-conditional operator (traditional null check)
        string name1 = person != null ? person.Name : "Unknown";
        Console.WriteLine("Name without null-conditional: " + name1);

        // With null-conditional operator along with null-coalescing operator
        string name2 = person?.Name ?? "Unknown";
        Console.WriteLine("Name with null-conditional: " + name2);

        person = new Person { Name = "John", Age = 30 };

        // Accessing a property using null-conditional operator
        int? age = person?.Age;
        Console.WriteLine("Age: " + (age.HasValue ? age.Value.ToString() : "Unknown"));
    }
}


Name without null-conditional: Unknown
Name with null-conditional: Unknown
Age: 30
