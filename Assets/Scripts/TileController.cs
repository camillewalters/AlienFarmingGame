using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class TileController : MonoBehaviour
{
    bool tilled = false;
    SpriteRenderer spriteRenderer;
    public Sprite wateredTilledSprite;
    public Sprite tilledSprite;
    public Sprite grassSprite;
    Crop curCrop;


    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.Log("cannot find the sprite renderer");
        }
    }
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

    public void Till()
    {
        Debug.Log("till");//Camille: for some reason this isnt hitting from the button currently
        tilled = true;
        spriteRenderer.sprite = tilledSprite;
        Debug.Log(spriteRenderer.sprite.name);
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
        Debug.Log("planting a crop");
        if (!tilled)
        {
            return;
        }
        //TODO:
        //curCrop = Instantiate(cropPrefab, transform).GetComponent<Crop>();
        //curCrop.Plant(cropData)
        GameManager.Instance.onNewDay += OnNewDay;//this is not being called right now because i never subscribed

    }

    bool HasCrop()
    {
        return curCrop != null;
    }

    void OnNewDay()
    {
        Debug.Log("New day for this tile");
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

    private void OnDestroy()
    {
        GameManager.Instance.onNewDay -= OnNewDay;
    }
}
