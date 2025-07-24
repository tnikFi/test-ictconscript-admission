using Domain.Common;

namespace Common.Interfaces;

public interface IDataSeed<out T> where T : BaseEntity
{
    public static abstract IEnumerable<T> Seed();
}