using AutoMapper;
using System.Reflection;

namespace EmailAuth.Common.Mappings
{
    /// <summary>
    /// Profile for AutoMapper mappings.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile() : this(Assembly.GetExecutingAssembly())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class with specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to apply mappings from.</param>
        public MappingProfile(Assembly assembly)
        {
            ApplyMappingsFromAssembly(assembly);
        }

        /// <summary>
        /// Applies mappings from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to apply mappings from.</param>
        public void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => !t.IsAbstract && t.GetInterfaces().Any(i =>
                    i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IMapFrom<>) || i.GetGenericTypeDefinition() == typeof(IMapTo<>))))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(Constants.Constants.MapFrom) ?? type.GetInterfaces().LastOrDefault(x => x.Name == "IMapFrom`1")?.GetMethod(Constants.Constants.MapFrom);

                methodInfo?.Invoke(instance, new object[] { this });

                methodInfo = type.GetMethod(Constants.Constants.MapTo) ?? type.GetInterfaces().LastOrDefault(x => x.Name == "IMapTo`1")?.GetMethod(Constants.Constants.MapTo);
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
