namespace WebAssembly {

    /**
     * Javascript debug console
    */
    export module Console {

        // 因为message可能是任意的JavaScript对象
        // 所以在这里不进行直接文本字符串的读取
        // 需要做一些额外的处理操作

        export function log(message: number) {
            console.log(Any(message));
        }

        export function warn(message: number) {
            console.warn(Any(message));
        }

        export function info(message: number) {
            console.info(Any(message));
        }

        export function error(message: number) {
            console.error(Any(message));
        }

        export function table(obj: number) {
            console.table(Any(obj));
        }

        export function trace(message: number) {
            console.trace(Any(message));
        }

        export function debug(message: number) {
            console.debug(Any(message));
        }

        function Any(intPtr: number): any {
            if (intPtr < 0) {
                // 可能是一个指针，因为在这里指针都是小于零的
                if (ObjectManager.isText(intPtr)) {
                    return ObjectManager.readText(intPtr);
                } else if (ObjectManager.isNull(intPtr)) {
                    // 是一个负数
                    return intPtr;
                } else {
                    return ObjectManager.getObject(intPtr);
                }
            } else if (GarbageCollection.exists(intPtr)) {
                // 是一个正实数，并且在GC之中存在定义
                // 则是webassembly内存之中的一个用户对象实例
                return ObjectManager.getObject(intPtr);
            } else {
                // 如何处理正实数？
                return ObjectManager.readText(intPtr);
            }
        }
    }
}