using System.Collections.Concurrent;
using System.Reflection;
using FakerCore.Exceptions;
using FakerCore.ValueGenerators.Abstractions;
using FakerCore.ValueGenerators.Primitives;
using FakerCore.ValueGenerators.Misc;

namespace FakerCore
{
    public class Faker
    {
        public int GeneratorCount
        {
            get { return _generators.Count; }
        }

        private ConcurrentDictionary<Type, IValueGenerator> _generators;

        public Faker()
        {
            _generators = new ConcurrentDictionary<Type, IValueGenerator>()
            {
                [typeof(byte)] = new ByteGenerator(),
                [typeof(sbyte)] = new SByteGenerator(),
                [typeof(short)] = new ShortGenerator(),
                [typeof (ushort)] = new UShortGenerator(),
                [typeof(char)] = new CharGenerator(),
                [typeof (int)] = new IntGenerator(),
                [typeof(uint)] = new UIntGenerator(),
                [typeof(long)] = new LongGenerator(),
                [typeof(ulong)] = new ULongGenerator(),

                [typeof(float)] = new FloatGenerator(),
                [typeof(double)] = new DoubleGenerator(),
                [typeof(decimal)] = new DecimalGenerator(),

                [typeof(string)] = new StringGenerator(),
                [typeof(DateTime)] = new DateTimeGenerator(),
            };
        }


        public void ClearGenerators()
        {
            _generators.Clear();
        }

        public bool ContainsGenerator(Type type)
        {
            return _generators.ContainsKey(type);
        }

        public IValueGenerator? GetGenerator(Type type)
        {
            _generators.TryGetValue(type, out IValueGenerator? generator);
            return generator;
        }

        public bool TrySetGenerator(Type type, IValueGenerator generator, bool shouldOverride)
        {
            if (generator == null)
                throw new ArgumentNullException();

            if (shouldOverride)
            {
                _generators[type] = generator;
                return true;
            }
            else
                return _generators.TryAdd(type, generator);
        }

        public bool TryRemoveGenerator(Type type)
        {
            return _generators.TryRemove(type, out _);
        }

        public T Create<T>()
        {
            Type type = typeof(T);
            return (T)CreateObject(type, new HashSet<Type>());
        }

        private object CreateObject(Type type, HashSet<Type> usedTypes)
        {
            if (_generators.TryGetValue(type, out var generator))
            {
                try
                {
                    return generator.Generate();
                }
                catch
                {
                    throw new InappropriateGeneratorExeption();
                }
            }

            if (usedTypes.Contains(type))
                throw new CyclicDependencyException();

            usedTypes.Add(type);
            object res = null!;

            var constructors = GetSuitableConstructors(type);
            foreach (var c in constructors)
            {
                try
                {
                    res = GetInstanceFromConstructor(c, usedTypes);
                    FillProperties(res, usedTypes);
                    break;
                }
                catch (Exception e) when (e is CyclicDependencyException || e is NoSuitableConstructorException)
                {

                }
            }

            usedTypes.Remove(type);

            if (res == null)
                throw new NoSuitableConstructorException();

            return res;
        }

        private object GetInstanceFromConstructor(ConstructorInfo constructor, HashSet<Type> usedTypes)
        {
            var createdParams = new List<object>();
            var paramsToCreate = constructor.GetParameters();

            foreach (var p in paramsToCreate)
            {
                var newParam = CreateObject(p.ParameterType, usedTypes);
                createdParams.Add(newParam);
            }

            return constructor.Invoke(createdParams.ToArray());
        }

        private void FillProperties(object obj, HashSet<Type> usedTypes)
        {
            Type type = obj.GetType();

            var props = type.GetProperties();
            foreach (var p in props)
            {
                Type propType = p.PropertyType;
                try
                {
                    if ((p.CanWrite) && (EqualsDefaultValue(p.GetValue(obj))))
                    {
                        var propValue = CreateObject(propType, usedTypes);
                        p.SetValue(obj, propValue);
                    }
                }
                catch (Exception e) when (e is CyclicDependencyException || e is NoSuitableConstructorException)
                {

                }
            }

            var fields = type.GetFields();
            foreach (var f in fields)
            {
                Type fieldType = f.FieldType;
                try
                {
                    if (EqualsDefaultValue(f.GetValue(obj)))
                    {
                        var fieldValue = CreateObject(fieldType, usedTypes);
                        f.SetValue(obj, fieldValue);
                    }
                }
                catch (Exception e) when (e is CyclicDependencyException || e is NoSuitableConstructorException)
                {

                }
            }
        }

        private List<ConstructorInfo> GetSuitableConstructors(Type type)
        {
            var constructors = type.GetConstructors();
            return constructors.Where((x) => x.IsPublic)
                               .OrderByDescending((item) => item.GetParameters().Length)
                               .ToList();
        }
        
        private bool EqualsDefaultValue(object? obj)
        {
            if (obj == null)
                return true;

            Type type = obj.GetType();

            if (type.IsPrimitive)
                return obj.Equals(Activator.CreateInstance(type));
            else
                return false;
        }
    }
}
