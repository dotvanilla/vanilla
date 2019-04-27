namespace vanilla.Wasm {

    export module FunctionApi {

        export interface IWasmFunc {
            (...param: any[]): void;

            /**
             * 当前的这个函数在WebAssembly导出来的函数的申明原型
            */
            WasmPrototype: () => any;
        }

        /**
         * 主要是创建一个对参数的封装函数，因为WebAssembly之中只有4中基础的数值类型
         * 所以字符串，对象之类的都需要在这里进行封装之后才能够被传递进入WebAssembly
         * 运行时环境之中
        */
        export function buildApiFunc(func: object): IWasmFunc {
            let ObjMgr = WebAssembly.ObjectManager;
            let api: IWasmFunc = <any>function () {
                let intptr: number = (<any>func).apply(this, buildArguments(<any>arguments));
                let result

                if (ObjMgr.isText(intptr)) {
                    result = ObjMgr.readText(intptr);
                } else if (!ObjMgr.isNull(intptr)) {
                    result = ObjMgr.getObject(intptr);
                } else {
                    result = intptr;
                }

                if (Wasm.showDebugMessage()) {
                    console.log("Strings in WebAssembly memory:");
                    WebAssembly.ObjectManager.printTextCache();
                    console.log("Objects in WebAssembly memory:");
                    WebAssembly.ObjectManager.printObjectCache();
                }

                return result;
            }

            api.WasmPrototype = <any>func;

            return api;
        }

        function buildArguments(args: any[]): any[] {
            let params: any[] = [];
            let value: any;

            for (var i = 0; i < args.length; i++) {
                value = args[i];

                if (!value || typeof value == "undefined") {
                    // zero intptr means nothing or value 0
                    value = 0;
                } else if (typeof value == "string") {
                    value = WebAssembly.ObjectManager.addText(value);
                } else if (typeof value == "object") {
                    value = WebAssembly.ObjectManager.addObject(value);
                } else if (typeof value == "boolean") {
                    value = value ? 1 : 0;
                } else {
                    // do nothing
                }

                params.push(value);
            }

            return params;
        }
    }
}