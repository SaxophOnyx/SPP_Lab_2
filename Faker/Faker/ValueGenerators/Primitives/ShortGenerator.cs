using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class ShortGenerator : IValueGenerator
    {
        private Random _random;


        public ShortGenerator()
        {
            _random = new Random();
        }

        public ShortGenerator(Random random)
        {
            _random = random;
        }


        public object Generate()
        {
            return _random.Next(short.MinValue, short.MaxValue + 1);
        }
    }
}
