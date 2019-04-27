/// <reference path="../../modules/linq.d.ts" />
var WebAssembly;
(function (WebAssembly) {
    /**
     * 在这个模块之中，所有的obj都是指针类型
    */
    let JsArray;
    (function (JsArray) {
        function push(array, obj) {
            let a = WebAssembly.ObjectManager.getObject(array);
            a.push(obj);
            return a.length;
        }
        JsArray.push = push;
        function pop(array) {
            let a = WebAssembly.ObjectManager.getObject(array);
            return a.pop();
        }
        JsArray.pop = pop;
        function indexOf(array, obj) {
            let a = WebAssembly.ObjectManager.getObject(array);
            return a.indexOf(obj);
        }
        JsArray.indexOf = indexOf;
        /**
         * @param size If this parameter is a negative value, then an empty
         *      will be returns.
        */
        function create(size) {
            if (!size || size == undefined || size <= 0) {
                return WebAssembly.ObjectManager.addObject([]);
            }
            else {
                return WebAssembly.ObjectManager.addObject(new Array(size));
            }
        }
        JsArray.create = create;
        function get(array, index) {
            let a = WebAssembly.ObjectManager.getObject(array);
            let obj = a[index];
            return obj;
        }
        JsArray.get = get;
        function set(array, index, value) {
            let a = WebAssembly.ObjectManager.getObject(array);
            a[index] = value;
            return array;
        }
        JsArray.set = set;
        function length(array) {
            let a = WebAssembly.ObjectManager.getObject(array);
            return a.length;
        }
        JsArray.length = length;
    })(JsArray = WebAssembly.JsArray || (WebAssembly.JsArray = {}));
    class WasmArray {
        /**
         * @param type 0 for number, 1 for string, 2 for others
        */
        constructor(type) {
            this.type = type;
            this.items = [];
            this.intptrs = [];
        }
        get length() {
            return this.items.length;
        }
        set(index, element) {
            let obj = WebAssembly.ObjectManager.getObject(element);
            this.items[index] = obj;
            this.intptrs[index] = element;
        }
        get(index) {
            return this.intptrs[index];
        }
    }
    WebAssembly.WasmArray = WasmArray;
})(WebAssembly || (WebAssembly = {}));
var WebAssembly;
(function (WebAssembly) {
    /**
     * Object manager for VB.NET webassembly application.
    */
    let ObjectManager;
    (function (ObjectManager) {
        let streamReader;
        /**
         * 在这里主要是为了避免和内部的数值产生冲突
        */
        let hashCode = -999999999;
        let hashTable = {};
        let textCache = {};
        /**
         * Load WebAssembly memory buffer into Javascript runtime.
        */
        function load(bytes) {
            streamReader = new vanilla.stringReader(bytes);
            hashCode += 100;
        }
        ObjectManager.load = load;
        function printTextCache() {
            console.table(textCache);
        }
        ObjectManager.printTextCache = printTextCache;
        function printObjectCache() {
            console.table(hashTable);
        }
        ObjectManager.printObjectCache = printObjectCache;
        /**
         * Read text data from WebAssembly runtime its memory block
         *
         * @param intptr The memory pointer
        */
        function readText(intptr) {
            if (intptr in textCache) {
                return textCache[intptr];
            }
            else if ((intptr in hashTable) && typeof hashTable[intptr] == "string") {
                return hashTable[intptr];
            }
            else {
                let cache = streamReader.readText(intptr);
                addText(cache);
                return cache;
            }
        }
        ObjectManager.readText = readText;
        function addText(text) {
            var key = hashCode;
            textCache[key] = text;
            hashCode++;
            return key;
        }
        ObjectManager.addText = addText;
        /**
         * Get a object using its hash code
         *
         * @returns If object not found, null will be returns
        */
        function getObject(key) {
            if (key in hashTable) {
                return hashTable[key];
            }
            else {
                return null;
            }
        }
        ObjectManager.getObject = getObject;
        function isNull(intPtr) {
            return !(intPtr in hashTable);
        }
        ObjectManager.isNull = isNull;
        function isText(intPtr) {
            return intPtr in textCache;
        }
        ObjectManager.isText = isText;
        function getType(hashCode) {
            if (hashCode in hashTable) {
                let type;
                let obj = hashTable[hashCode];
                if (Array.isArray(obj)) {
                    return "array";
                }
                if ((type = typeof obj) == "object") {
                    return obj.constructor.name;
                }
                else {
                    return type;
                }
            }
            else {
                return "void";
            }
        }
        ObjectManager.getType = getType;
        /**
         * Add any object to a internal hashTable and then returns its hash code.
        */
        function addObject(o) {
            var key = hashCode;
            hashTable[key] = o;
            hashCode++;
            return key;
        }
        ObjectManager.addObject = addObject;
    })(ObjectManager = WebAssembly.ObjectManager || (WebAssembly.ObjectManager = {}));
})(WebAssembly || (WebAssembly = {}));
var WebAssembly;
(function (WebAssembly) {
    let XMLHttpRequest;
    (function (XMLHttpRequest) {
        function get(url) {
            throw "not implement!";
        }
        XMLHttpRequest.get = get;
    })(XMLHttpRequest = WebAssembly.XMLHttpRequest || (WebAssembly.XMLHttpRequest = {}));
})(WebAssembly || (WebAssembly = {}));
var WebAssembly;
(function (WebAssembly) {
    /**
     * Javascript debug console
    */
    let Console;
    (function (Console) {
        // 因为message可能是任意的JavaScript对象
        // 所以在这里不进行直接文本字符串的读取
        // 需要做一些额外的处理操作
        function log(message) {
            console.log(Any(message));
        }
        Console.log = log;
        function warn(message) {
            console.warn(Any(message));
        }
        Console.warn = warn;
        function info(message) {
            console.info(Any(message));
        }
        Console.info = info;
        function error(message) {
            console.error(Any(message));
        }
        Console.error = error;
        function table(obj) {
            console.table(Any(obj));
        }
        Console.table = table;
        function trace(message) {
            console.trace(Any(message));
        }
        Console.trace = trace;
        function debug(message) {
            console.debug(Any(message));
        }
        Console.debug = debug;
        function Any(intPtr) {
            if (intPtr < 0) {
                // 可能是一个指针，因为在这里指针都是小于零的
                if (WebAssembly.ObjectManager.isText(intPtr)) {
                    return WebAssembly.ObjectManager.readText(intPtr);
                }
                else if (WebAssembly.ObjectManager.isNull(intPtr)) {
                    // 是一个负数
                    return intPtr;
                }
                else {
                    return WebAssembly.ObjectManager.getObject(intPtr);
                }
            }
            else {
                // 如何处理正实数？
                return WebAssembly.ObjectManager.readText(intPtr);
            }
        }
    })(Console = WebAssembly.Console || (WebAssembly.Console = {}));
})(WebAssembly || (WebAssembly = {}));
var WebAssembly;
(function (WebAssembly) {
    let Document;
    (function (Document) {
        function getElementById(id) {
            let idText = WebAssembly.ObjectManager.readText(id);
            let node = document.getElementById(idText);
            return WebAssembly.ObjectManager.addObject(node);
        }
        Document.getElementById = getElementById;
        function writeElementText(nodeObj, text) {
            let node = WebAssembly.ObjectManager.getObject(nodeObj);
            let textVal = WebAssembly.ObjectManager.readText(text);
            node.innerText = textVal;
        }
        Document.writeElementText = writeElementText;
        function writeElementHtml(node, text) {
            let nodeObj = WebAssembly.ObjectManager.getObject(node);
            let htmlVal = WebAssembly.ObjectManager.readText(text);
            nodeObj.innerHTML = htmlVal;
        }
        Document.writeElementHtml = writeElementHtml;
        function createElement(tag) {
            let tagName = WebAssembly.ObjectManager.readText(tag);
            let node = document.createElement(tagName);
            return WebAssembly.ObjectManager.addObject(node);
        }
        Document.createElement = createElement;
        ;
        function setAttribute(node, attr, value) {
            let nodeObj = WebAssembly.ObjectManager.getObject(node);
            let name = WebAssembly.ObjectManager.readText(attr);
            let attrVal = WebAssembly.ObjectManager.readText(value);
            nodeObj.setAttribute(name, attrVal);
        }
        Document.setAttribute = setAttribute;
        function appendChild(parent, node) {
            let parentObj = WebAssembly.ObjectManager.getObject(parent);
            let child = WebAssembly.ObjectManager.getObject(node);
            parentObj.appendChild(child);
        }
        Document.appendChild = appendChild;
    })(Document = WebAssembly.Document || (WebAssembly.Document = {}));
})(WebAssembly || (WebAssembly = {}));
var WebAssembly;
(function (WebAssembly) {
    /**
     * A module contains string related api for simulate
     * ``Microsoft.VisualBasic.Strings`` module.
    */
    let Strings;
    (function (Strings) {
        function Mid(text, from, length) {
            let string = WebAssembly.ObjectManager.readText(text);
            let substr = string.substr(from - 1, length);
            return WebAssembly.ObjectManager.addText(substr);
        }
        Strings.Mid = Mid;
        function Len(text) {
            return WebAssembly.ObjectManager.readText(text).length;
        }
        Strings.Len = Len;
        function UCase(text) {
            let string = WebAssembly.ObjectManager.readText(text);
            if (string) {
                string = string.toUpperCase();
            }
            else {
                string = "";
            }
            return WebAssembly.ObjectManager.addText(string);
        }
        Strings.UCase = UCase;
        function LCase(text) {
            let string = WebAssembly.ObjectManager.readText(text);
            if (string) {
                string = string.toLowerCase();
            }
            else {
                string = "";
            }
            return WebAssembly.ObjectManager.addText(string);
        }
        Strings.LCase = LCase;
    })(Strings = WebAssembly.Strings || (WebAssembly.Strings = {}));
})(WebAssembly || (WebAssembly = {}));
var WebAssembly;
(function (WebAssembly) {
    let RegularExpression;
    (function (RegularExpression) {
        function regexp(pattern, flags) {
            let patternText = WebAssembly.ObjectManager.readText(pattern);
            let r = new RegExp(patternText, WebAssembly.ObjectManager.readText(flags));
            return WebAssembly.ObjectManager.addObject(r);
        }
        RegularExpression.regexp = regexp;
        function replace(text, pattern, replacement) {
            let input = WebAssembly.ObjectManager.readText(text);
            let patternObj = WebAssembly.ObjectManager.getObject(pattern);
            let replaceAs = WebAssembly.ObjectManager.readText(replacement);
            let result = input.replace(patternObj, replaceAs);
            return WebAssembly.ObjectManager.addObject(result);
        }
        RegularExpression.replace = replace;
        /**
         * Returns a Boolean value that indicates whether or not a pattern exists in a
         * searched string.
         *
         * @param string String on which to perform the search.
        */
        function test(pattern, string) {
            let patternObj = WebAssembly.ObjectManager.getObject(pattern);
            let text = WebAssembly.ObjectManager.readText(string);
            return patternObj.test(text) ? 1 : 0;
        }
        RegularExpression.test = test;
        /**
         * Executes a search on a string using a regular expression pattern, and returns an array
         * containing the results of that search.
         *
         * @param string The String object or string literal on which to perform the search.
        */
        function exec(pattern, string) {
            let patternObj = WebAssembly.ObjectManager.getObject(pattern);
            let text = WebAssembly.ObjectManager.readText(string);
            let match = patternObj.exec(text);
            return WebAssembly.ObjectManager.addObject(match);
        }
        RegularExpression.exec = exec;
    })(RegularExpression = WebAssembly.RegularExpression || (WebAssembly.RegularExpression = {}));
})(WebAssembly || (WebAssembly = {}));
var WebAssembly;
(function (WebAssembly) {
    /**
     * String api from javascript.
    */
    let JsString;
    (function (JsString) {
        function fromCharCode(n) {
            let s = String.fromCharCode(n);
            return WebAssembly.ObjectManager.addText(s);
        }
        JsString.fromCharCode = fromCharCode;
        function charCodeAt(text, index) {
            let input = WebAssembly.ObjectManager.readText(text);
            return input.charCodeAt(index);
        }
        JsString.charCodeAt = charCodeAt;
        function charAt(text, index) {
            let input = WebAssembly.ObjectManager.readText(text);
            return WebAssembly.ObjectManager.addText(input.charAt(index));
        }
        JsString.charAt = charAt;
        function join(text, deli) {
            let inptrs = WebAssembly.ObjectManager.getObject(text);
            let strs = inptrs.map(i => WebAssembly.ObjectManager.readText(i));
            let deliText = WebAssembly.ObjectManager.readText(deli);
            let output = strs.join(deliText);
            return WebAssembly.ObjectManager.addText(output);
        }
        JsString.join = join;
        function toString(obj) {
            let s;
            if (WebAssembly.ObjectManager.isNull(obj)) {
                // 没有目标，说明是一个数字
                s = obj.toString();
            }
            else {
                // 不是空的，说明是一个对象
                s = WebAssembly.ObjectManager.getObject(obj).toString();
            }
            return WebAssembly.ObjectManager.addText(s);
        }
        JsString.toString = toString;
        function add(a, b) {
            let str1 = WebAssembly.ObjectManager.readText(a);
            let str2 = WebAssembly.ObjectManager.readText(b);
            return WebAssembly.ObjectManager.addText(str1 + str2);
        }
        JsString.add = add;
        function length(text) {
            return WebAssembly.ObjectManager.readText(text).length;
        }
        JsString.length = length;
        function replace(text, find, replacement) {
            let input = WebAssembly.ObjectManager.readText(text);
            let findObj;
            if (WebAssembly.ObjectManager.getType(find) == "RegExp") {
                findObj = WebAssembly.ObjectManager.getObject(find);
            }
            else {
                findObj = WebAssembly.ObjectManager.readText(find);
            }
            let replaceStr = WebAssembly.ObjectManager.readText(replacement);
            let result = input.replace(findObj, replaceStr);
            return WebAssembly.ObjectManager.addText(result);
        }
        JsString.replace = replace;
        function indexOf(input, find) {
            let text = WebAssembly.ObjectManager.readText(input);
            let findText = WebAssembly.ObjectManager.readText(find);
            return text.indexOf(findText);
        }
        JsString.indexOf = indexOf;
    })(JsString = WebAssembly.JsString || (WebAssembly.JsString = {}));
})(WebAssembly || (WebAssembly = {}));
var vanilla;
(function (vanilla) {
    /**
     * The VisualBasic.NET application AssemblyInfo
    */
    class AssemblyInfo {
        constructor(AssemblyTitle, AssemblyDescription, AssemblyCompany, AssemblyProduct, AssemblyCopyright, AssemblyTrademark, Guid, AssemblyVersion, AssemblyFileVersion) {
            this.AssemblyTitle = AssemblyTitle;
            this.AssemblyDescription = AssemblyDescription;
            this.AssemblyCompany = AssemblyCompany;
            this.AssemblyProduct = AssemblyProduct;
            this.AssemblyCopyright = AssemblyCopyright;
            this.AssemblyTrademark = AssemblyTrademark;
            this.Guid = Guid;
            this.AssemblyVersion = AssemblyVersion;
            this.AssemblyFileVersion = AssemblyFileVersion;
        }
        toString() {
            return this.AssemblyTitle;
        }
        static readAssemblyInfo(assm) {
            let webassm = assm.instance.exports;
            return new AssemblyInfo(WebAssembly.ObjectManager.readText(webassm.AssemblyTitle()), WebAssembly.ObjectManager.readText(webassm.AssemblyDescription()), WebAssembly.ObjectManager.readText(webassm.AssemblyCompany()), WebAssembly.ObjectManager.readText(webassm.AssemblyProduct()), WebAssembly.ObjectManager.readText(webassm.AssemblyCopyright()), WebAssembly.ObjectManager.readText(webassm.AssemblyTrademark()), WebAssembly.ObjectManager.readText(webassm.Guid()), WebAssembly.ObjectManager.readText(webassm.AssemblyVersion()), WebAssembly.ObjectManager.readText(webassm.AssemblyFileVersion()));
        }
    }
    vanilla.AssemblyInfo = AssemblyInfo;
})(vanilla || (vanilla = {}));
var vanilla;
(function (vanilla) {
    var Wasm;
    (function (Wasm) {
        let FunctionApi;
        (function (FunctionApi) {
            /**
             * 主要是创建一个对参数的封装函数，因为WebAssembly之中只有4中基础的数值类型
             * 所以字符串，对象之类的都需要在这里进行封装之后才能够被传递进入WebAssembly
             * 运行时环境之中
            */
            function buildApiFunc(func) {
                let ObjMgr = WebAssembly.ObjectManager;
                let api = function () {
                    let intptr = func.apply(this, buildArguments(arguments));
                    let result;
                    if (ObjMgr.isText(intptr)) {
                        result = ObjMgr.readText(intptr);
                    }
                    else if (!ObjMgr.isNull(intptr)) {
                        result = ObjMgr.getObject(intptr);
                    }
                    else {
                        result = intptr;
                    }
                    if (Wasm.showDebugMessage()) {
                        console.log("Strings in WebAssembly memory:");
                        WebAssembly.ObjectManager.printTextCache();
                        console.log("Objects in WebAssembly memory:");
                        WebAssembly.ObjectManager.printObjectCache();
                    }
                    return result;
                };
                api.WasmPrototype = func;
                return api;
            }
            FunctionApi.buildApiFunc = buildApiFunc;
            function buildArguments(args) {
                let params = [];
                let value;
                for (var i = 0; i < args.length; i++) {
                    value = args[i];
                    if (!value || typeof value == "undefined") {
                        // zero intptr means nothing or value 0
                        value = 0;
                    }
                    else if (typeof value == "string") {
                        value = WebAssembly.ObjectManager.addText(value);
                    }
                    else if (typeof value == "object") {
                        value = WebAssembly.ObjectManager.addObject(value);
                    }
                    else if (typeof value == "boolean") {
                        value = value ? 1 : 0;
                    }
                    else {
                        // do nothing
                    }
                    params.push(value);
                }
                return params;
            }
        })(FunctionApi = Wasm.FunctionApi || (Wasm.FunctionApi = {}));
    })(Wasm = vanilla.Wasm || (vanilla.Wasm = {}));
})(vanilla || (vanilla = {}));
var vanilla;
(function (vanilla) {
    /**
     * The web assembly helper
    */
    let Wasm;
    (function (Wasm) {
        /**
         * The webassembly engine.
        */
        const engine = window.WebAssembly;
        /**
         * Run the compiled VisualBasic.NET assembly module
         *
         * > This function add javascript ``math`` module as imports object automatic
         *
         * @param module The ``*.wasm`` module file path
         * @param run A action delegate for utilize the VB.NET assembly module
         *
        */
        function RunAssembly(module, opts) {
            fetch(module)
                .then(function (response) {
                if (response.ok) {
                    return response.arrayBuffer();
                }
                else {
                    throw `Unable to fetch Web Assembly file ${module}.`;
                }
            })
                .then(buffer => new Uint8Array(buffer))
                .then(module => ExecuteInternal(module, opts))
                .then(assembly => {
                let exportAssm = exportWasmApi(assembly);
                if (showDebugMessage()) {
                    console.log("Load external WebAssembly module success!");
                    console.log(assembly);
                    console.log("VisualBasic.NET Project AssemblyInfo:");
                    console.log(exportAssm.AssemblyInfo);
                }
                opts.run(exportAssm);
            });
        }
        Wasm.RunAssembly = RunAssembly;
        function showDebugMessage(opt) {
            if (typeof opt != "boolean") {
                if (typeof TypeScript == "object") {
                    if (typeof TypeScript.logging == "object" && TypeScript.logging.outputEverything) {
                        return true;
                    }
                }
            }
            else {
                setConfigDebugger(opt);
            }
            return false;
        }
        Wasm.showDebugMessage = showDebugMessage;
        function setConfigDebugger(opt) {
            let host = window;
            if (typeof TypeScript != "object") {
                host.TypeScript = {};
            }
            if (typeof TypeScript.logging != "object") {
                host.TypeScript.logging = {};
            }
            host.TypeScript.logging.outputEverything = opt;
        }
        function exportWasmApi(assm) {
            let exports = assm.instance.exports;
            let api = {
                AssemblyInfo: vanilla.AssemblyInfo.readAssemblyInfo(assm)
            };
            for (let name in exports) {
                let obj = exports[name];
                if (typeof obj == "function") {
                    obj = Wasm.FunctionApi.buildApiFunc(obj);
                }
                else {
                    // do nothing
                }
                api[name] = obj;
            }
            return api;
        }
        function createBytes(opts) {
            let page = opts.page || { init: 10, max: 2048 };
            let config = { initial: page.init };
            return new window.WebAssembly.Memory(config);
        }
        function ExecuteInternal(module, opts) {
            var byteBuffer = createBytes(opts);
            var dependencies = {
                "global": {},
                "env": {
                    bytechunks: byteBuffer
                }
            };
            // read/write webassembly memory
            WebAssembly.ObjectManager.load(byteBuffer);
            // add javascript api dependencies imports
            handleApiDependencies(dependencies, opts);
            let assembly = engine.instantiate(module, dependencies);
            return assembly;
        }
        function getMath() {
            let runtime = window;
            let math = runtime.Math;
            math["isNaN"] = x => runtime.isNaN(x);
            math["isFinite"] = x => runtime.isFinite(x);
            return math;
        }
        function handleApiDependencies(dependencies, opts) {
            var api = opts.api || {
                document: false,
                console: true,
                http: false,
                text: true,
                array: true
            };
            // imports the javascript math module for VisualBasic.NET 
            // module by default
            dependencies["Math"] = getMath();
            // Andalso imports some basically string api for VisualBasic.NET
            // as well
            dependencies["string"] = WebAssembly.JsString;
            if (typeof opts.imports == "object") {
                for (var key in opts.imports) {
                    dependencies[key] = opts.imports[key];
                }
            }
            if (api.document) {
                dependencies["document"] = WebAssembly.Document;
            }
            if (api.console) {
                dependencies["console"] = WebAssembly.Console;
            }
            if (api.http) {
                dependencies["XMLHttpRequest"] = WebAssembly.XMLHttpRequest;
            }
            if (api.text) {
                dependencies["RegExp"] = WebAssembly.RegularExpression;
                dependencies["Strings"] = WebAssembly.Strings;
            }
            if (api.array) {
                dependencies["Array"] = WebAssembly.JsArray;
            }
            return dependencies;
        }
    })(Wasm = vanilla.Wasm || (vanilla.Wasm = {}));
})(vanilla || (vanilla = {}));
var vanilla;
(function (vanilla) {
    class memoryReader {
        constructor(bytechunks) {
            this.buffer = bytechunks.buffer;
        }
        sizeOf(intPtr) {
            let buffer = new Uint8Array(this.buffer, intPtr);
            let size = buffer.findIndex(b => b == 0);
            return size;
        }
    }
    vanilla.memoryReader = memoryReader;
    /**
     * Read string helper from WebAssembly memory.
    */
    class stringReader extends memoryReader {
        /**
         * @param memory The memory buffer
        */
        constructor(memory) {
            super(memory);
            this.decoder = new TextDecoder();
        }
        /**
         * Read text from WebAssembly memory buffer.
        */
        readTextRaw(offset, length) {
            let str = new Uint8Array(this.buffer, offset, length);
            let text = this.decoder.decode(str);
            return text;
        }
        readText(intPtr) {
            return this.readTextRaw(intPtr, this.sizeOf(intPtr));
        }
    }
    vanilla.stringReader = stringReader;
})(vanilla || (vanilla = {}));
/// <reference path="memoryReader.ts" />
var vanilla;
(function (vanilla) {
    class arrayReader extends vanilla.memoryReader {
        /**
         * @param memory The memory buffer
        */
        constructor(memory) {
            super(memory);
        }
        array(intPtr, type) {
            // 数组的起始前4个字节是数组长度
            let length = this.toInt32(intPtr);
            let uint8s = new Uint8Array(this.buffer, intPtr + 4);
            let buffer = new DataView(uint8s);
            // The output data buffer
            let data = [];
            let load = arrayReader.getReader(buffer, type);
            let offset = arrayReader.sizeOf(type);
            intPtr = 0;
            for (var i = 0; i < length; i++) {
                data.push(load(intPtr));
                intPtr = intPtr + offset;
            }
            return data;
        }
        static sizeOf(type) {
            if (type == "i32" || type == "f32") {
                return 4;
            }
            else if (type == "i64" || type == "f64") {
                return 8;
            }
            else {
                throw `Unavailable for ${type}`;
            }
        }
        static getReader(buffer, type) {
            if (type == "i32") {
                return function (offset) {
                    return buffer.getInt32(offset);
                };
            }
            else if (type == "i64") {
                throw "not implements";
            }
            else if (type == "f32") {
                return function (offset) {
                    return buffer.getFloat32(offset);
                };
            }
            else if (type == "f64") {
                return function (offset) {
                    return buffer.getFloat64(offset);
                };
            }
            else {
                throw `Unavailable for ${type}`;
            }
        }
        toInt32(intPtr) {
            let uint8s = new Uint8Array(this.buffer, intPtr, 4);
            let view = new DataView(uint8s);
            return view.getInt32(0);
        }
    }
    vanilla.arrayReader = arrayReader;
})(vanilla || (vanilla = {}));
//# sourceMappingURL=visualbasic.wasm.js.map