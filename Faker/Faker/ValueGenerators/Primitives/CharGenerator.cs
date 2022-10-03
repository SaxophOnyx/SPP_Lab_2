using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class CharGenerator : IValueGenerator
    {
        private Random _random;


        public CharGenerator()
        {
            _random = new Random();
        }

        public CharGenerator(Random random)
        {
            _random = random;
        }


        public object Generate()
        {
            return _random.Next(char.MinValue, char.MaxValue + 1);
        }
    }
}
