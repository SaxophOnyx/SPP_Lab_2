using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Misc
{
    public class DateTimeGenerator : IValueGenerator
    {
        private Random _random;


        public DateTimeGenerator(Random random)
        {
            _random = random;
        }

        public DateTimeGenerator()
        {
            _random = new Random();
        }


        public object Generate()
        {
            long ticks = _random.NextInt64();
            return new DateTime(ticks);
        }
    }
}
