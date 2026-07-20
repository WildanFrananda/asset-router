namespace AssetRouter.Core.Interfaces;

public interface IDbPersistence {
    Task RestoreAsync();
    Task PersistAsync();
}