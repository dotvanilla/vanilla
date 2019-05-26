namespace WebAssembly {

    export module GarbageCollection {

        const allocates: object = {};
        const cacheOfmeta: object = {};

        export function addObject(addressOf: number, class_id: number) {
            allocates[addressOf] = class_id;
        }

        export function getType(addressOf: number): classMeta {
            return cacheOfmeta[allocates[addressOf]];
        }

        export function sizeOf(addressOf: number) {
            let meta: classMeta = getType(addressOf);
            let size: number = meta.allocateSize;

            if (isNullOrUndefined(size) || size <= 0) {
                // calculate size and write into cache
                size = classSize(meta);
                meta.allocateSize = size;
            }

            return size;
        }

        export function classSize(meta: classMeta): number {

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

    export interface classMeta {
        namespace: string;
        class: string;
        class_id: number;
        isStruct: boolean;
        fields: object;

        /**
         * 在完成加载之后计算之后在js环境之中写进来的
        */
        allocateSize: number;
    }

    export interface type {
        type: typeAlias;
        generic: type[];
        raw: string;
    }

    /**
     * The compiler type alias
    */
    export enum typeAlias {
        /**
         * Function or expression have no value returns
        */
        void = -1,
        any,
        i32,
        i64,
        f32,
        f64,
        string,
        boolean,

        /**
         * Fix length array in WebAssembly runtime
        */
        array,

        /**
         * Array list in javascript runtime
        */
        list,

        /** 
         * Javascript object
        */
        table,

        /**
         * 所有用户自定义的引用类型
        */
        intptr
    }
}