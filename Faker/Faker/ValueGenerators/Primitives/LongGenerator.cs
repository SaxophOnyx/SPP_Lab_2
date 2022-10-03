using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class LongGenerator : IValueGenerator
    {
        private Random _random;


        public LongGenerator()
        {
            _random = new Random();
        }

        public LongGenerator(Random random)
        {
            _random = random;
        }


        public object Generate()
        {
            return _random.NextInt64(long.MinValue, long.MaxValue);
        }
    }
}
