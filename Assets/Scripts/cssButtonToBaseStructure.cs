using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cssButtonToBaseStructure : MonoBehaviour
{
    public int indexOfStructure;

    public void Click()
    {
        cssStructureCreator.instance.InstantiateStructure(indexOfStructure);
    }
}
