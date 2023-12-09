using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string StudentNumber { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string Occupation { get; set; }
    public string Gender { get; set; }
    public string CountryCode { get; set; }
    public string AreaCode { get; set; }
    public string PhoneNumber { get; set; }
}

class Program
{
    static void StoreEntry(List<Student> studentList)
    {
        while (true)
        {
            Console.Write("Enter student number: ");
            string studentNumber = Console.ReadLine();
            Console.Write("Enter surname: ");
            string surname = Console.ReadLine();
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter occupation: ");
            string occupation = Console.ReadLine();
            Console.Write("Enter gender (M/F): ");
            string gender = Console.ReadLine();
            Console.Write("Enter country code: ");
            string countryCode = Console.ReadLine();
            Console.Write("Enter area code: ");
            string areaCode = Console.ReadLine();
            Console.Write("Enter phone number: ");
            string phoneNumber = Console.ReadLine();

            Student student = new Student
            {
                StudentNumber = studentNumber,
                Surname = surname,
                FirstName = firstName,
                Occupation = occupation,
                Gender = gender,
                CountryCode = countryCode,
                AreaCode = areaCode,
                PhoneNumber = phoneNumber
            };

            studentList.Add(student);

            Console.Write("Do you want to enter another entry [Y/N]? ");
            string anotherEntry = Console.ReadLine().ToUpper();
            if (anotherEntry != "Y")
            {
                break;
            }
        }
    }

    static void EditEntry(List<Student> studentList)
    {
        Console.Write("Enter student number: ");
        string studentNumber = Console.ReadLine();

        Student foundStudent = studentList.FirstOrDefault(student => student.StudentNumber == studentNumber);

        if (foundStudent != null)
        {
            Console.WriteLine($"\nHere is the existing information about {studentNumber}");
            Console.WriteLine($"{foundStudent.FirstName} {foundStudent.Surname} is a {foundStudent.Occupation}." +
                              $" {Pronoun(foundStudent.Gender)} phone number is {foundStudent.PhoneNumber}\n");

            Console.WriteLine("[1] Surname [2] First name [3] Occupation [4] Gender");
            Console.WriteLine("[5] Country Code [6] Area Code [7] Phone Number [8] Back to Menu");

            Console.Write("\nEnter your data information that you want to change (1-8): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("\nEnter new Surname: ");
                    string newSurname = Console.ReadLine();
                    foundStudent.Surname = newSurname;
                    break;
                case "2":
                    Console.Write("\nEnter new First Name: ");
                    string newFirstName = Console.ReadLine();
                    foundStudent.FirstName = newFirstName;
                    break;
                case "3":
                    Console.Write("\nEnter new Occupation: ");
                    string newOccupation = Console.ReadLine();
                    foundStudent.Occupation = newOccupation;
                    break;
                case "4":
                    Console.Write("\nEnter new Gender (M/F): ");
                    string newGender = Console.ReadLine();
                    foundStudent.Gender = newGender;
                    break;
                case "5":
                    Console.Write("\nEnter new Country Code: ");
                    string newCountryCode = Console.ReadLine();
                    foundStudent.CountryCode = newCountryCode;
                    break;
                case "6":
                    Console.Write("\nEnter new Area Code: ");
                    string newAreaCode = Console.ReadLine();
                    foundStudent.AreaCode = newAreaCode;
                    break;
                case "7":
                    Console.Write("\nEnter new Phone Number: ");
                    string newPhoneNumber = Console.ReadLine();
                    foundStudent.PhoneNumber = newPhoneNumber;
                    break;
                case "8":
                    Console.WriteLine("\nGo back to the Menu Press Enter...");
                    break;
                default:
                    Console.WriteLine("\nInvalid choice. Go back to the Menu Press Enter...");
                    break;
            }
        }
        else
        {
            Console.WriteLine($"\nStudent with student number {studentNumber} not found. Go back to the Menu Press Enter...");
        }
    }

    static void SearchByCountry(List<Student> studentList)
    {
        Console.WriteLine("\nSEARCH ASEAN PHONEBOOK BY COUNTRY");

        Dictionary<string, (string, string)> countries = new Dictionary<string, (string, string)>
        {
            { "1", ("Philippines", "63") },
            { "2", ("Thailand", "66") },
            { "3", ("Singapore", "65") },
            { "4", ("Indonesia", "62") },
            { "5", ("Malaysia", "60") },
            { "6", ("ALL", null) }
        };

        List<(string, string)> selectedCountries = new List<(string, string)>();
        while (true)
        {
            Console.WriteLine("\nFrom which Country:");
            foreach (var country in countries)
            {
                Console.Write($"[{country.Key}] {country.Value.Item1} ");
            }
            Console.WriteLine("[0] No More");

            Console.Write("\nEnter your choice (1-6): ");
            string choice = Console.ReadLine();
            if (choice == "0")
            {
                break;
            }
            else if (countries.ContainsKey(choice))
            {
                selectedCountries.Add(countries[choice]);
            }
        }

        Console.Write("\nHere are the students from");
        if (selectedCountries.Any(c => c.Item1 == "ALL"))
        {
            Console.WriteLine(" ALL countries:");
            var sortedStudents = studentList.OrderBy(x => (countries.ContainsKey(x.CountryCode) ? countries[x.CountryCode].Item1 : ""), StringComparer.Ordinal)
                                            .ThenBy(x => x.Surname, StringComparer.Ordinal);
            foreach (var student in sortedStudents)
            {
                Console.WriteLine($"{student.FirstName}, {student.Surname}, with student number {student.StudentNumber}," +
                                  $" is a {student.Occupation}. {Pronoun(student.Gender)} phone number is {student.PhoneNumber}");
            }
        }
        else
        {
            Console.WriteLine(" and " + string.Join(", ", selectedCountries.Select(c => c.Item1)) + ":");
            var selectedStudents = studentList.Where(student => selectedCountries.Any(c => c.Item2 == student.CountryCode))
                                             .OrderBy(x => x.Surname, StringComparer.Ordinal);
            foreach (var student in selectedStudents)
            {
                Console.WriteLine($"{student.FirstName}, {student.Surname}, with student number {student.StudentNumber}," +
                                  $" is a {student.Occupation}. {Pronoun(student.Gender)} phone number is {student.PhoneNumber}");
            }
        }

        Console.WriteLine("\nGo back to the Menu Press Enter...");
    }

    static string Pronoun(string gender)
    {
        return gender.ToUpper() == "M" ? "His" : "Her";
    }

    static void Main()
    {
        List<Student> studentList = new List<Student>();
        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Store Entry");
            Console.WriteLine("2. Edit Entry");
            Console.WriteLine("3. Search by Country");
            Console.WriteLine("4. Exit");

            Console.Write("\nEnter your choice (1-4): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    StoreEntry(studentList);
                    break;
                case "2":
                    EditEntry(studentList);
                    break;
                case "3":
                    SearchByCountry(studentList);
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option (1-4).");
                    break;
            }
        }
    }
}
