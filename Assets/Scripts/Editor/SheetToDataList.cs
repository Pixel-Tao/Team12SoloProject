using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class SheetToDataList
{
    private const string _soDirPath = "Assets/Resources/SO";
    private const string _sheetDirName = "Sheets";
    private const string _dataDirName = "DataList";
    
    [MenuItem("Assets/Create/Convert to DataList", false)]
    static void Convert()
    {
        var selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
        string folderPath = AssetDatabase.GetAssetPath(selectedAssets[0]);
        if (AssetDatabase.IsValidFolder(folderPath) == false) return;

        string dataDirPath = $"{_soDirPath}/{_dataDirName}";
        if (AssetDatabase.IsValidFolder(dataDirPath) == false)
            AssetDatabase.CreateFolder(_soDirPath, _dataDirName);
        
        string[] guids = AssetDatabase.FindAssets("", new[] { folderPath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            var sheet = AssetDatabase.LoadAssetAtPath<Sheet>(path);
            var sheetType = sheet.GetType();
            string sheetName = sheetType.Name;
            string dataListName = sheetName.Replace("Sheets", "DataList");
            var dataListType = Type.GetType($"{dataListName}, Assembly-CSharp");
            var dataListObj = dataListType.GetMethod("Convert", BindingFlags.Static | BindingFlags.Public)
                .Invoke(null, new object[] { sheet }) as ScriptableObject;
            string dataListPath = dataDirPath + $"/{dataListName}.asset";

            AssetDatabase.CreateAsset(dataListObj, dataListPath);
            AssetDatabase.SaveAssets();
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/Create/Convert to DataList", true)]
    static bool CanConvert()
    {
        var selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
        if (selectedAssets.Length != 1) return false;
        var path = AssetDatabase.GetAssetPath(selectedAssets[0]);
        return path.EndsWith($"{_soDirPath}/{_sheetDirName}");
    }
}