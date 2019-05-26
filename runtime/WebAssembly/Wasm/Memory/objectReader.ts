namespace vanilla {

    export class objectReader extends memoryReader {

        public constructor(memory: WasmMemory) {
            super(memory);
        }

        public readObject(intptr: number): object {
            var meta: classMeta = WebAssembly.GarbageCollection.getType(intptr);
            var fields = meta.fields;
            var obj: object = {};
            var offset: number = 0;

            for (let name in fields) {
                let type: type = fields[name];

            }

            return obj;
        }
    }
}