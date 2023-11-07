namespace Archero
{
    public interface ISpawnPoint : ILocatable
    {
        SpawnPointType Type { get; }
    }
}
