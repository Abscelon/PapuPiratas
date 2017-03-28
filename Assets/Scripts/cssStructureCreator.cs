using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Se encarga de instanciar y posicionar una estructura de base.
/// </summary>
public class cssStructureCreator : MonoBehaviour
{
    public static cssStructureCreator instance; // singleton
    public GameObject[] structures;
    public Transform instPoint;

    private cssBaseStructure newStructure;
    private List<cssBaseStructure> baseStructures;

    // crear sistema de snap

    private void Awake()
    {
        if (instance == null)
            instance = this;
        baseStructures = new List<cssBaseStructure>();
    }

    #region Public Methods
    public void InstantiateStructure(int indexOfStructure)
    {
        GameObject go = structures[indexOfStructure];
        newStructure = Instantiate(go, instPoint.position, go.transform.rotation).GetComponent<cssBaseStructure>();
        AddBaseStructure(newStructure);
    }

    public void AddBaseStructure(cssBaseStructure st)
    {
        baseStructures.Add(st);
    }

    public void ActivateAllBaseStructures()
    {
        foreach (cssBaseStructure st in baseStructures)
            st.Activate();
    }
    #endregion
}
