using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class TileController : MonoBehaviour
{
    bool tilled = false;
    SpriteRenderer spriteRenderer;
    Sprite wateredTilledSprite;
    Sprite tilledSprite;
    Sprite grassSprite;
    Crop curCrop;

    public void Interact()
    {
        if (!tilled)
        {
            Till();
        }
        else if (!HasCrop() && GameManager.Instance.CanPlantCrop())
        {
            PlantNewCrop(GameManager.Instance.SelectedCropToPlant);
        }
        else if (HasCrop() && curCrop.CanHarvest())
        {
            curCrop.Harvest();
        }
        else
        {
            Water();
        }
    }

    void Till()
    {
        tilled = true;
        spriteRenderer.sprite = tilledSprite;
    }

    void Water()
    {
        if (HasCrop())
        {
            curCrop.Water();
        }
        else
        {
            spriteRenderer.sprite = wateredTilledSprite;

        }       
    }


    public void PlantNewCrop(CropData cropData)
    {
        if (!tilled)
        {
            return;
        }
        //TODO:
        //curCrop = Instantiate(cropPrefab, transform).GetComponent<Crop>();
        //curCrop.Plant(cropData)
        GameManager.Instance.onNewDay += OnNewDay;

    }

    bool HasCrop()
    {
        return curCrop != null;
    }

    void OnNewDay()
    {
        if (!HasCrop())
        {
            tilled = false;
            spriteRenderer.sprite = grassSprite;
            GameManager.Instance.onNewDay -= OnNewDay;
        }
        else
        {
            spriteRenderer.sprite = tilledSprite;
            curCrop.NewDay();
        }
    }
}
