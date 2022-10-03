using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Collections
{
    public class ListGenerator<T> : IValueGenerator
    {
        public static int DefaultMaxLength
        {
            get { return 16; }
        }

        public int MaxLength { get; }

        private Faker _faker;

        private Random _random;


        public ListGenerator(Faker faker, Random random)
        {
            _faker = faker;
            MaxLength = DefaultMaxLength;
            _random = random;
        }

        public ListGenerator(Faker faker, int maxLength, Random random)
        {
            if (maxLength < 0)
                throw new ArgumentOutOfRangeException();

            _faker = faker;
            MaxLength = maxLength;
            _random = random;
        }


        public object Generate()
        {
            var length = _random.Next(MaxLength + 1);
            var result = new List<T>(length);

            for (int i = 0; i < length; ++i)
            {
                T item = _faker.Create<T>();
                result.Add(item);
            }

            return result;
        }
    }
}
