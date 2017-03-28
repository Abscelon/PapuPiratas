using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class cssUICombat : MonoBehaviour, IPointerClickHandler {

    #region Public Variables
    /*Script "CombatManager" que maneja el estado de los botones de las cartas en el UI, recibe de
      esta clase las cantidades de monedas disponibles, le indica a esta clase cuando actualizar los valores en
      pantalla, y recibe inputs del combate en pantalla */
    public cssCombatManager combatManager;
    //Es la posición de cada carta en el array que CombatManager usa para distinguir las cartas individualmente
    public int coordOfSelfInArray;
    #endregion

    //Le indica a CombatManager que se tocó esta carta
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        combatManager.setSelectedCardTo(coordOfSelfInArray);
    }
}
