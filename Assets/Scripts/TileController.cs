using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public ICropFactory cropFactory;
    private Rigidbody2D rb;


    public void Awake()//I'm not sure if this needs to be in Awake or Enable because I don't necessarily need to do it all the time?
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.Log("cannot find the sprite renderer");
        }
        rb = GetComponent<Rigidbody2D>();
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

    public void Till()//TODO: make private
    {
        Debug.Log("till");
        tilled = true;
        spriteRenderer.sprite = tilledSprite;
    }

    public void Water()//TODO: make private 
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
        var cropPrefab = Resources.Load("CropPrefab");
        //TODO:
        //if(!cropFactory.CreateCrop(cropData).TryGetComponent<Crop>(out curCrop))
        //{
        //    Debug.LogError("There is no Crop component assigned to this prefab.");
        //}
        //curCrop = Instantiate(CropFactory.Instance.CreateCrop(cropData),transform).GetComponent<Crop>();
        curCrop = Instantiate(cropPrefab, transform).GetComponent<Crop>();
        curCrop.Plant(cropData);
        spriteRenderer.sprite = null;
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
            //spriteRenderer.sprite = tilledSprite;
            curCrop.NewDay();
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.onNewDay -= OnNewDay;
    }
    
    public void MakeLarger()
    {
        gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 0f);
    }

    public void ReturnToNormalSize()
    {
        gameObject.transform.localScale = Vector3.one;
    }

    public void Update()
    {
        //This should not be done like this. I should subscribe to events instead.
        if (gameObject== GameManager.Instance.SelectedTile)
        {
            MakeLarger();
        }
        else
        {
            ReturnToNormalSize();
        }
    }
}
