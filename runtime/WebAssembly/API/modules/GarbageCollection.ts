namespace WebAssembly {

    /**
     * 对WebAssembly模块的内存回收帮助模块
    */
    export module GarbageCollection {

        const allocates: object = {};
        const cacheOfmeta: object = {};

        export function addObject(addressOf: number, class_id: number) {
            allocates[addressOf] = class_id;
        }

        export function getType(addressOf: number): classMeta {
            let class_id: number = allocates[addressOf];
            let type: classMeta = lazyGettype(class_id);

            return type;
        }

        export function lazyGettype(class_id: number): classMeta {
            if (!(class_id in cacheOfmeta)) {
                // read class meta from webassembly memory
                let base64 = ObjectManager.readText(class_id);
                let json = Base64.decode(base64);

                cacheOfmeta[class_id] = JSON.parse(json);
            }

            return cacheOfmeta[class_id];
        }

        export function sizeOf(addressOf: number): number {
            let meta: classMeta = getType(addressOf);
            let size: number = meta.allocateSize;

            if (isNullOrUndefined(size) || size <= 0) {
                // calculate size and write into cache
                size = classSize(meta);
                meta.allocateSize = size;
            }

            return size;
        }

        /**
         * 只需要计算所有的字段的大小即可
        */
        export function classSize(meta: classMeta): number {
            let size: number = 0;
            let fieldType: type;

            for (let fieldName in meta.fields) {
                fieldType = (<type>meta.fields[fieldName]);
                size += typeSize(fieldType);
            }

            return size;
        }

        export function typeSize(type: type): number {
            let fieldType: typeAlias = type.type;

            if (fieldType == typeAlias.void) {
                return 0;
            } else if (fieldType == typeAlias.f64 || fieldType == typeAlias.i64) {
                return 8;
            } else if (fieldType == typeAlias.intptr) {
                let id: number = class_id(type);
                let cls: classMeta = lazyGettype(id);

                if (cls.isStruct) {
                    return classSize(cls);
                } else {
                    // size of intptr is i32 4 bytes
                    return 4;
                }

            } else {
                return 4;
            }
        }

        export function class_id(type: type): number {
            if (type.type != typeAlias.intptr) {
                return type.type;
            } else {
                let id: string | number = /\[\d+\]/.exec(type.raw)[0];

                id = (<string>id).substr(1, id.length - 2);
                id = parseInt(<string>id);

                return <number>id;
            }
        }
    }
}