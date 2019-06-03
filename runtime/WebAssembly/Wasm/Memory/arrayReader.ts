﻿/// <reference path="memoryReader.ts" />

namespace vanilla {

    export class arrayReader extends memoryReader {

        /**
         * @param memory The memory buffer
        */
        public constructor(memory: WasmMemory, public littleEndian: boolean = true) {
            super(memory, littleEndian);
        }

        private static toString(type: type): string | number {
            switch (type.type) {
                case typeAlias.i32:
                    return "i32";
                case typeAlias.f32:
                    return "f32";
                case typeAlias.i64:
                    return "i64";
                case typeAlias.f64:
                    return "f64";
                case typeAlias.intptr:
                    // 因为可能是structure，并且structure是直接保存在内存之中的
                    // 所以在这里需要做一些额外的判断
                    let meta = WebAssembly.GarbageCollection.lazyGettype(parseInt(type.raw));

                    if (meta.isStruct) {
                        return WebAssembly.GarbageCollection.classSize(meta);
                    } else {
                        return "i32";
                    }

                default:
                    return "i32";
            }
        }

        /**
         * 使用这个函数只会读取数值向量
        */
        public vector(intPtr: number, class_id: type = Wasm.typeOf(this.toInt32(intPtr))): number[] {
            // 数组的起始前4个字节是数组的元素类型         
            // 然后是元素的数量
            let length: number = this.toInt32(intPtr + 4);
            let buffer = new DataView(this.buffer, intPtr + 8);
            let type: string | number = arrayReader.toString(class_id);
            // The output data buffer
            let data: number[] = [];

            if (typeof type == "string") {
                let load = arrayReader.getReader(buffer, type, this.littleEndian);
                let offset: number = arrayReader.sizeOf(type);

                intPtr = 0;

                for (var i: number = 0; i < length; i++) {
                    data.push(load(intPtr));
                    intPtr = intPtr + offset;
                }
            } else {
                // 这个是一个结构体数组
                // 将数组内的每一个元素的offset位置都读出来

                // 跳过前4+4个字节
                intPtr += 8;

                for (var i: number = 0; i < length; i++) {
                    data.push(intPtr + i * type);
                }
            }

            return data;
        }

        public array(intPtr: number): any[] {
            let type = Wasm.typeOf(this.toInt32(intPtr));
            let vector = this.vector(intPtr, type);

            if (type.type == typeAlias.intptr) {
                // all of the element in vector is intptr
                return vector.map(p => WebAssembly.ObjectManager.getObject(p));
            } else if (type.type == typeAlias.string) {
                return vector.map(p => WebAssembly.ObjectManager.readText(p));
            } else {
                return <any>vector;
            }
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

        private static getReader(buffer: DataView, type: string, littleEndian: boolean): (offset: number) => number {
            if (type == "i32") {
                return function (offset) {
                    return buffer.getInt32(offset, littleEndian);
                }
            } else if (type == "i64") {
                throw "not implements";
            } else if (type == "f32") {
                return function (offset) {
                    return buffer.getFloat32(offset, littleEndian);
                }
            } else if (type == "f64") {
                return function (offset) {
                    return buffer.getFloat64(offset, littleEndian);
                }
            } else {
                throw `Unavailable for ${type}`;
            }
        }

        public toInt32(intPtr: number): number {
            return new DataView(this.buffer, intPtr, 4).getInt32(0, this.littleEndian);
        }
    }
}