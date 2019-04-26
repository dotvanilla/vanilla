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

            return new AssemblyInfo(
                WebAssembly.ObjectManager.readText(webassm.AssemblyTitle()),
                WebAssembly.ObjectManager.readText(webassm.AssemblyDescription()),
                WebAssembly.ObjectManager.readText(webassm.AssemblyCompany()),
                WebAssembly.ObjectManager.readText(webassm.AssemblyProduct()),
                WebAssembly.ObjectManager.readText(webassm.AssemblyCopyright()),
                WebAssembly.ObjectManager.readText(webassm.AssemblyTrademark()),
                WebAssembly.ObjectManager.readText(webassm.Guid()),
                WebAssembly.ObjectManager.readText(webassm.AssemblyVersion()),
                WebAssembly.ObjectManager.readText(webassm.AssemblyFileVersion())
            );
        }
    }
}