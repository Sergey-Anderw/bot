using System.Reflection;
using AutoMapper;

namespace Shared.AutoMapper
{
	public class AutoMapperProfile : Profile
	{
		private readonly Assembly[] _assemblies;

		private AutoMapperProfile(Assembly[] assemblies)
		{
			_assemblies = assemblies;

			LoadStandardMappings();
			LoadCustomMappings();
		}

		public static Profile Initialize(params Assembly[] assemblies)
		{
			return new AutoMapperProfile(assemblies ?? new[]
			{
				Assembly.GetExecutingAssembly()
			});
		}

		private void LoadStandardMappings()
		{
			foreach (var assembly in _assemblies)
			{
				var mapsFrom = MapperProfileHelper.LoadStandardMappings(assembly);
				foreach (var map in mapsFrom)
				{
					CreateMap(map.Source, map.Destination).ReverseMap();
				}
			}
		}

		private void LoadCustomMappings()
		{
			foreach (var assembly in _assemblies)
			{
				var mapsFrom = MapperProfileHelper.LoadCustomMappings(assembly);
				foreach (var map in mapsFrom)
				{
					map.CreateMappings(this);
				}
			}
		}
	}
}
