using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class ULongGenerator : IValueGenerator
    {
        private Random _random;


        public ULongGenerator()
        {
            _random = new Random();
        }

        public ULongGenerator(Random random)
        {
            _random = random;
        }


        public object Generate()
        {
            return _random.NextInt64(0, long.MaxValue) * _random.Next(0, 3);
        }
    }
}
