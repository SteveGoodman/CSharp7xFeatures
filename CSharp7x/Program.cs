using System;

namespace CSharp7x
{
    class Person
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string MiddleName { get; internal set; }


        //public Person(string firstName, string lastName, string middleName) =>
        //    (FirstName, LastName, MiddleName) == (firstName, lastName, middleName);

        internal Person()
        { }

        public Person(string firstName, string lastName, string middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public override bool Equals(object other)
        {
            return Equals(other as Person);
        }

        public virtual bool Equals(Person other)
        {
            if (other == null) { return false; }
            if (object.ReferenceEquals(this, other)) { return true; }
            return (FirstName, LastName, MiddleName) == (other.FirstName, other.LastName, other.MiddleName);
        }

        public override int GetHashCode()
        {
            return (FirstName, LastName, MiddleName).GetHashCode();
        }

        public static bool operator ==(Person p1, Person p2)
        {
            if (p2 is null) return false;
            if (object.ReferenceEquals(p1, p2)) { return true; }
            if ((object)p1 == null || (object)p2 == null) { return false; }
            return (p1.FirstName, p1.LastName, p1.MiddleName) == (p2.FirstName, p2.LastName, p2.MiddleName);
        }

        public static bool operator !=(Person item1, Person item2)
        {
            return !(item1 == item2);
        }


        public void Deconstruct(out string firstName, out string lastName) => (firstName, lastName) = (FirstName, LastName);

        internal void Deconstruct(out string firstName, out string lastName, out string middleName)
        {
            firstName = FirstName;
            lastName = LastName;
            middleName = MiddleName;
        }

        public void CopyTo(Person other) => (other.FirstName, other.LastName, other.MiddleName) = this;

        ~Person()
        {
            //This should now be called a finalizer.
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //demo step backwards...

            var p1 = GetPerson("Steve", "Goodman", "");
            var p2 = GetPerson("John", "Smith","");
            var p3 = GetPerson("Steve", "Goodman", "");
            var p4 = GetPerson();

            Console.WriteLine($"Hello {p1.FirstName} {p1.LastName}");
            Console.WriteLine($"Hello {p2.FirstName} {p2.LastName}");
            Console.WriteLine($"Hello {p3.FirstName} {p3.LastName}");

            Console.WriteLine($"p1=p3 {p1 == p3}");

            Console.WriteLine($"P4 == null: {p4 == null}");
            try
            {
                CheckNotNull(p4, nameof(p4));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine($"P4 is null: {p4 is null}");

            //Deconstruct
            var (f, l) = new Person("Bob", "Jones", null);
            Console.WriteLine($"{f} {l}");

            //CopyTo
            var p5 = new Person();
            p1.CopyTo(p5);
            (f, l) = p5;
            Console.WriteLine($"{f} {l}");

            Console.ReadLine();
        }

        private static Person GetPerson(string firstName, string lastName, string middleName)
        {
            return new Person(firstName, lastName, middleName); 
        }

        private static Person GetPerson()
        {
            return null;
        }

        static T CheckNotNull<T>(T value, string name) where T : class
            => value ?? throw new ArgumentNullException(name);
    }
}
