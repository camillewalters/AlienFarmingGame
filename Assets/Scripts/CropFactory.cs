using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CropFactory : MonoBehaviour, ICropFactory
{
    public static CropFactory Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public GameObject CreateCrop(CropData cropData)
    {
        switch (cropData.cropName)
        {
            case "Wheat":
                return Instantiate(Resources.Load("WheatPrefab")) as GameObject;

            case "Turnip":
                return Instantiate(Resources.Load("TurnipPrefab")) as GameObject;

            // Add more cases as needed

            default:
                Debug.LogError("Unknown CropType");
                return null;
        }
    }
}
