using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalRChatApi.Mapper
{
    public abstract class Mapper<TSource, TValue> : IMapper<TSource, TValue>, IMapper
    {

        public IList<TSource> Map(IEnumerable<TValue> sourceList)
        {            
            return (sourceList != null) ? sourceList.Select(new Func<TValue, TSource>(this.Map)).ToList<TSource>() : null;
        }

        public IList<TSource> MapOrEmptyList(IEnumerable<TValue> sourceList)
        {
            return (sourceList != null) ? sourceList.Select(new Func<TValue, TSource>(this.Map)).ToList<TSource>() : new List<TSource>();
        }

        public IList<TValue> Map(IEnumerable<TSource> sourceList)
        {
            return (sourceList != null) ? sourceList.Select(new Func<TSource, TValue>(this.Map)).ToList<TValue>() : null;
        }

        public IList<TValue> MapOrEmptyList(IEnumerable<TSource> sourceList)
        {
            return (sourceList != null) ? sourceList.Select(new Func<TSource, TValue>(this.Map)).ToList<TValue>() : new List<TValue>();
        }

        public abstract TSource Map(TValue value);

        public abstract TValue Map(TSource source);
    }
}