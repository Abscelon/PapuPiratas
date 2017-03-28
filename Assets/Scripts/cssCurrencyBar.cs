using UnityEngine;
using System.Collections;

public class cssCurrencyBar : MonoBehaviour {

    #region Public Variables
    //maxCurrency es el maximo total que puedo tener, y myCurrency es lo que tengo de la moneda
    public float maxCurrency, myCurrency;
    /*Script "CombatManager" que maneja el estado de los botones de las cartas en el UI, recibe de
      esta clase las cantidades de monedas disponibles, le indica a esta clase cuando actualizar los valores en
      pantalla, y recibe inputs del combate en pantalla */
    public cssCombatManager combatManager;
    #endregion

    #region Private Variables


    #region Lerping's Variables
    /// <summary>
    /// Variables de Lerp
    /// </summary>
    // areLerping significa que se está interpolando
    private bool areLerping = false;
    // timeOfLerp es el tiempo total que toma la interpolacion (0.3f) y timeOfStart el tiempo inicial
    private float timeOfLerp = 0.3f, timeOfStart;
    //startPos es la posicion inicial, y endPos la posicion final
    private Vector3 startPos, endPos;
    #endregion
    #endregion

    #region Public Methods
    //Actualiza la transformacion de la barra hasta el porcentaje disponible de moneda
    public void refreshCurrency()
    {
        StartLerping();
    }

    //Aplica el costo que recibe, a la cantidad total de la moneda e indica al CombatManager que verifique si las cartas
    //seran utilizables tras aplicar el cambio
    public void applyCost(float cost)
    {
        myCurrency -= cost;
        refreshCurrency();
        combatManager.checkForUsableCards();
    }

    //Añade la cantidad que recibe e indica al CombatManager revisar si las cartas seran utilizables tras aplicar el cambio
    public void addToCurrency(float amount)
    {
        myCurrency += amount;
        refreshCurrency();
        combatManager.checkForUsableCards();
    }

    //Retorna la cantidad que tengo de la moneda
    public float getCurrency()
    {
        return myCurrency;
    }
    #endregion

    #region Lerping's methods
    /// <summary>
    /// Ambas funciones de abajo son usadas exclusivamente en lerp
    /// </summary>
    //Lerping ocurre en esta funcion
    void FixedUpdate()
    {
        if (areLerping)
        {
            float timeSinceStarted = Time.time - timeOfStart;
            float percentageComplete = timeSinceStarted / timeOfLerp;

            transform.localScale = Vector3.Lerp(startPos, endPos, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                areLerping = false;
            }
        }
    }

    //Prepara el comienzo del lerp
    void StartLerping()
    {
        timeOfStart = Time.time;

        startPos = transform.localScale;
        endPos = new Vector3(myCurrency / maxCurrency, transform.localScale.y, transform.localScale.z);
        areLerping = true;
    }
    #endregion
}
