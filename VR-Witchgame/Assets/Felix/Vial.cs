using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vial : MonoBehaviour
{
    #region Variables
    [Header("Vial variables:")]
    [SerializeField] GameObject liquidInVial;
    [SerializeField] GameObject cork; // by having a cork we dont lose coherence of the liquid having an open vial upside down!
    [SerializeField] AudioSource effect1_Sound;
    [SerializeField] ParticleSystem effect1_Effect;
    [SerializeField] AudioSource effect2_Sound;
    [SerializeField] ParticleSystem effect2_Effect;
    [SerializeField] AudioSource effect3_Sound;
    [SerializeField] ParticleSystem effect3_Effect;
    [SerializeField] AudioSource effect4_Sound;
    [SerializeField] ParticleSystem effect4_Effect;
    [SerializeField] AudioSource effect5_Sound;
    [SerializeField] ParticleSystem effect5_Effect;
    [SerializeField] AudioSource effect6_Sound;
    [SerializeField] ParticleSystem effect6_Effect;

    [Header("handshakes - DON'T TOUCH!")]
    Color32 colorOfMixture;
    bool vialWasFilled = false;
    int potionType;
    public bool wasThrownOrDropped = false;

    
    int color_R_component;
    int color_G_component;
    int color_B_component;
    int color_A_component;
    int amountOfIngredients = 0;
    int currentIngredientList = 0;
    GameObject usedIngredient;
    public int potionEffect = 0;
    public bool potionIsReady = false;
    #endregion

    public void FillVial(int potionEffect, Color32 potionColor)
    {
        vialWasFilled = true;
        cork.SetActive(true);
        liquidInVial.GetComponent<MeshRenderer>().material.color = potionColor; // determine new color here
        potionType = potionEffect; // determine the effect of the vial here:
        Debug.Log("The vial was filled!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(wasThrownOrDropped == true)
        {
            GetPotionEffect(collision);
        }
    }

    void GetPotionEffect(Collision collision)
    {
        switch (potionType)
        {
            case 1: // should be redundant
                print("effect 1");
                //effect1_Sound.Play();
                //effect1_Effect.Play();
                //effect:
                collision.gameObject.GetComponent<Transform>().localScale = new Vector3(0f, 0f, 0f);
                Debug.Log("the hit target: " + collision.gameObject.name + "should now be scaled down to 0");
                Invoke("DestroyThis", .5f);
                break;
            case 2:
                print("effect 2");
                //effect2_Sound.Play();
                //effect2_Effect.Play();
                //effect:
                collision.gameObject.GetComponent<Transform>().localScale = new Vector3(0f, 0f, 0f);
                Debug.Log("the hit target: " + collision.gameObject.name + "should now be scaled down to 0");
                Invoke("DestroyThis", .5f);
                break;
            case 3:
                print("effect 3");
                //effect3_Sound.Play();
                //effect3_Effect.Play();
                //effect:
                collision.gameObject.GetComponent<Transform>().localScale = new Vector3(0f, 0f, 0f);
                Debug.Log("the hit target: " + collision.gameObject.name + "should now be scaled down to 0");
                Invoke("DestroyThis", .5f);
                break;
            case 4:
                print("effect 4");
                //effect4_Sound.Play();
                //effect4_Effect.Play();
                //effect:
                collision.gameObject.GetComponent<Transform>().localScale = new Vector3(0f, 0f, 0f);
                Debug.Log("the hit target: " + collision.gameObject.name + "should now be scaled down to 0");
                Invoke("DestroyThis", .5f);
                break;
            case 5:
                print("effect 5");
                //effect5_Sound.Play();
                //effect5_Effect.Play();
                //effect:
                collision.gameObject.GetComponent<Transform>().localScale = new Vector3(0f, 0f, 0f);
                Debug.Log("the hit target: " + collision.gameObject.name + "should now be scaled down to 0");
                Invoke("DestroyThis", .5f);
                break;
            case 6:
                print("effect 6");
                //effect6_Sound.Play();
                //effect6_Effect.Play();
                //effect:
                collision.gameObject.GetComponent<Transform>().localScale = new Vector3(0f, 0f, 0f);
                Debug.Log("the hit target: " + collision.gameObject.name + "should now be scaled down to 0");
                Invoke("DestroyThis", .5f);
                break;
            default:
                print("Incorrect potion effect.");
                break;
        }
    }

    void DestroyThis()
    {
        Destroy(this);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
