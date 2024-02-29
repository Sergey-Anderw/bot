﻿using AutoMapper;

namespace Shared.Extensions
{
    public static class MapperExtensions
    {
        public static TResult MergeInto<TResult>(this IMapper mapper, object item1, object item2)
        {
            var result = mapper.Map<TResult>(item1);
            return mapper.Map(item2, result);
        }

        public static TResult MergeInto<TResult>(this IMapper mapper, params object[] objects)
        {
            var res = mapper.Map<TResult>(objects.First());
            return objects.Skip(1).Aggregate(res, (r, obj) => mapper.Map(obj, r));
        }
    }
}