﻿namespace vanilla {

    export class memoryReader {

        protected buffer: ArrayBuffer;
        protected littleEndian: boolean = true;

        public constructor(bytechunks: WasmMemory, littleEndian: boolean = true) {
            this.buffer = bytechunks.buffer;
            this.littleEndian = littleEndian;
        }

        /**
         * f32/i32
        */
        public get32BitNumber(offset: number, floatPoint = false): number {
            let view = new DataView(this.buffer, offset, 4);

            if (floatPoint) {
                return view.getFloat32(0, this.littleEndian);
            } else {
                return view.getInt32(0, this.littleEndian);
            }
        }

        public get64BitNumber(offset: number, floatPoint = false): number {
            let view = new DataView(this.buffer, offset, 8);

            if (floatPoint) {
                return view.getFloat64(0, this.littleEndian);
            } else {
                throw "Int64 is not implement...";
            }
        }

        /**
         * size of null terminated string
        */
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