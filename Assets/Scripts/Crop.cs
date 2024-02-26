using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Crop : MonoBehaviour
{
    private CropData curCropData;
    private int m_DayPlanted;
    private int m_DaysSinceLastWatered;

    public SpriteRenderer SpriteRenderer;

    public static event UnityAction<CropData> onPlantCrop;
    public static event UnityAction<CropData> onHarvestCrop;

    public void Plant(CropData crop)
    {
        curCropData = crop;
        m_DayPlanted = GameManager.Instance.CurrentDay;
        m_DaysSinceLastWatered = 0;
        UpdateSprite();
        onPlantCrop?.Invoke(crop);
    }

    /// <summary>
    /// Number of days the crop has been planted. 
    /// </summary>
    /// <returns></returns>
    int CropProgress()
    {
        return GameManager.Instance.CurrentDay - m_DayPlanted;
    }

    /// <summary>
    /// Whether the crop can be harvested
    /// </summary>
    /// <returns></returns>
    public bool CanHarvest()
    {
        return CropProgress() > curCropData.daysToGrow;
    }

    /// <summary>
    /// Water the crop
    /// </summary>
    public void Water()
    {
        m_DaysSinceLastWatered = 0;
    }

    /// <summary>
    /// When a harvest is attempted
    /// </summary>
    public void Harvest()
    {
        if (CanHarvest())
        {
            onHarvestCrop?.Invoke(curCropData);
            Destroy(curCropData);
        }
    }

    void UpdateSprite()
    {
        int progress = CropProgress();

        if (progress < curCropData.daysToGrow)
        {
            SpriteRenderer.sprite = curCropData.growProgressSprites[progress];//TODO: change this algorithm for difference btwn array length and days to grow
        }
        else
        {
            SpriteRenderer.sprite = curCropData.readyToHarvestSprite;
        }
    }

    public void NewDay()
    {
        m_DaysSinceLastWatered++;

        if(m_DaysSinceLastWatered > curCropData.daysToHarvest)
        {
            Destroy(gameObject);
        }

        UpdateSprite();
    }
}
