namespace vanilla {

    /**
     * The VisualBasic.NET application AssemblyInfo
    */
    export class AssemblyInfo {

        public constructor(
            public AssemblyTitle: string,
            public AssemblyDescription: string,
            public AssemblyCompany: string,
            public AssemblyProduct: string,
            public AssemblyCopyright: string,
            public AssemblyTrademark: string,
            public Guid: string,
            public AssemblyVersion: string,
            public AssemblyFileVersion: string
        ) {
        }

        public toString(): string {
            return this.AssemblyTitle;
        }

        /**
         * Read ``AssemblyInfo.vb`` of target vbproj
        */
        public static readAssemblyInfo(assm: IWasm): AssemblyInfo {
            let webassm: any = assm.instance.exports;
            let readText = function (name: string) {
                let ref: string = `AssemblyInfo.${name}`;
                let out: Delegate.Func<number> = webassm[ref];

                // 20190607 如果是单文件编译，而非整个vbproj项目的编译
                // 则AssemblyInfo是缺失的
                // 则这个时候全部返回空字符串就好了
                if (isNullOrUndefined(out)) {
                    return "";
                } else {
                    return WebAssembly.ObjectManager.readText(out());
                }
            }

            return new AssemblyInfo(
                readText("AssemblyTitle"),
                readText("AssemblyDescription"),
                readText("AssemblyCompany"),
                readText("AssemblyProduct"),
                readText("AssemblyCopyright"),
                readText("AssemblyTrademark"),
                readText("Guid"),
                readText("AssemblyVersion"),
                readText("AssemblyFileVersion")
            );
        }
    }
}