namespace vanilla {

    export class objectReader extends memoryReader {

        public constructor(memory: WasmMemory, public littleEndian: boolean = true) {
            super(memory, littleEndian);
        }

        public readObject(intptr: number): object {
            var meta: classMeta = WebAssembly.GarbageCollection.getType(intptr);
            var fields = meta.fields;
            var obj: object = {};
            var offset: number = intptr;
            var value: any;
            var fieldType: type;
            var type: typeAlias;

            if (TypeScript.logging.outputEverything) {
                console.log(`typeof(&${intptr}) is (&${meta.class_id})${meta.class}`);
            }

            for (let name in fields) {
                fieldType = <type>fields[name];
                type = fieldType.type;

                if (TypeScript.logging.outputEverything) {
                    console.log(`  > ${name} as ${type} = &${offset}`);
                }

                switch (type) {
                    case typeAlias.f32:
                        value = this.get32BitNumber(offset, true);
                        offset += 4;
                        break;
                    case typeAlias.f64:
                        value = this.get64BitNumber(offset, true);
                        offset += 8;
                        break;
                    case typeAlias.i32:
                        value = this.get32BitNumber(offset, false);
                        offset += 4;
                        break;
                    case typeAlias.i64:
                        value = this.get64BitNumber(offset, false);
                        offset += 8;
                        break;
                    case typeAlias.intptr:
                        let class_id = WebAssembly.GarbageCollection.class_id(fieldType);
                        let class_info = WebAssembly.GarbageCollection.lazyGettype(class_id);

                        if (class_info.isStruct) {
                            value = this.readObject(offset);
                            offset += WebAssembly.GarbageCollection.classSize(class_info);
                        } else {
                            // read intptr
                            intptr = this.get32BitNumber(offset, false);
                            // read object value by intptr
                            value = this.readObject(intptr);
                            offset += 4;
                        }

                        break;
                    case typeAlias.string:
                        // 4 byte intptr
                        intptr = this.get32BitNumber(offset, false);
                        value = WebAssembly.ObjectManager.readText(intptr);
                        offset += 4;

                        if (TypeScript.logging.outputEverything) {
                            console.log(`read string from &${intptr}:`);
                            console.log(value);
                        }

                        break;
                    default:

                        throw "not implement";
                }

                obj[name] = value;
            }

            if (TypeScript.logging.outputEverything) {
                console.log('!end_of_read_object');
            }

            return obj;
        }
    }
}