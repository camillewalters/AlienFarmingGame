using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    public Dictionary<CropData, GameObject> cropPrefabDictionary = new Dictionary<CropData, GameObject>();

    // Assign CropData and prefab pairs in the Inspector
    public CropData crop1;
    public GameObject crop1Prefab;

    public CropData crop2;
    public GameObject crop2Prefab;

    void Awake()
    {
        // Populate the dictionary
        cropPrefabDictionary.Add(crop1, crop1Prefab);
        cropPrefabDictionary.Add(crop2, crop2Prefab);
        // Add more entries as needed
    }
}