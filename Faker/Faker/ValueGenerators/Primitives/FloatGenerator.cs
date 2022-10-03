using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class FloatGenerator : IValueGenerator
    {
        private Random _random;


        public FloatGenerator()
        {
            _random = new Random();
        }

        public FloatGenerator(Random random)
        {
            _random = random;
        }


        public object Generate()
        {
            return _random.NextSingle() * _random.Next(int.MinValue, int.MaxValue);
        }
    }
}
