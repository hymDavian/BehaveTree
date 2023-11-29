using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace SkillEditor.Json
{
    internal class JsonHelper
    {
        public JsonHelper() { }



        /// <summary>
        /// 将当前节点信息写入到文件
        /// </summary>
        /// <param name="path"></param>
        public static void WriteTreeInfoToFile(string fileName)
        {
            SaveNodes save = new SaveNodes();
            save.useCls = SkillEditData.currentClass;
            save.nodes = new EditTreeNodeInfo[SkillEditData.allNodes.Count];
            var bkdatas = SkillEditData.bkDatas;
            save.bkKeys = bkdatas.Key;
            save.bkValues = bkdatas.Value;
            for (int i = 0; i < SkillEditData.allNodes.Count; i++)
            {
                var nodector = SkillEditData.allNodes[i];
                var savenode = save.nodes[i] = new EditTreeNodeInfo();
                savenode.pos = new NodePosition() { x = nodector.position.x, y = nodector.position.y, z = nodector.position.z };
                savenode.nodetype = nodector.nodetype;
                savenode.id = nodector.id;
                savenode.children = new string[nodector.children.Count];
                for (int j = 0; j < nodector.children.Count; j++)
                {
                    savenode.children[j] = nodector.children[j].id;
                }
            }
            string json = JsonSerializer.Serialize<SaveNodes>(save);
            string path = Application.streamingAssetsPath + "/" + fileName + ".json";
            Directory.CreateDirectory(Application.streamingAssetsPath);
            File.WriteAllText(path, json);
        }
        /// <summary>
        /// 读取节点树文件
        /// </summary>
        /// <param name="jsonPath"></param>
        /// <returns></returns>
        public static SaveNodes ReadTreeInfo(string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                Debug.LogError("不存在路径" + jsonPath);
                return null;
            }
            string jsonStr = File.ReadAllText(jsonPath);
            SaveNodes ret = null;
            try
            {
                ret = JsonSerializer.Deserialize<SaveNodes>(jsonStr);
            }
            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
                return null;
            }
            if (ret == null)
            {
                Debug.LogError("解析json返回null!");
                return null;
            }
            return ret;
        }

        /// <summary>
        /// 获取目标类型的所有导出成员函数
        /// </summary>
        /// <param name="key">TS类名</param>
        /// <param name="jsonPath">json路径</param>
        /// <returns></returns>
        public static List<FuncDescription> GetTSClsFunctions(string key, string jsonPath = null)
        {
            if (SkillEditData.TryGetTSClassFuncs(key, out List<FuncDescription> list))
            {
                return list;
            }
            if (!File.Exists(jsonPath))
            {
                Debug.LogError("不存在路径" + jsonPath);
                return null;
            }
            string jsonStr = File.ReadAllText(jsonPath);
            List<FuncDescription> ret = null;
            try
            {
                ret = JsonSerializer.Deserialize<List<FuncDescription>>(jsonStr);
            }
            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
                return null;
            }
            if (ret == null)
            {
                Debug.LogError("解析json返回null!");
                return null;
            }
            SkillEditData.AddClassFuncs(key, ret);

            return ret;
        }

        public static string openFileDialog(out string fileName)
        {
            FileOpenDialog dialog = new FileOpenDialog();
            dialog.structSize = Marshal.SizeOf(dialog);
            dialog.filter = "json files\0*.json\0All Files\0*.*\0\0";
            dialog.file = new string(new char[256]);
            dialog.maxFile = dialog.file.Length;
            dialog.fileTitle = new string(new char[64]);
            dialog.maxFileTitle = dialog.fileTitle.Length;
            dialog.initialDir = UnityEngine.Application.dataPath;  //默认路径
            dialog.title = "Open File Dialog";
            dialog.defExt = "json";//显示文件的类型
            dialog.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;  //OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
            if (DialogShow.GetOpenFileName(dialog))
            {
                fileName = dialog.fileTitle.Split('.')[0];
                return dialog.file;
            }
            fileName = null;
            return null;
        }
    }



}
