using FakerTests.Models;

namespace FakerTests.Models.General
{
    public class User
    {
        public int Id { get; set; }

        public FullName FullName { get; set; }

        public int Age { get; set; }


        public User(int id, FullName fullName, int age)
        {
            Id = id;
            FullName = fullName;
            Age = age;
        }


        public override bool Equals(object? obj)
        {
            User u = (User)obj;

            bool idE = Id == u.Id;
            bool nameE = FullName.Name.Equals(u.FullName.Name);
            bool surnameE = FullName.Surname.Equals(u.FullName.Surname);
            bool ageE = Age == u.Age;

            return idE && nameE && surnameE && ageE;
        }
    }
}
