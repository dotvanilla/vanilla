/// <reference path="../modules/linq.d.ts" />
declare namespace WebAssembly {
    /**
     * 在这个模块之中，所有的obj都是指针类型
    */
    module JsArray {
        function push(array: number, obj: number): number;
        function pop(array: number): number;
        function indexOf(array: number, obj: number): number;
        /**
         * @param size If this parameter is a negative value, then an empty
         *      will be returns.
        */
        function create(size: number): number;
        function get(array: number, index: number): number;
        function set(array: number, index: number, value: number): number;
        function length(array: number): number;
    }
    class WasmArray {
        type: number;
        items: any[];
        private intptrs;
        readonly length: number;
        /**
         * @param type 0 for number, 1 for string, 2 for others
        */
        constructor(type: number);
        set(index: number, element: number): void;
        get(index: number): number;
    }
}
declare namespace WebAssembly {
    module XMLHttpRequest {
        function get(url: number): number;
    }
}
declare namespace WebAssembly {
    /**
     * Javascript debug console
    */
    module Console {
        function log(message: number): void;
        function warn(message: number): void;
        function info(message: number): void;
        function error(message: number): void;
        function table(obj: number): void;
        function trace(message: number): void;
        function debug(message: number): void;
    }
}
declare namespace WebAssembly {
    module Document {
        function getElementById(id: number): number;
        function writeElementText(nodeObj: number, text: number): void;
        function writeElementHtml(node: number, text: number): void;
        function createElement(tag: number): number;
        function setAttribute(node: number, attr: number, value: number): void;
        function appendChild(parent: number, node: number): void;
    }
}
declare namespace WebAssembly {
    /**
     * Url location api
    */
    module Location {
    }
}
declare namespace WebAssembly {
}
declare namespace WebAssembly {
    /**
     * Object manager for VB.NET webassembly application.
    */
    module ObjectManager {
        function getLoadedMemory(): vanilla.WasmMemory;
        /**
         * Load WebAssembly memory buffer into Javascript runtime.
        */
        function load(bytes: vanilla.WasmMemory): void;
        function printTextCache(): void;
        function printObjectCache(): void;
        /**
         * Read text data from WebAssembly runtime its memory block
         *
         * @param intptr The memory pointer
        */
        function readText(intptr: number): string;
        function addText(text: string): number;
        /**
         * Get a object using its hash code
         *
         * @returns If object not found, null will be returns
        */
        function getObject(key: number): any;
        function isNull(intPtr: number): boolean;
        function isText(intPtr: number): boolean;
        function getType(hashCode: number): string;
        /**
         * Add any object to a internal hashTable and then returns its hash code.
        */
        function addObject(o: any): number;
    }
}
declare namespace WebAssembly {
    /**
     * A module contains string related api for simulate
     * ``Microsoft.VisualBasic.Strings`` module.
    */
    module Strings {
        function Mid(text: number, from: number, length: number): number;
        function Len(text: number): number;
        function UCase(text: number): number;
        function LCase(text: number): number;
    }
}
declare namespace WebAssembly {
    module RegularExpression {
        function regexp(pattern: number, flags: number): number;
        function replace(text: number, pattern: number, replacement: number): number;
        /**
         * Returns a Boolean value that indicates whether or not a pattern exists in a
         * searched string.
         *
         * @param string String on which to perform the search.
        */
        function test(pattern: number, string: number): number;
        /**
         * Executes a search on a string using a regular expression pattern, and returns an array
         * containing the results of that search.
         *
         * @param string The String object or string literal on which to perform the search.
        */
        function exec(pattern: number, string: number): number;
    }
}
declare namespace WebAssembly {
    /**
     * String api from javascript.
    */
    module JsString {
        function fromCharCode(n: number): number;
        function charCodeAt(text: number, index: number): number;
        function charAt(text: number, index: number): number;
        function join(text: number, deli: number): number;
        function toString(obj: number): number;
        function add(a: number, b: number): number;
        function length(text: number): number;
        function replace(text: number, find: number, replacement: number): number;
        function indexOf(input: number, find: number): number;
    }
}
declare namespace vanilla {
    interface WebAssembly {
        instantiate(module: Uint8Array, dependencies: object): IWasm;
    }
    interface IWasm {
        instance: WasmInstance;
    }
    interface WasmInstance {
        exports: {};
    }
    interface WasmMemory {
        buffer: ArrayBuffer;
    }
}
declare namespace vanilla {
    /**
     * The VisualBasic.NET application AssemblyInfo
    */
    class AssemblyInfo {
        AssemblyTitle: string;
        AssemblyDescription: string;
        AssemblyCompany: string;
        AssemblyProduct: string;
        AssemblyCopyright: string;
        AssemblyTrademark: string;
        Guid: string;
        AssemblyVersion: string;
        AssemblyFileVersion: string;
        constructor(AssemblyTitle: string, AssemblyDescription: string, AssemblyCompany: string, AssemblyProduct: string, AssemblyCopyright: string, AssemblyTrademark: string, Guid: string, AssemblyVersion: string, AssemblyFileVersion: string);
        toString(): string;
        static readAssemblyInfo(assm: IWasm): AssemblyInfo;
    }
}
declare namespace vanilla {
    interface AssemblyExport {
        AssemblyInfo: AssemblyInfo;
        memory: WasmMemory;
    }
    interface RunDelegate {
        (assm: AssemblyExport): void;
    }
    interface Config {
        /**
         * A lambda function for run your VisualBasic.NET application.
        */
        run: RunDelegate;
        /**
         * Your custom javascript api that imports for your VisualBasic app, like:
         *
         * 1. third-part javascript library,
         * 2. the WebAssembly module export api which comes from another VisualBasic.NET or C/C++ application
         * 3. WebGL, Unity api etc for game development in VisualBasic.NET
        */
        imports?: {};
        /**
         * Options for javascript build-in api
        */
        api?: apiOptions;
        /**
         * The VB.NET application memory configuration.
        */
        page: {
            init?: number;
            /**
             * Config max memory page size for your VB.NET app
            */
            max?: number;
        };
    }
    /**
     * The javascript internal api imports option for VB.NET WebAssembly
    */
    interface apiOptions {
        /**
         * Add javascript html document api imports for VB.NET?
        */
        document?: boolean;
        /**
         * Add javascript debugger console api imports for VB.NET?
        */
        console?: boolean;
        /**
         * Add javascript http request api like get/post or WebSocket api imports for VB.NET?
        */
        http?: boolean;
        /**
         * Add string and text api from javascript, like String and RegExp imports for VB.NET?
        */
        text?: boolean;
        array?: boolean;
    }
}
declare namespace vanilla {
    /**
     * The web assembly helper
    */
    module Wasm {
        /**
         * Run the compiled VisualBasic.NET assembly module
         *
         * > This function add javascript ``math`` module as imports object automatic
         *
         * @param module The ``*.wasm`` module file path
         * @param run A action delegate for utilize the VB.NET assembly module
         *
        */
        function RunAssembly(module: string, opts: Config): void;
        function showDebugMessage(opt?: boolean): boolean;
    }
}
declare namespace vanilla.Wasm.Application {
    /**
     * Create the VisualBasic.NET application module
    */
    function BuildAppModules(wasm: object): any;
}
declare namespace vanilla.Wasm.Application {
    interface IWasmFunc {
        (...param: any[]): void;
        /**
         * 当前的这个函数在WebAssembly导出来的函数的申明原型
        */
        WasmPrototype: () => any;
    }
}
declare namespace vanilla.Wasm.Application {
    /**
     * A helper module for create function wrapper
    */
    module FunctionApi {
        /**
         * 主要是创建一个对参数的封装函数，因为WebAssembly之中只有4中基础的数值类型
         * 所以字符串，对象之类的都需要在这里进行封装之后才能够被传递进入WebAssembly
         * 运行时环境之中
        */
        function buildApiFunc(func: object): IWasmFunc;
    }
}
declare namespace vanilla {
    class memoryReader {
        protected buffer: ArrayBuffer;
        constructor(bytechunks: WasmMemory);
        sizeOf(intPtr: number): number;
    }
    /**
     * Read string helper from WebAssembly memory.
    */
    class stringReader extends memoryReader {
        private decoder;
        /**
         * @param memory The memory buffer
        */
        constructor(memory: WasmMemory);
        /**
         * Read text from WebAssembly memory buffer.
        */
        readTextRaw(offset: number, length: number): string;
        readText(intPtr: number): string;
    }
}
declare namespace vanilla {
    class arrayReader extends memoryReader {
        /**
         * @param memory The memory buffer
        */
        constructor(memory: WasmMemory);
        array(intPtr: number, type: string): number[];
        private static sizeOf;
        private static getReader;
        toInt32(intPtr: number): number;
    }
}
