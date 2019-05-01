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

        public static readAssemblyInfo(assm: IWasm): AssemblyInfo {
            let webassm: any = assm.instance.exports;
            let readText = function (name: string) {
                let ref: string = `AssemblyInfo.${name}`;
                let out = webassm[ref];

                return WebAssembly.ObjectManager.readText(out());
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