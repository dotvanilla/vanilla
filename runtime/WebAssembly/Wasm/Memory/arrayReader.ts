/// <reference path="memoryReader.ts" />

namespace vanilla {

    export class arrayReader extends memoryReader {

        /**
         * @param memory The memory buffer
        */
        public constructor(memory: WasmMemory) {
            super(memory);
        }

        /**
         * 使用这个函数只会读取数值向量
        */
        public vector(intPtr: number): number[] {
            // 数组的起始前4个字节是数组的元素类型
            let class_id: number = this.toInt32(intPtr);
            // 然后是元素的数量
            let length: number = this.toInt32(intPtr + 4);
            let buffer = new DataView(this.buffer, intPtr + 8);

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
            return new DataView(this.buffer, intPtr, 4).getInt32(0, true);
        }
    }
}