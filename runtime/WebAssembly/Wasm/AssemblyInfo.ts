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
    }
}