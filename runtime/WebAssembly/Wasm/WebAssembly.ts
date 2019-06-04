namespace vanilla {

    /**
     * The web assembly helper
    */
    export module Wasm {

        /**
         * The webassembly engine.
        */
        const engine: WebAssembly = (<any>window).WebAssembly;

        export function typeOf(class_id: number): type {
            let alias: typeAlias = class_id < 10 ? class_id : typeAlias.intptr;

            return <type>{
                type: alias,
                raw: class_id.toString()
            }
        }

        /** 
         * Run the compiled VisualBasic.NET assembly module
         * 
         * > This function add javascript ``math`` module as imports object automatic
         * 
         * @param module The ``*.wasm`` module file path
         * @param run A action delegate for utilize the VB.NET assembly module
         *         
        */
        export function RunAssembly(module: string, opts: Config): void {
            fetch(module)
                .then(function (response) {
                    if (response.ok) {
                        return response.arrayBuffer();
                    } else {
                        throw `Unable to fetch Web Assembly file ${module}.`;
                    }
                })
                .then(buffer => new Uint8Array(buffer))
                .then(module => ExecuteInternal(module, opts))
                .then(assembly => {
                    let exportAssm: AssemblyExport = exportWasmApi(assembly);

                    if (showDebugMessage()) {
                        console.log("Load external WebAssembly module success!");
                        console.log(assembly);
                        console.log("VisualBasic.NET Project AssemblyInfo:");
                        console.log(exportAssm.AssemblyInfo);
                    }

                    opts.run(exportAssm);
                });
        }

        export function showDebugMessage(opt?: boolean): boolean {
            if (typeof opt != "boolean") {
                if (typeof TypeScript == "object") {
                    if (typeof TypeScript.logging == "object" && TypeScript.logging.outputEverything) {
                        return true;
                    }
                }
            } else {
                setConfigDebugger(opt);
            }

            return false;
        }

        function setConfigDebugger(opt: boolean): void {
            let host: {
                TypeScript: {
                    logging?: {
                        outputEverything?: boolean
                    }
                }
            } = (<any>window);

            if (typeof TypeScript != "object") {
                host.TypeScript = {};
            }
            if (typeof TypeScript.logging != "object") {
                host.TypeScript.logging = {};
            }

            host.TypeScript.logging.outputEverything = opt;
        }

        function exportWasmApi(assm: IWasm): AssemblyExport {
            let assmInfo: AssemblyInfo = AssemblyInfo.readAssemblyInfo(assm);
            let exports: AssemblyExport = Application.BuildAppModules(assm.instance.exports);

            exports.AssemblyInfo = assmInfo;
            exports.memory = WebAssembly.ObjectManager.getLoadedMemory();

            return exports;
        }

        function createBytes(opts: Config): WasmMemory {
            let page = opts.page || { init: 10, max: 2048 };
            let config = { initial: page.init };

            return new (<any>window).WebAssembly.Memory(config);
        }

        function ExecuteInternal(module: Uint8Array, opts: Config): IWasm {
            var byteBuffer: WasmMemory = createBytes(opts);
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

        function getMath(): any {
            let runtime = (<any>window);
            let math = runtime.Math;

            math["isNaN"] = x => runtime.isNaN(x);
            math["isFinite"] = x => runtime.isFinite(x);

            return math;
        }

        function handleApiDependencies(dependencies: object, opts: Config) {
            var api: apiOptions = opts.api || {
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
            dependencies["GC"] = WebAssembly.GarbageCollection;

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
    }
}