using UnityEngine;

[CreateAssetMenu(fileName = "PuzzManagerConfigFile", menuName = "ScriptableObjects/PuzzManagerConfigFileScriptableObject", order = 1)]
public class PuzzManagerConfig : ScriptableObject
{
    public GameObject BabyPuzPrefab;
    public float MinRebirthTime;
    public float MaxRebirthTime;
}