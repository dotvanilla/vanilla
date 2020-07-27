namespace WebAssembly {

    /**
     * Object manager for VB.NET webassembly application.
    */
    export module ObjectManager {

        let streamReader: vanilla.stringReader;
        let objectReader: vanilla.objectReader;
        let arrayReader: vanilla.arrayReader;

        /**
         * 在这里主要是为了避免和内部的数值产生冲突
        */
        let hashCode: number = -999999999;
        let hashTable: object = {};
        let textCache: object = {};

        let loadedMemory: vanilla.WasmMemory;

        export function getLoadedMemory(): vanilla.WasmMemory {
            return loadedMemory;
        }

        /**
         * Load WebAssembly memory buffer into Javascript runtime.
        */
        export function load(bytes: vanilla.WasmMemory): void {
            streamReader = new vanilla.stringReader(bytes);
            objectReader = new vanilla.objectReader(bytes);
            arrayReader = new vanilla.arrayReader(bytes);
            loadedMemory = bytes;
            hashCode += 100;
        }

        export function printTextCache() {
            console.table(textCache);
        }

        export function printObjectCache() {
            console.table(hashTable);
        }

        /**
         * Read text data from WebAssembly runtime its memory block
         * 
         * @param intptr The memory pointer
        */
        export function readText(intptr: number): string {
            if (intptr in textCache) {
                return textCache[intptr];
            } else if ((intptr in hashTable) && typeof hashTable[intptr] == "string") {
                return hashTable[intptr];
            } else {
                let cache = streamReader.readText(intptr);
                addText(cache);
                return cache;
            }
        }

        export function addText(text: string): number {
            var key: number = hashCode;

            textCache[key] = text;
            hashCode++;

            return key;
        }

        export function readArray(intPtr: number): any[] {
            return arrayReader.array(intPtr);
        }

        /**
         * Get a object using its hash code
         * 
         * @returns If object not found, null will be returns
        */
        export function getObject(key: number): any {
            if (key in hashTable) {
                // get javascript object
                return hashTable[key];
            } else if (GarbageCollection.exists(key)) {
                if (GarbageCollection.classOf(key) == typeAlias.array) {
                    // get webassembly array
                    return arrayReader.array(key);
                } else {
                    // get webassembly object
                    return objectReader.readObject(key);
                }
            } else {
                return null;
            }
        }

        export function isNull(intPtr: number): boolean {
            return !(intPtr in hashTable);
        }

        export function isText(intPtr: number): boolean {
            return intPtr in textCache;
        }

        export function getType(hashCode: number): string {
            if (hashCode in hashTable) {
                let type: string;
                let obj: any = hashTable[hashCode];

                if (Array.isArray(obj)) {
                    return "array"
                }

                if ((type = typeof obj) == "object") {
                    return obj.constructor.name;
                } else {
                    return type;
                }
            } else {
                return "void";
            }
        }

        /**
         * Add any object to a internal hashTable and then returns its hash code.
        */
        export function addObject(o: any): number {
            let key: number = hashCode;

            hashTable[key] = o;
            hashCode++;

            return key;
        }
    }
}