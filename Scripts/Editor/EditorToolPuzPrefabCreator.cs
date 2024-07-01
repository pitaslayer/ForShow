using System.IO;
using UnityEditor;
using UnityEngine;
using PUZ.Behaviour;
using System;
using System.Linq;
using UnityEngine.AI;
using PUZ.Timer;

#if UNITY_EDITOR
public static class EditorToolPuzPrefabCreator
{
    private const string SOPath = "Assets/Tiago/Data/Stats/";
    private const string CsvPath = "Assets/Tiago/Data/CSV/";
    private const string PrefabPath = "Assets/Tiago/Prefabs/Puz/";

    [MenuItem("ScriptableObjects/CreatePuzzPrefab")]
    private static void CreatePuzzPrefab()
    {
        var info = new DirectoryInfo(SOPath);
        var fileInfo = info.GetFiles().Where(file => file.Extension !=".meta");

        foreach(FileInfo f in fileInfo)
        {
            Debug.Log(f.Name);
            PuzzStats stats = AssetDatabase.LoadAssetAtPath<PuzzStats>($"Assets/Tiago/Data/Stats/{f.Name}");
            PuzType type = stats.PuzType;
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<PlayerDetector>();
            gameObject.AddComponent<FleeTimer>();
            gameObject.AddComponent<NavMeshAgent>();
            gameObject.AddComponent((PUZObject)SelectPuzScript(type));
            gameObject.GetComponent(type(SelectPuzScript(type))).SetStatsFile(stats);
            //gameObject.GetComponent(typeof(SelectPuzScript(type))).SetStatsFile(stats);

            bool prefabSuccess;
            PrefabUtility.SaveAsPrefabAsset(gameObject,  $"Assets/Tiago/Prefabs/Puz/{SelectPuzName(type)}.prefab", out prefabSuccess);
            if (prefabSuccess == true)
                Debug.Log("Prefab was saved successfully");
            else
                Debug.Log("Prefab failed to save" + prefabSuccess);
        } 
    }

    [MenuItem("ScriptableObjects/CreatePuzzStats")]
    private static void CreatePuzzPStats()
    {

        var info = new DirectoryInfo(CsvPath);
        var fileInfo = info.GetFiles().Where(file => file.Extension !=".meta");

        foreach(FileInfo f in fileInfo)
        {
            string[] allLines = File.ReadAllLines(CsvPath + f.Name);
            foreach (string s in allLines)
            {
                string[] splitData = s.Split(',');
                CreatePuzzStatsScriptableObject(SelectPuzStatsScript((PuzType)int.Parse(splitData[0])), splitData);
            }
        }
    } 

    public static bool AssetActuallyExists(string assetPath)
    {
        return !(AssetDatabase.AssetPathToGUID(assetPath) == string.Empty ||
            AssetDatabase.GetMainAssetTypeAtPath(assetPath) != null);
    }
    
    static PUZ.Behaviour.PUZObject SelectPuzScript(PuzType type)
    {
        switch(type)
        {
            case PuzType.Yellow : return Type.GetType("PUZ_Yellow, PUZ.Behaviour.PUZObject");
            case PuzType.Green : return Type.GetType("PUZ_Green, PUZ.Behaviour.PUZObject");
            case PuzType.Purple : return Type.GetType("PUZ_Purple, PUZ.Behaviour.PUZObject");
            case PuzType.Blue : return Type.GetType("PUZ_Blue, PUZ.Behaviour.PUZObject");
            default: return Type.GetType("PUZ_Green, PUZ.Behaviour.PUZObject");
        }
    }

     static string SelectPuzName(PuzType type)
    {
        switch(type)
        {
            case PuzType.Yellow : return "PUZ_Yellow";
            case PuzType.Green : return "PUZ_Green";
            case PuzType.Purple : return "PUZ_Purple";
            case PuzType.Blue : return "PUZ_Blue";
            default: return "PUZ_Green";
        }
    }

    static string SelectPuzStatsScript(PuzType type)
    {
        switch(type)
        {
            case PuzType.Yellow : return "PUZStats_Yellow";
            case PuzType.Green : return "PUZStats_Green";
            case PuzType.Purple : return "PUZStats_Purple";
            case PuzType.Blue : return "PUZStats_Blue";
            default: return "PUZStats_Green";
        }
    }

    static void CreatePuzzStatsScriptableObject(string assetName, string[] data)
    {
        string[] result = AssetDatabase.FindAssets(assetName, new[] {SOPath});

        PuzzStats statsObject= null;

        if (result.Length > 1)
        {
            Debug.LogError("More than 1 Asset founded");
            return;
        }

        if(result.Length == 0 || !AssetActuallyExists(assetName))
        {
            Debug.Log("Create new Asset");
            statsObject = ScriptableObject.CreateInstance<PuzzStats>();
            AssetDatabase.CreateAsset(statsObject, $"Assets/Tiago/Data/Stats/{assetName}.asset");
        }
        else
        {
            string path = AssetDatabase.GUIDToAssetPath(result[0]);
            Debug.Log(path);
            statsObject= (PuzzStats )AssetDatabase.LoadAssetAtPath(path, typeof(PuzzStats ));
            Debug.Log("Found Asset File !!!");
        }

        statsObject.PuzType = (PuzType)int.Parse(data[0]);
        statsObject.Health = float.Parse(data[1]);
        statsObject.WalkingSpeed = float.Parse(data[2]);
        statsObject.RunningSpeed = float.Parse(data[3]);
        statsObject.Stamina = float.Parse(data[4]);

        statsObject.Visibility = float.Parse(data[5]);
        statsObject.FearLevel = float.Parse(data[6]);
        statsObject.Hunger = float.Parse(data[7]);

        EditorUtility.SetDirty(statsObject);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
#endif