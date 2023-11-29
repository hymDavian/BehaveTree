type FuncDescription = {
    name: string,
    paramsList: string[]
}
type TreeEditFuncLibType = FuncDescription[];

//获取类的所有指定的成员函数
function getClsFuncs<T extends { new(...args: any[]): {} }>(clsObj: T, funcFlag: string = "_out"): TreeEditFuncLibType {
    const funcs = new Set<string>();

    let proto = clsObj.prototype;
    const selfKeys = Reflect.ownKeys(proto);

    let base = Object.getPrototypeOf(proto)
    while (base) {
        const baseKeys = Reflect.ownKeys(base);
        selfKeys.concat(baseKeys).filter(key => {
            if (typeof key === "string" && key.endsWith(funcFlag)) {
                let isfunc = false;
                try {
                    isfunc = typeof proto[key] === "function";

                } catch (error) {
                    isfunc = true;
                }
                return isfunc;
            }
            return false;
        }).forEach(key => {
            funcs.add(key as string);
        })
        base = Object.getPrototypeOf(base);
    }



    const result: TreeEditFuncLibType = [];
    funcs.forEach(funcName => {
        const isGet = !!Object.getOwnPropertyDescriptor(clsObj.prototype, funcName)?.get;
        const isSet = !!Object.getOwnPropertyDescriptor(clsObj.prototype, funcName)?.set;

        if (isGet || isSet) {
            // console.error(`${clsObj.name}.${funcName.toString()} isget:${isGet} | isset:${isSet}`);
            return;
        }
        const func = clsObj.prototype[funcName];
        if (typeof func === "function") {
            let paramsArr = getParameterName(clsObj.prototype[funcName]);
            result.push({
                name: funcName,
                paramsList: paramsArr
            })
        }
    })
    return result;
}
// 获取函数的参数名
function getParameterName(fn) {
    if (typeof fn !== 'object' && typeof fn !== 'function') return;
    const COMMENTS = /((\/\/.*$)|(\/\*[\s\S]*?\*\/))/mg;
    const DEFAULT_PARAMS = /=[^,)]+/mg;
    const FAT_ARROWS = /=>.*$/mg;
    let code = fn.prototype ? fn.prototype.constructor.toString() : fn.toString();
    code = code
        .replace(COMMENTS, '')
        .replace(FAT_ARROWS, '')
        .replace(DEFAULT_PARAMS, '');
    let result = code.slice(code.indexOf('(') + 1, code.indexOf(')')).match(/([^\s,]+)/g);
    return result === null ? [] : result;
}


// class Base1 {

//     public a: string;
//     private b: boolean;
//     protected c: number;

//     get t1() {
//         return 1
//     }
//     set t1(v) {

//     }

//     get t2() {
//         return 2;
//     }

//     test_out() {

//     }
// }

// class Base2 extends Base1 {
//     ffff_out(ff: number) {

//     }
// }

// class AAA extends Base2 {

//     public aaa: string;
//     private bbb: boolean;
//     protected ccc: number;

//     ddd: any;

//     playAnimation_out(guid: string, loopNum: number, speed: number) {

//     }

//     private testff_out(nn: number) {

//     }
// }

// let test = getClsFuncs(AAA);
// console.log(JSON.stringify(test));


enum nodetype {
    ActionNodeCtor,
    ConditionNodeCtor,
    SelectorNodeCtor,
    SequenceNodeCtor,
}
type EditTreeNodeInfo = {
    pos: { x: number, y: number, z: number },
    nodetype: nodetype,
    id: string,
    children: string[],
    //todo 动作节点的函数使用信息
    //todo 条件节点的条件设置信息
}
type SaveNodes = {
    nodes: EditTreeNodeInfo[],
    useCls: string,//动作节点的使用类函数库

}