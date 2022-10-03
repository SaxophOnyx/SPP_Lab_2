using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class DecimalGenerator : IValueGenerator
    {
        private Random _random;


        public DecimalGenerator()
        {
            _random = new Random();
        }

        public DecimalGenerator(Random random)
        {
            _random = random;
        }


        public object Generate()
        {
            byte scale = (byte)_random.Next(29);
            bool sign = _random.Next(2) == 1;

            return new decimal(_random.Next(), _random.Next(), _random.Next(), sign, scale);
        }
    }
}
