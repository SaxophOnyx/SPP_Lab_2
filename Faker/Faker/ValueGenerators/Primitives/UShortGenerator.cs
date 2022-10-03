using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class UShortGenerator : IValueGenerator
    {
        private Random _random;


        public UShortGenerator()
        {
            _random = new Random();
        }

        public UShortGenerator(Random random)
        {
            _random = random;
        }


        public object Generate()
        {
            return _random.Next(ushort.MinValue, ushort.MaxValue + 1);
        }
    }
}
