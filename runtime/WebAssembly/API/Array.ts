namespace WebAssembly {

    /**
     * 在这个模块之中，所有的obj都是指针类型
    */
    export module JsArray {

        export function push(array: number, obj: number): number {
            let a: number[] = ObjectManager.getObject(array);
            a.push(obj);
            return a.length;
        }

        export function pop(array: number): number {
            let a: number[] = ObjectManager.getObject(array);
            return a.pop();
        }

        export function indexOf(array: number, obj: number): number {
            let a: number[] = ObjectManager.getObject(array);
            return a.indexOf(obj);
        }

        /**
         * @param size If this parameter is a negative value, then an empty
         *      will be returns.
        */
        export function create(size: number): number {
            if (!size || size == undefined || size <= 0) {
                return ObjectManager.addObject([]);
            } else {
                return ObjectManager.addObject(new Array(size));
            }
        }

        export function get(array: number, index: number): number {
            let a: number[] = ObjectManager.getObject(array);
            let obj: number = a[index];

            return obj;
        }

        export function set(array: number, index: number, value: number): number {
            let a: number[] = ObjectManager.getObject(array);
            a[index] = value;
            return array;
        }

        export function length(array: number): number {
            let a: number[] = ObjectManager.getObject(array);
            return a.length;
        }

        export function toArray(array: number): number {
            let a: number[] = ObjectManager.getObject(array);
            let copy = [...a];
            return ObjectManager.addObject(copy);
        }
    }

    export class WasmArray {

        public items: any[];
        private intptrs: number[];

        public get length(): number {
            return this.items.length;
        }

        /**
         * @param type 0 for number, 1 for string, 2 for others
        */
        public constructor(public type: number) {
            this.items = [];
            this.intptrs = [];
        }

        public set(index: number, element: number) {
            let obj = ObjectManager.getObject(element);
            this.items[index] = obj;
            this.intptrs[index] = element;
        }

        public get(index: number): number {
            return this.intptrs[index];
        }
    }
}