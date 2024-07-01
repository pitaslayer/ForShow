using UnityEngine;
using PUZ.Behaviour;

[CreateAssetMenu(fileName = "PuzzAIConfig", menuName = "ScriptableObjects/PuzzAIConfigScriptableObject", order = 1)]
public class PuzzAIConfig : ScriptableObject
{
    public float Acceleration;
    public float Speed;
    public float AngularSpeed;
    public float Radius;
    public float Height;
}