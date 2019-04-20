/// <reference path="../../modules/linq.d.ts" />
declare namespace WebAssembly {
    /**
     * 在这个模块之中，所有的obj都是指针类型
    */
    module JsArray {
        function push(array: number, obj: number): number;
        function pop(array: number): number;
        function indexOf(array: number, obj: number): number;
        function create(): number;
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
    /**
     * Object manager for VB.NET webassembly application.
    */
    module ObjectManager {
        /**
         * Load WebAssembly memory buffer into Javascript runtime.
        */
        function load(bytes: TypeScript.WasmMemory): void;
        function printTextCache(): void;
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
declare namespace TypeScript {
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
declare namespace TypeScript {
    interface Config {
        /**
         * A lambda function for run your VisualBasic.NET application.
        */
        run: Delegate.Sub;
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
declare namespace TypeScript {
    interface IWasmFunc {
        (): void;
        /**
         * 当前的这个函数在WebAssembly导出来的函数的申明原型
        */
        WasmPrototype: () => any;
    }
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
    }
}
declare namespace TypeScript {
    class memoryReader {
        protected buffer: ArrayBuffer;
        constructor(bytechunks: TypeScript.WasmMemory);
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
