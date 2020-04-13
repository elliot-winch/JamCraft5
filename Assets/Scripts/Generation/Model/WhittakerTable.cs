using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Whittaker", menuName = "ScriptableObjects/Whittaker Table")]
public class WhittakerTable : ScriptableObject
{
    [Serializable]
    public struct Row
    {
        [SerializeField]
        public List<Biome> Data;
    }

    [SerializeField]
    public List<Row> Rows;
}
