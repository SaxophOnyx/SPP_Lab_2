using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class SByteGenerator : IValueGenerator
    {
        private Random _random;


        public SByteGenerator()
        {
            _random = new Random();
        }

        public SByteGenerator(Random random)
        {
            _random = random;
        }


        public object Generate()
        {
            return _random.Next(sbyte.MinValue, sbyte.MaxValue + 1);
        }
    }
}
