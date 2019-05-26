namespace WebAssembly {

    export module GarbageCollection {

        const allocates: object = {};
        const cacheOfmeta: object = {};

        export function addObject(addressOf: number, class_id: number) {
            allocates[addressOf] = class_id;
        }

        export function getType(addressOf: number) {
            cacheOfmeta[allocates[addressOf]];
        }
    }


}