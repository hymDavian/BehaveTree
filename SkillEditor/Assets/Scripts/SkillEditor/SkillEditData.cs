using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillEditor
{
    internal static class SkillEditData
    {
        private static Dictionary<string, List<FuncDescription>> _tsClassFuncs = new Dictionary<string, List<FuncDescription>>();
        /// <summary>
        /// 添加一个类的函数列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="list"></param>
        public static void AddClassFuncs(string name, List<FuncDescription> list)
        {
            if (list != null && list.Count > 0)
            {

                _tsClassFuncs[name] = list;
                onClassListRefresh.Invoke(_tsClassFuncs);
            }
        }
        /// <summary>
        /// 尝试获取一个类的函数列表
        /// </summary>
        public static bool TryGetTSClassFuncs(string name, out List<FuncDescription> tsClassFuncs)
        {
            return _tsClassFuncs.TryGetValue(name, out tsClassFuncs);
        }
        /// <summary>
        /// 获取当前类
        /// </summary>
        /// <returns></returns>
        public static List<FuncDescription> GetCurrentClsFuncs()
        {
            if (TryGetTSClassFuncs(currentClass, out var tsClassFuncs))
            {
                return tsClassFuncs;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// ts类列表变更事件
        /// </summary>
        public static event Action<Dictionary<string, List<FuncDescription>>> onClassListRefresh;

        private static string _curCls = "";
        /// <summary>
        /// 当前树的使用函数类
        /// </summary>
        public static string currentClass
        {
            get { return _curCls; }
            set
            {
                if (value != _curCls)
                {
                    _curCls = value;
                    if (onCurrentTSClsChange != null)
                    {
                        onCurrentTSClsChange();

                    }
                }
            }
        }
        /// <summary>
        /// 当前适用类变更
        /// </summary>
        public static event Action onCurrentTSClsChange;
        /// <summary>
        /// 树的  所有节点
        /// </summary>
        public static readonly List<INodeTree> allNodes = new List<INodeTree>();

        public static int nodeidSeed = 0;

        private static readonly Dictionary<string, string> _bkDataset = new Dictionary<string, string>();
        public static void SetBlackboardValues(string[] keys, string[] values)
        {
            _bkDataset.Clear();
            for (int i = 0; i < keys.Length; i++)
            {
                _bkDataset[keys[i]] = values[i];
            }
            if(onBlackboardDataRefresh!= null)
            {
                onBlackboardDataRefresh.Invoke(_bkDataset.ToArray());
            }
            if (onBlackboardKeysChange != null)
            {
                onBlackboardKeysChange.Invoke(_bkDataset.Keys.ToArray());
            }
        }
        public static void SetBlackboardValue(string key,string value)
        {
            if (_bkDataset.ContainsKey(key))
            {
                _bkDataset[key] = value;
            }
            else
            {
                _bkDataset[key] = value;
                if (onBlackboardKeysChange != null)
                {
                    onBlackboardKeysChange.Invoke(_bkDataset.Keys.ToArray());
                }
            }
        }
        public static void RemoveBlackboard(string key)
        {
            if (_bkDataset.ContainsKey(key))
            {
                _bkDataset.Remove(key);
                if (onBlackboardKeysChange != null)
                {
                    onBlackboardKeysChange.Invoke(_bkDataset.Keys.ToArray());
                }
            }
        }

        public static KeyValuePair<string[], string[]> bkDatas
        {
            get
            {
                string[] keys = _bkDataset.Keys.ToArray();
                string[] values = _bkDataset.Values.ToArray();
                return new KeyValuePair<string[], string[]>(keys, values);
            }
        }

        /// <summary>
        /// 当黑板变量被整体设置时
        /// </summary>
        public static event Action<KeyValuePair<string, string>[]> onBlackboardDataRefresh;
        /// <summary>
        /// 当黑板键变更
        /// </summary>
        public static event Action<string[]> onBlackboardKeysChange;

    }
    /// <summary>
    /// 函数描述信息
    /// </summary>
    internal class FuncDescription
    {
        public string name { get; set; }
        public List<string> paramsList { get; set; }
    }

    enum ENodeType
    {
        ActionNodeCtor,
        ConditionNodeCtor,
        SelectorNodeCtor,
        SequenceNodeCtor
    }
    struct NodePosition
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }
    internal class EditTreeNodeInfo
    {
        public NodePosition pos { get; set; }
        public ENodeType nodetype { get; set; }
        public string id { get; set; }
        public string[] children { get; set; }
    }
    internal class  SaveNodes
    {
        public EditTreeNodeInfo[] nodes { get; set; }
        public string useCls { get; set; }

        public string[] bkKeys { get; set; }
        public string[] bkValues { get; set; }
    }
}
