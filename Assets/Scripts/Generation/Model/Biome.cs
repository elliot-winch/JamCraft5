using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "ScriptableObjects/Biome")]
public class Biome : ScriptableObject
{
    public string Name;
    [SerializeField]
    public LayeredPerlinNoiseParameters ViewGenerationParameters;
    [SerializeField]
    public Material Material;
}
