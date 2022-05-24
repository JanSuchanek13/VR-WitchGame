using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Mixture : MonoBehaviour
{
    #region Variables
    [Header("Mixture variables:")]
    [SerializeField] float minPitch = 1f;
    [SerializeField] float maxPitch = 2f;
    // when adding anything to the pot (actual ingredients and otherwise):
    [SerializeField] AudioSource addedSomething_Sound; // high pitched "plopp" sound
    [SerializeField] ParticleSystem addedSomething_Effect; //drops squirting out in the current color
    // when you successfully added two actual ingredients:
    [SerializeField] AudioSource poof_Sound;
    //[SerializeField] ParticleSystem poof_Effect; // Susi used a GO instead of PE
    [SerializeField] GameObject poof_Effect;
    
    [Header("handshakes - DON'T TOUCH!")]
    Color32 baseColor;
    int color_R_component;
    int color_G_component;
    int color_B_component;
    int color_A_component;
    int amountOfIngredients = 0;
    int currentIngredientList = 0;
    GameObject usedIngredient;
    public Color32 colorOfMixture;
    public int potionEffect = 0;
    public bool potionIsReady = false;
    #endregion


    void Start()
    {
        baseColor = this.GetComponent<MeshRenderer>().material.color;
        colorOfMixture = baseColor;
        color_R_component = colorOfMixture.r;
        color_G_component = colorOfMixture.g;
        color_B_component = colorOfMixture.b;
        color_A_component = colorOfMixture.a;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Kettle" && other.tag != "mixture" && other.tag != "LevelBoundaries") // dont splash by default.
        {
            if (other.GetComponent<Ingredient>() && other.GetComponent<Ingredient>().wasUsed == false)
            {
                if (amountOfIngredients < 2)
                {
                    Debug.Log("an ingredient was thrown in the pot");
                    usedIngredient = other.gameObject;

                    //usedIngredient = other.GetComponentInParent<GameObject>();
                    other.GetComponent<Ingredient>().wasUsed = true;
                    amountOfIngredients++;

                    // adds color from ingredient to mixture:
                    color_R_component += other.GetComponent<Ingredient>().additiveColor.r;
                    if (color_R_component > 255)  // expl.: if value is higher than 255 it starts to subract the overhang!!!
                    {
                        color_R_component = 255;
                    }
                    color_G_component += other.GetComponent<Ingredient>().additiveColor.g;
                    if (color_G_component > 255)
                    {
                        color_G_component = 255;
                    }
                    color_B_component += other.GetComponent<Ingredient>().additiveColor.b;
                    if (color_B_component > 255)
                    {
                        color_B_component = 255;
                    }
                    color_A_component += other.GetComponent<Ingredient>().additiveColor.a / 3; // the regular alpha of the object is 255, needs to be a fraction (3)
                    if (color_A_component > 255)
                    {
                        color_A_component = 255;
                    }
                    colorOfMixture = new Color32((byte)color_R_component, (byte)color_G_component, (byte)color_B_component, (byte)color_A_component); // determine new color here
                    this.GetComponent<MeshRenderer>().material.color = colorOfMixture; // apply color here

                    currentIngredientList += other.GetComponent<Ingredient>().effect; // add to the mixtures effect:

                    Invoke("DestroyIngredient", .5f); // get rid of the dropped ingredient:

                    if (amountOfIngredients >= 2) // finish the process if 2 ingredients were used:
                    {
                        CookPotion();
                        Debug.Log("The potion has 2 components and is ready");
                    }
                }
            }

            // reset the Mixture:
            if(other.tag == "Neutralizer")
            {
                colorOfMixture = baseColor; // reset mixture color.
                this.GetComponent<MeshRenderer>().material.color = colorOfMixture; // apply color here.

                poof_Effect.SetActive(false);
                /*ParticleSystem.MainModule bigCloud = poof_Effect.GetComponent<ParticleSystem>().main;
                ParticleSystem.MainModule fireSparks = poof_Effect.GetComponentInChildren<ParticleSystem>().main;
                poof_Effect.GetComponentInChildren<Light>().color = colorOfMixture;
                bigCloud.startColor = new ParticleSystem.MinMaxGradient(colorOfMixture);
                fireSparks.startColor = new ParticleSystem.MinMaxGradient(colorOfMixture);
                poof_Effect.GetComponentInChildren<Light>().enabled = false; // this turns off lamp when resetting the mixture!*/
                
                // reset the mixing channels:
                color_R_component = colorOfMixture.r;
                color_G_component = colorOfMixture.g;
                color_B_component = colorOfMixture.b;
                color_A_component = colorOfMixture.a;

                currentIngredientList = 0; // reset ingredient-list.
                amountOfIngredients = 0; // reset ingredient-counter.
                potionIsReady = false; // can reset the mixture, even if it was done.
                Debug.Log("The mixture should have been reset now!");
                Debug.Log("color " + colorOfMixture + "; current value of Ingredients: " + currentIngredientList + "; number of different ing. in pot: " + amountOfIngredients + "; is the potion read?: " + potionIsReady);
            }

            // juice effect for dropping something into the pot:
            bool opaqueSpill = true;
            int saveOldAlpha = color_A_component;
            usedIngredient = other.gameObject;
            //addedSomething_Sound.pitch = Random.Range(minPitch, maxPitch);
            //addedSomething_Sound.Play();
            ParticleSystem.MainModule settings = addedSomething_Effect.GetComponent<ParticleSystem>().main;
            if(color_A_component != 255) // change color of splash to fully opaque
            {
                opaqueSpill = false;
                color_A_component = 255;
                colorOfMixture = new Color32((byte)color_R_component, (byte)color_G_component, (byte)color_B_component, (byte)color_A_component); // determine new color here
            }
            settings.startColor = new ParticleSystem.MinMaxGradient(colorOfMixture);
            addedSomething_Effect.Play();

            // clean up:
            if (other.tag != "vial" && other.tag != "Player") // dont destroy player or vial when touching mixture:
            {
                Invoke("DestroyIngredient", .5f); // get rid of the dropped item:
            }
            if (opaqueSpill == false) // change color back to normal (or the mixture will change)
            {
                opaqueSpill = true;
                color_A_component = saveOldAlpha;
                colorOfMixture = new Color32((byte)color_R_component, (byte)color_G_component, (byte)color_B_component, (byte)color_A_component); // determine new color here
            }
            if (other.tag == "vial" && potionIsReady == true)
            {
                other.GetComponent<Vial>().FillVial(currentIngredientList, colorOfMixture);
            }
        }
    }
    void DestroyIngredient()
    {
        Destroy(usedIngredient);
        //usedIngredient.SetActive(false);
        usedIngredient = null;
    }
    void CookPotion()
    {
        //poof_Sound.pitch = Random.Range(minPitch * 2, maxPitch * 2); // louder than just adding stuff
        //poof_Sound.Play();

        //poof_Effect.Play();

        // amplify crazy colors:
        float color_R_component = colorOfMixture.r;
        float color_G_component = colorOfMixture.g;
        float color_B_component = colorOfMixture.b;
        //Debug.Log(colorOfMixture);
        if(color_R_component > color_G_component && color_R_component > color_B_component) // if RED dominant
        {
            color_R_component = 255;
            color_G_component *= 8f;
            color_B_component *= 8f;
        }else if(color_R_component < color_G_component && color_G_component > color_B_component) // if GREEN dominant
        {
            color_R_component *= .8f;
            color_G_component = 255;
            color_B_component *= 8f;
        }else // if BLUE dominant
        {
            color_R_component *= .8f;
            color_G_component *= 8f;
            color_B_component = 255;
        }
        colorOfMixture = new Color32((byte)color_R_component, (byte)color_G_component, (byte)color_B_component, (byte)color_A_component); // determine new color here
        this.GetComponent<MeshRenderer>().material.color = colorOfMixture; // apply color here
        //Debug.Log(colorOfMixture);

        ParticleSystem.MainModule bigCloud = poof_Effect.GetComponent<ParticleSystem>().main;
        ParticleSystem.MainModule fireSparks = poof_Effect.GetComponentInChildren<ParticleSystem>().main;
        poof_Effect.GetComponentInChildren<Light>().color = colorOfMixture;
        bigCloud.startColor = new ParticleSystem.MinMaxGradient(colorOfMixture);
        //Debug.Log("big cloud color: " + poof_Effect.GetComponentInChildren<Light>().color);
        fireSparks.startColor = new ParticleSystem.MinMaxGradient(colorOfMixture);
        //Debug.Log("small fire cracker color: " + fireSparks.startColor);
        poof_Effect.GetComponentInChildren<Light>().enabled = true; // this reacts to the pot being reset and lamps turnt off!
        poof_Effect.SetActive(true);

        potionEffect = currentIngredientList; // potionEffect is the public var. that is used by vial to pick up the potion.
        potionIsReady = true;
    }
}
