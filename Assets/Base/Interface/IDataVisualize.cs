
public interface IDataVisualize<T>: IDataHolder<T> where T : IIdentifiedData
{
    void Set(T info, OwnedState ownedState);
}

public interface IDataHolder<T> where T : IIdentifiedData
{
    T info { get; }
}