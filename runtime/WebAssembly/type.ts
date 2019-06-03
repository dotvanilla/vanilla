
interface classMeta {
    namespace: string;
    class: string;
    class_id: number;
    isStruct: boolean;
    fields: object;

    /**
     * 在完成加载之后计算之后在js环境之中写进来的
    */
    allocateSize: number;
}

interface type {
    type: typeAlias;
    generic?: type[];
    raw: string;
}

/**
 * The compiler type alias
*/
enum typeAlias {
    /**
     * Function or expression have no value returns
    */
    void = -1,
    any,
    i32,
    i64,
    f32,
    f64,
    string,
    boolean,

    /**
     * Fix length array in WebAssembly runtime
    */
    array,

    /**
     * Array list in javascript runtime
    */
    list,

    /** 
     * Javascript object
    */
    table,

    /**
     * 所有用户自定义的引用类型
    */
    intptr
}