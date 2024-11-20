using AutoMapper;

namespace EmailAuth.Common.Mappings
{
    /// <summary>
    /// Interface for mapping from a type.
    /// </summary>
    /// <typeparam name="T">The type to map from.</typeparam>
    public interface IMapFrom<T>
    {
        /// <summary>
        /// Maps from the specified type.
        /// </summary>
        /// <param name="profile">The AutoMapper profile.</param>
        void MapFrom(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }

    /// <summary>
    /// Maps to the specified type.
    /// </summary>
    /// <param name="profile">The AutoMapper profile.</param>
    public interface IMapTo<T>
    {
        void MapTo(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}