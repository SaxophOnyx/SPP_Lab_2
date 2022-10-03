using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class UIntGenerator : IValueGenerator
    {
        private Random _random;


        public UIntGenerator()
        {
            _random = new Random();
        }

        public UIntGenerator(Random random)
        {
            _random = random;
        }


        public object Generate()
        {
            return _random.Next() * _random.Next(0, 3);
        }
    }
}
