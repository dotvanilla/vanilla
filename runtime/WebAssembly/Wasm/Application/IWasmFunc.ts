namespace vanilla.Wasm.Application {

    export interface IWasmFunc {
        (...param: any[]): void;

        /**
         * 当前的这个函数在WebAssembly导出来的函数的申明原型
        */
        WasmPrototype: () => any;
    }
}