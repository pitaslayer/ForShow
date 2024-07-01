using UnityEngine;
using PUZ.Behaviour;

[CreateAssetMenu(fileName = "PuzzStats", menuName = "ScriptableObjects/PuzzStatsScriptableObject", order = 1)]
public class PuzzStats : ScriptableObject
{
    public PuzType PuzType;
    public float Health;
    public float WalkingSpeed;
    public float RunningSpeed;
    public float Stamina;

    public float Visibility;
    public float FearLevel;
    public float Hunger;

    public GameObject puzzObject;
}