namespace vanilla.Wasm.Application {

    /**
     * Create the VisualBasic.NET application module
    */
    export function BuildAppModules(wasm: object): any {
        let app: {} = {};
        let obj: any;
        let ref: { module: string, name: string };

        for (let name in wasm) {
            obj = wasm[name];
            ref = getTagValue(name);

            if (typeof obj == "function") {
                obj = Application.FunctionApi.buildApiFunc(obj);
            } else {
                // do nothing
            }

            if (ref.module == "global" && ref.name == "GetMemorySize") {
                app["GetMemorySize"] = obj;
            } else {
                if (!(ref.module in app)) {
                    app[ref.module] = {};
                }

                app[ref.module][ref.name] = obj;
            }
        }

        return app;
    }

    /**
     * 在VB.NET之中，对象之间是通过小数点来分割引用单词的
    */
    const tag: string = ".";

    function getTagValue(str: string) {
        var i: number = str.indexOf(tag);
        var tagLen: number = tag.length;

        if (i > -1) {
            var name: string = str.substr(0, i);
            var value: string = str.substr(i + tagLen);

            return { module: name, name: value };
        } else {
            return { module: "", name: str };
        }
    }
}