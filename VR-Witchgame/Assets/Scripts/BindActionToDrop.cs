using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindActionToDrop : MonoBehaviour
{
    public void Drop()
    {
        Debug.Log("was dropped");
        GetComponent<Vial>().wasThrownOrDropped = true;
    }
}
