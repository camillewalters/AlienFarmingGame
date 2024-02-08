using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDayButtonManager : MonoBehaviour
{ 

    // This method will be called when the button is clicked
    public void OnButtonClick()
    {
       GameManager.Instance.RaiseNewDayEvent();
    }
}
