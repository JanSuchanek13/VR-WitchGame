using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public bool ingredient_1 = false;
    public bool ingredient_2 = false;
    public bool ingredient_3 = false;
    public Color32 additiveColor;
    public int effect = 0;
    public bool wasUsed = false;
    void Start()
    {
        // save my color to hand over if used:
        additiveColor = this.GetComponent<MeshRenderer>().material.color;

        // find out what type of ingredient I am:
        if (this.tag == "Ing_1")
        {
            ingredient_1 = true;
            effect = 1;
            //Debug.Log("I am 1");
        }else if(this.tag == "Ing_2")
        {
            ingredient_2 = true;
            effect = 2;
            //Debug.Log("I am 2");
        }else
        {
            ingredient_3 = true;
            effect = 3;
            //Debug.Log("I am 3");
        }
    }
}
