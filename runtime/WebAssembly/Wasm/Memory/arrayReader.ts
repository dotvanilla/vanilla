﻿namespace vanilla {

    export class arrayReader extends memoryReader {

        /**
         * @param memory The memory buffer
        */
        public constructor(memory: WasmMemory) {
            super(memory);
        }

        public array(intPtr: number, type: string): number[] {
            // 数组的起始前4个字节是数组长度
            let length: number = this.toInt32(intPtr);
            let uint8s = new Uint8Array(this.buffer, intPtr + 4);
            let buffer = new DataView(uint8s);

            // The output data buffer
            let data: number[] = [];
            let load = arrayReader.getReader(buffer, type);
            let offset: number = arrayReader.sizeOf(type);

            intPtr = 0;

            for (var i: number = 0; i < length; i++) {
                data.push(load(intPtr));
                intPtr = intPtr + offset;
            }

            return data;
        }

        private static sizeOf(type: string): number {
            if (type == "i32" || type == "f32") {
                return 4;
            } else if (type == "i64" || type == "f64") {
                return 8;
            } else {
                throw `Unavailable for ${type}`;
            }
        }

        private static getReader(buffer: DataView, type: string): (offset: number) => number {
            if (type == "i32") {
                return function (offset) {
                    return buffer.getInt32(offset);
                }
            } else if (type == "i64") {
                throw "not implements";
            } else if (type == "f32") {
                return function (offset) {
                    return buffer.getFloat32(offset);
                }
            } else if (type == "f64") {
                return function (offset) {
                    return buffer.getFloat64(offset);
                }
            } else {
                throw `Unavailable for ${type}`;
            }
        }

        public toInt32(intPtr: number): number {
            let uint8s = new Uint8Array(this.buffer, intPtr, 4);
            let view = new DataView(uint8s);

            return view.getInt32(0);
        }
    }
}