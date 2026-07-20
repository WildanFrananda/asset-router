window.dbPersistence = {
    dbName: "AssetRouterFS",
    storeName: "files",
    key: "local_allocation.db",

    _open() {
        return new Promise((resolve, reject) => {
            const req = indexedDB.open(this.dbName, 1);
            req.onupgradeneeded = () => req.result.createObjectStore(this.storeName);
            req.onsuccess = () => resolve(req.result);
            req.onerror = () => reject(req.error);
        });
    },

    async save(bytes) {
        const db = await this._open();
        return new Promise((resolve, reject) => {
            const tx = db.transaction(this.storeName, "readwrite");
            tx.objectStore(this.storeName).put(bytes, this.key);
            tx.oncomplete = () => { db.close(); resolve(); };
            tx.onerror = () => { db.close(); reject(tx.error); };
        });
    },

    async load() {
        const db = await this._open();
        return new Promise((resolve, reject) => {
            const tx = db.transaction(this.storeName, "readonly");
            const req = tx.objectStore(this.storeName).get(this.key);
            req.onsuccess = () => {
                db.close();
                resolve(req.result ? new Uint8Array(req.result) : null);
            };
            req.onerror = () => { db.close(); reject(req.error); };
        });
    }
};
