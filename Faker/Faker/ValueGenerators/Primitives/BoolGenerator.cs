using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class BoolGenerator : IValueGenerator
    {
        private Random _random;


        public BoolGenerator()
        {
            _random = new Random();
        }

        public BoolGenerator(Random random)
        {
            _random = random;
        }


        public object Generate()
        {
            return _random.Next(int.MinValue, int.MaxValue) < 0;
        }
    }
}
