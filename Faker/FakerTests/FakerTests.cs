using FakerCore;
using FakerCore.Exceptions;
using FakerCore.ValueGenerators.Collections;
using FakerCore.ValueGenerators.Primitives;
using FakerTests.Models.Error;
using FakerTests.Models.General;

namespace FakerTests
{
    [TestClass]
    public class FakerTests
    {
        [TestMethod]
        public void Create_Test_General()
        {
            //arrange
            Random seedRandomizer = new Random();
            int seed = seedRandomizer.Next();

            var fakerIntGenerator = new IntGenerator(new Random(seed));
            var fakerStringGenerator = new StringGenerator(new Random(seed));

            var intGenerator = new IntGenerator(new Random(seed));
            var stringGenerator = new StringGenerator(new Random(seed));

            Faker faker = new Faker();
            faker.ClearGenerators();
            faker.TrySetGenerator(typeof(int), fakerIntGenerator, true);
            faker.TrySetGenerator(typeof(string), fakerStringGenerator, true);

            int id = (int)intGenerator.Generate();
            string name = (string)stringGenerator.Generate();
            string surname = (string)stringGenerator.Generate();
            int age = (int)intGenerator.Generate();
            User user = new User(id, new FullName(name, surname), age);

            //act
            User fakerUser = faker.Create<User>();

            //assert
            Assert.AreEqual(user, fakerUser);
        }

        [TestMethod]
        public void Create_Test_List()
        {
            //arrange
            Random seedRandomizer = new Random();
            int seed = seedRandomizer.Next();
            int maxLength = 8;

            var fakerIntGenerator = new IntGenerator(new Random(seed));
            var fakerStringGenerator = new StringGenerator(new Random(seed));
            var faker = new Faker();
            faker.ClearGenerators();
            var fakerListGenerator = new ListGenerator<User>(faker, maxLength, new Random(seed));
            faker.TrySetGenerator(typeof(int), fakerIntGenerator, true);
            faker.TrySetGenerator(typeof(string), fakerStringGenerator, true);
            faker.TrySetGenerator(typeof(List<User>), fakerListGenerator, true);

            var testIntGenerator = new IntGenerator(new Random(seed));
            var testStringGenerator = new StringGenerator(new Random(seed));
            var testFaker = new Faker();
            testFaker.ClearGenerators();
            testFaker.TrySetGenerator(typeof(int), testIntGenerator, true);
            testFaker.TrySetGenerator(typeof(string), testStringGenerator, true);

            Random lengthRandomizer = new Random(seed);
            int length = lengthRandomizer.Next(maxLength + 1);

            List<User> testList = new List<User>(length);
            for (int i = 0; i < length; ++i)
            {
                User u = testFaker.Create<User>();
                testList.Add(u);
            }

            //act
            List<User> fakerList = faker.Create<List<User>>();

            //assert
            CollectionAssert.AreEqual(testList, fakerList);
        }

        [TestMethod]
        [ExpectedException(typeof(NoSuitableConstructorException))]
        public void Create_Test_CyclicDependency()
        {
            Faker faker = new Faker();
            CyclicModel model = faker.Create<CyclicModel>();
        }

        [TestMethod]
        [ExpectedException(typeof(NoSuitableConstructorException))]
        public void Create_Test_PrivateConstructor()
        {
            Faker faker = new Faker();
            PrivateConstructorModel model = faker.Create<PrivateConstructorModel>();
        }
    }
}