using System.Collections.Immutable;
using System.Text;
using FakerCore.ValueGenerators.Abstractions;

namespace FakerCore.ValueGenerators.Primitives
{
    public class StringGenerator : IValueGenerator
    {
        public static int DefaultMaxLength
        {
            get { return 64; }
        }

        public ImmutableArray<char> Alphabet { get; }

        private int _maxLength;

        private Random _random;


        public StringGenerator()
        {
            _random = new Random();
            _maxLength = DefaultMaxLength;
            Alphabet = CreateDefaultAlphabet();
        }

        public StringGenerator(int maxLength)
        {
            _random = new Random();
            _maxLength = maxLength;
            Alphabet = CreateDefaultAlphabet();
        }

        public StringGenerator(Random random)
        {
            _random = random;
            _maxLength = DefaultMaxLength;
            Alphabet = CreateDefaultAlphabet();
        }

        public StringGenerator(int maxLength, Random random)
        {
            _random = random;
            _maxLength = maxLength;
            Alphabet = CreateDefaultAlphabet();
        }

        public StringGenerator(int maxLength, IEnumerable<char> alphabet)
        {
            _random = new Random();
            _maxLength = maxLength;

            var builder = ImmutableArray.CreateBuilder<char>();
            builder.AddRange(alphabet);
            Alphabet = builder.ToImmutable();
        }

        public StringGenerator(int maxLength, Random random, IEnumerable<char> alphabet)
        {
            _random = random;
            _maxLength = maxLength;

            var builder = ImmutableArray.CreateBuilder<char>();
            builder.AddRange(alphabet);
            Alphabet = builder.ToImmutable();
        }


        public object Generate()
        {
            int length = _random.Next(1, _maxLength);
            StringBuilder builder = new StringBuilder(length);

            for (int i = 0; i < length; ++i)
            {
                int index = _random.Next(0, Alphabet.Length);
                builder.Append(Alphabet[index]);
            }

            return builder.ToString();
        }

        private ImmutableArray<char> CreateDefaultAlphabet()
        {
            var builder = ImmutableArray.CreateBuilder<char>();
            builder.AddRange('A', 'B', 'C', 'D', 'E',
                             'F', 'G', 'H', 'I', 'J',
                             'K', 'L', 'M', 'N', 'O',
                             'P', 'Q', 'R', 'S', 'T',
                             'U', 'V', 'W', 'X', 'Y',
                             'Z');

            return builder.ToImmutable();
        }
    }
}
