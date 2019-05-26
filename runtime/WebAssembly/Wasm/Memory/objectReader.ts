namespace vanilla {

    export class objectReader extends memoryReader {

        public constructor(memory: WasmMemory) {
            super(memory);
        }

        public readObject(intptr: number): object {
            let class: WebAssembly.classMeta = WebAssembly.GarbageCollection.getType(intptr);
        }
    }
}