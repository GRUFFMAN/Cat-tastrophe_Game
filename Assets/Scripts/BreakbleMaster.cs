using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakbleMaster : MonoBehaviour
{
    public GameObject humanRef;

    void Start()
    {
        humanRef = GameObject.Find("Cat-Cher");
    }

}
