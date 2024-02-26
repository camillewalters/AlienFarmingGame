using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop Data", menuName = "New Crop Data")]
public class CropData : ScriptableObject
{
    public string cropName;
    public int daysToGrow;
    public Sprite[] growProgressSprites;
    public Sprite readyToHarvestSprite;

    public int purchasePrice;
    public int sellPrice;
    public int daysToHarvest = 3;

}