namespace vanilla {

    export class memoryReader {

        protected buffer: ArrayBuffer;

        public constructor(bytechunks: WasmMemory) {
            this.buffer = bytechunks.buffer;
        }

        public sizeOf(intPtr: number): number {
            let buffer = new Uint8Array(this.buffer, intPtr);
            let size: number = buffer.findIndex(b => b == 0);

            return size;
        }
    }

    /**
     * Read string helper from WebAssembly memory.
    */
    export class stringReader extends memoryReader {

        private decoder: TextDecoder = new TextDecoder();

        /**
         * @param memory The memory buffer
        */
        public constructor(memory: WasmMemory) {
            super(memory);
        }

        /**
         * Read text from WebAssembly memory buffer.
        */
        public readTextRaw(offset: number, length: number): string {
            let str = new Uint8Array(this.buffer, offset, length);
            let text: string = this.decoder.decode(str);

            return text;
        }

        public readText(intPtr: number): string {
            return this.readTextRaw(intPtr, this.sizeOf(intPtr));
        }
    }   
}