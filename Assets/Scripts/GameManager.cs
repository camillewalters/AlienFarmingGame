using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int CurrentDay;
    public int Money;
    public CropData SelectedCropToPlant;
    public int Inventory = 5;//TODO: implement this properly
    public GameObject SelectedTile;

    public static GameManager Instance;
    public event UnityAction onNewDay; //TODO: implement when this is changed (right now its a button)

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

    // Property to check if the event is subscribed
    public bool IsOnNewDayEventSubscribed
    {
        get { return onNewDay != null; }
    }

    void OnEnable()
    {
        Crop.onPlantCrop += OnPlantCrop;
        Crop.onHarvestCrop += OnHarvestCrop;
    }
    void OnDisable()
    {
        Crop.onPlantCrop -= OnPlantCrop;
        Crop.onHarvestCrop -= OnHarvestCrop;
    }

    // Called when a crop has been planted.
    // Listening to the Crop.onPlantCrop event.
    public void OnPlantCrop(CropData crop)
    {
        Inventory--;
    }
    // Called when a crop has been harvested.
    // Listening to the Crop.onCropHarvest event.
    public void OnHarvestCrop(CropData crop)
    {
        Money += crop.sellPrice;
    }

    // Called when we want to purchase a crop.
    public void PurchaseCrop(CropData crop)
    {
    }

    // Do we have enough crops to plant?
    public bool CanPlantCrop()
    {
        //return Inventory > 0;
        return true;
    }
    // Called when the buy crop button is pressed.
    public void OnBuyCropButton(CropData crop)
    {

    }

    public void RaiseNewDayEvent()
    {
        Debug.Log("raise new day event");
        CurrentDay++;
        onNewDay?.Invoke();

    }
}

