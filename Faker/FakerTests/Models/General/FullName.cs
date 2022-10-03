namespace FakerTests.Models.General
{
    public struct FullName
    {
        public string Name { get; set; }

        public string Surname { get; set; }


        public FullName(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}
