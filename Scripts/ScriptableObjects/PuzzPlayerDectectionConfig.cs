using UnityEngine;
using PUZ.Behaviour;

[CreateAssetMenu(fileName = "PuzzPlayerDetectionConfig", menuName = "ScriptableObjects/PuzzPlayerDetectionConfigScriptableObject", order = 1)]
public class PuzzPlayerDetectionConfig : ScriptableObject
{
    public float FOV;
    public float CloserDistance;
    public float DistantDistance;
}