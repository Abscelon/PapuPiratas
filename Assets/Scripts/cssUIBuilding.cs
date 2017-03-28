using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class cssUIBuilding : MonoBehaviour , IPointerClickHandler {

    public cssCurrencyBar barScript;
    public bool usable;
    public float currency, myPrice;

    private void LateUpdate()
    {
        currency = barScript.getCurrency();
        if (currency >= myPrice)
            usable = true;
        else
            usable = false;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (usable)
        {
            barScript.applyCost(myPrice);
            barScript.refreshCurrency();
            print("built! there's " + barScript.getCurrency() + " left");
        }
        else
        {
            print("can´t build! Need: " + myPrice + " You have: " + barScript.getCurrency());
        }
    }
}
