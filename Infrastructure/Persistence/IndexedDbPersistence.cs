namespace AssetRouter.Infrastructure.Persistence;

using AssetRouter.Core.Interfaces;
using Microsoft.JSInterop;

public class IndexedDbPersistence(IJSRuntime js) : IDbPersistence {
    private const string DbFileName = "local_allocation.db";

    public async Task RestoreAsync() {
        var bytes = await js.InvokeAsync<byte[]?>("dbPersistence.load");

        if (bytes is { Length: > 0 }) {
            File.WriteAllBytes(DbFileName, bytes);
        }
    }

    public async Task PersistAsync() {
        if (!File.Exists(DbFileName)) {
            return;
        }

        var bytes = File.ReadAllBytes(DbFileName);
        await js.InvokeVoidAsync("dbPersistence.save", bytes);
    }
}
