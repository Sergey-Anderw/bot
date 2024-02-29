using Domain.Entities;

namespace Persistence.Seeds
{
	internal static class RequestsEntities
	{
		public static List<CategoryEntity> CategoryEntity()
		{
			var parentId = Guid.NewGuid();
			return
            [
                new CategoryEntity
                {
                    Id = parentId,
                    NameTranslations = new Dictionary<string, string>
                    {
                        ["ru"] = "Спорт",
                        ["en"] = "Sport",
                        ["es"] = "Deporte"
                    }
                },

                new CategoryEntity
                {
                    Id = Guid.NewGuid(),
                    ParentId = parentId,
                    NameTranslations = new Dictionary<string, string>
                    {
                        ["ru"] = "Теннис",
                        ["en"] = "Tennis",
                        ["es"] = "Tenis"
                    }
                }
            ];

		}


	}
}
