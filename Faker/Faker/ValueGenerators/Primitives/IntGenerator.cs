using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class IntGenerator : IValueGenerator
    {
        private Random _random;


        public IntGenerator()
        {
            _random = new Random();
        }

        public IntGenerator(Random random)
        {
            _random = random;
        }


        public object Generate()
        {
            return _random.Next(int.MinValue, int.MaxValue);
        }
    }
}
