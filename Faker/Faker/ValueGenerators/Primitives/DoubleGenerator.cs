using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class DoubleGenerator : IValueGenerator
    {
        private Random _random;


        public DoubleGenerator()
        {
            _random = new Random();
        }

        public DoubleGenerator(Random rand)
        {
            _random = rand;
        }


        public object Generate()
        {
            return _random.NextDouble() * _random.Next(int.MinValue, int.MaxValue);
        }
    }
}
