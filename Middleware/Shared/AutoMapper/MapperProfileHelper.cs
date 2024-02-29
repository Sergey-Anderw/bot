using System.Reflection;
using Shared.Interfaces.Mapping;

namespace Shared.AutoMapper
{
	public static class MapperProfileHelper
	{
		public static IList<Map> LoadStandardMappings(Assembly rootAssembly)
		{
			var types = rootAssembly.GetExportedTypes();

			var mapsFrom = (
				from type in types
				from instance in type.GetInterfaces()
				where
					instance.IsGenericType && instance.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
					!type.IsAbstract &&
					!type.IsInterface
				select new Map
				{
					Source = type.GetInterfaces().First().GetGenericArguments().First(),
					Destination = type
				}).ToList();

			return mapsFrom;
        }

		public static IList<IHaveCustomMapping> LoadCustomMappings(Assembly rootAssembly)
		{
			var types = rootAssembly.GetExportedTypes();

			var mapsFrom = (
				from type in types
				let constructors = type.GetConstructors()
				where
					typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
					!type.IsAbstract &&
					!type.IsInterface &&
					constructors.Length == 1 &&
					constructors[0].GetParameters().Length == 0
				select Activator.CreateInstance(type) as IHaveCustomMapping).ToList();

			return mapsFrom;
		}
	}
}
