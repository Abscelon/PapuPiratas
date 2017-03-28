using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cssCombatManager : MonoBehaviour {

    #region Public Variables
    //Es la posición, dentro de los arrays de cartas, de la carta seleccionada por el jugador
    public int selectedCardCoord;
    /*GameObject con forma de márco que usamos, moviendolo sobre una carta, para indicarle al jugador cuál carta
      tiene seleccionada*/
    public GameObject selectionFrame;
    //Script "CurrencyBarr" que usamos para actualizar la barra y manejar las cantidades de monedas, añadiendole o restandole
    public cssCurrencyBar currencyBar;
    //Array "prices" que tiene todos los precios de las cartas que el jugador escogió
    public float[] prices;
    //Array "cardsButtons" de los objetos que usamos como botones para las cartas en el UI
    public GameObject[] cardsButtons;
    //Array de los scripts de cada carta en el mismo orden que los anteriores
    public cssUICombat[] cardsScripts;
    //Array de las imagenes de cada carta en el mismo orden que los anteriores arrays de los componentes de las cartas
    public Image[] cardsImages;
    //public int largoDeArrayDePrices; útil para no tener que buscar este valor constante varias veces durante combate
    #endregion

    #region Private Variables
    
    //Es el precio de la carta que tenemos seleccionada
    private float selectedCardPrice;
    //Booleana que indica si se puede usar alguna de las cartas de todo el deck
    private bool activeDeck;
    #endregion



    #region Public Methods
    //Cambiar la carta seleccionada si la que se escogió es menor o igual que la cantidad de moneda disponible
    public void setSelectedCardTo(int myCardCoord)
    {
        if(prices[myCardCoord] > currencyBar.getCurrency())
        {
            print("Can't use! You need: " + prices[myCardCoord] + " and you only have: " + currencyBar.getCurrency());
        }
        else
        {
            selectedCardCoord = myCardCoord;
            selectionFrame.transform.localPosition = cardsButtons[selectedCardCoord].transform.localPosition;
        }
    }
    /* En prueba
    public void setSelectedCardPrice(float myPrice)
    {
        prices[selectedCardCoord] = myPrice;
    }
    */
    //Revisar cada carta y poner su transparencia de acuerdo a si puede usarse o no
    public void checkForUsableCards()
    {
        for(int i = 0; i < prices.Length; i++)
        {
            if (prices[i] <= currencyBar.getCurrency())
            {
                cardsImages[i].color = new Color(255f, 255f, 255f, 1f);
            }
            else
            {
                cardsImages[i].color = new Color(255f, 255f, 255f, 0.5f);
            }
        }
    }
    //Cambiar la carta seleccionada a cualquier otra carta que se pueda usar
    //(solo se usa cuando la carta seleccionada es demasiado cara para seguirse usando)
    public void setObligatedCardChange()
    {
        for(int i = 0; i < prices.Length; i++)
        {
            if (prices[i] <= currencyBar.getCurrency())
                setSelectedCardTo(i);
        }
        checkForActiveDeck();
    }
    //Revisar si el deck de cartas debería estar activo o no dependiendo de la cantidad de moneda
    public void checkForActiveDeck()
    {
        if (prices[selectedCardCoord] > currencyBar.getCurrency())
            activeDeck = false;
        else
            activeDeck = true;
    }
    //Retornar el precio de la carta seleccionada
    public float getSelectedCardPrice()
    {
        return prices[selectedCardCoord];
    }
    #endregion

    #region Unity Callbacks
    //Ponemos a la primera carta del deck como la seleccionada
    //Revisamos si se puede usar cualquiera de las cartas en el deck
    //Le asignamos el valor a la variable dentro de los scripts de las cartas la posición individual correspondiente en el array
    private void Start()
    {
        setSelectedCardTo(0);
        checkForActiveDeck();
        for(int i = 0; i < cardsScripts.Length; i++)
        {
            cardsScripts[i].coordOfSelfInArray = i;
        }
    }
    //Esperamos el input para utilizar la carta seleccionada
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && prices[selectedCardCoord] <= currencyBar.getCurrency())
        {
            currencyBar.applyCost(prices[selectedCardCoord]);
            currencyBar.refreshCurrency();
        }
    }
    #endregion
}
