using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class ByteGenerator : IValueGenerator
    {
        private Random _random;


        public ByteGenerator()
        {
            _random = new Random();
        }

        public ByteGenerator(Random random)
        {
            _random = random;
        }


        public object Generate()
        {
            return _random.Next(byte.MinValue, byte.MaxValue + 1);
        }
    }
}
