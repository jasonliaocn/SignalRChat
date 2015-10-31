using System.Collections.Generic;

namespace SignalRChatApi.Mapper
{
    public interface IMapper
    {
    }

    public interface IMapper<TSource, TValue> : IMapper
    {
        IList<TSource> Map(IEnumerable<TValue> sourceList);
        IList<TSource> MapOrEmptyList(IEnumerable<TValue> sourceList);
        IList<TValue> Map(IEnumerable<TSource> sourceList);
        IList<TValue> MapOrEmptyList(IEnumerable<TSource> sourceList);
        TSource Map(TValue value);
        TValue Map(TSource source);
    }
}
