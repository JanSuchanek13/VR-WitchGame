using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vial : MonoBehaviour
{
    #region Variables
    [Header("General Variables:")]
    [SerializeField] GameObject liquidInVial;
    [SerializeField] Light liquidShine;
    [SerializeField] GameObject cork; // by having a cork we dont lose coherence of the liquid having an open vial upside down!
    [SerializeField] AudioSource glasBruch_Sound; // always is set to play, no matter the other effects // should have lesser "priority"!
    [SerializeField] float minPitch = 1f;
    [SerializeField] float maxPitch = 2f;
    [SerializeField] float timeToDestruct = .2f;
    
    [Header("SHRINK-Potion (2):")]
    [SerializeField] AudioSource effect1_Sound;
    [SerializeField] ParticleSystem effect1_Effect;
    [SerializeField] float minimumShrinkEffect = 1.3f;
    [SerializeField] float maximumShrinkEffect = 5f;
    
    [Header("GROW-Potion (3):")]
    [SerializeField] AudioSource effect2_Sound;
    [SerializeField] ParticleSystem effect2_Effect;
    [SerializeField] float minimumGrowEffect = 1.3f;
    [SerializeField] float maximumGrowEffect = 4f;
    
    [Header("COLOR-Potion (4):")]
    [SerializeField] AudioSource effect3_Sound;
    [SerializeField] ParticleSystem effect3_Effect;
    
    [Header("EXPLOSION-Potion (5):")]
    [SerializeField] AudioSource effect4_Sound;
    [SerializeField] ParticleSystem effect4_Effect;
    [SerializeField] float explosionRadius = 3f;
    [SerializeField] float explosionForce = 5f;
    
    [Header("FLOAT-Potion (6):")]
    [SerializeField] AudioSource effect5_Sound;
    [SerializeField] ParticleSystem effect5_Effect;

    [Header("handshakes - DON'T TOUCH!")]
    Color32 currentColor;
    [SerializeField] bool vialIsArmed = false; // only serialized for testing
    [SerializeField] int potionType = 0; // only serialized for testing
    public bool wasThrownOrDropped = false; // handshake for JAN VR Lokomotion, to be turnt "true" when it is thrown or dropped to "arm" the potion
    #endregion

    public void FillVial(int potionEffect, Color32 potionColor)
    {
        vialIsArmed = true;
        cork.SetActive(true);
        liquidInVial.SetActive(true);
        liquidInVial.GetComponent<MeshRenderer>().material.color = potionColor; // determine new color here
                                                                                //emissionSourceMaterial.EnableKeyword("_EMISSION");
                                                                                //liquidInVial.GetComponent<Material>().EnableKeyword("_EMISSION");
        liquidInVial.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");

        liquidInVial.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", potionColor);
        liquidShine.enabled = true;
        liquidShine.color = potionColor;
        potionType = potionEffect; // determine the effect of the vial here:
        currentColor = potionColor;
        Debug.Log("The vial was filled and has effect: " + potionEffect);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(wasThrownOrDropped == true && vialIsArmed == true) // this is to check if the potion actually left the hand of the player
        {
            vialIsArmed = false; // this should prevent the flaks reacting multiple times when hitting multiple things
            GetPotionEffect(collision); // this gets the corresponding effect to the potion carried and applies it to the collision target
        }
    }

    void GetPotionEffect(Collision collision) 
    {
        switch (potionType)
        {
            // no case 1 as currently impossible to make with the ingredients
            case 2: print("shrink");

                //juice:
                //glasBruch_Sound.pitch = Random.Range(minPitch, maxPitch)
                //glasBruch_Sound.Play();
                //effect1_Sound.pitch = Random.Range(minPitch, maxPitch);
                //effect1_Sound.Play();
                //effect1_Effect.Play();

                //effect:
                if (collision.gameObject.tag != "LevelBoundaries") // should prevent outer walls to be modded
                {
                    float randomShrinkNumber = Random.Range(minimumShrinkEffect, maximumShrinkEffect);
                    collision.gameObject.GetComponent<Transform>().localScale /= randomShrinkNumber;
                }
                Invoke("DestroyThis", timeToDestruct);
                break;

            case 3: print("grow");

                //juice:
                //glasBruch_Sound.pitch = Random.Range(minPitch, maxPitch)
                //glasBruch_Sound.Play();
                //effect2_Sound.pitch = Random.Range(minPitch, maxPitch);
                //effect2_Sound.Play();
                //effect2_Effect.Play();

                //effect:
                if (collision.gameObject.tag != "LevelBoundaries") // should prevent outer walls to be modded
                {
                    float randomGrowNumber = Random.Range(minimumGrowEffect, maximumGrowEffect);
                    collision.gameObject.GetComponent<Transform>().localScale *= randomGrowNumber;
                }
                Invoke("DestroyThis", timeToDestruct);
                break;

            case 4: print("color");
                //juice:
                //glasBruch_Sound.pitch = Random.Range(minPitch, maxPitch)
                //glasBruch_Sound.Play();
                //effect3_Sound.pitch = Random.Range(minPitch, maxPitch);
                //effect3_Sound.Play();
                //effect3_Effect.Play();

                //effect:
                collision.gameObject.GetComponent<MeshRenderer>().material.color = currentColor;
                Invoke("DestroyThis", timeToDestruct);
                break;

            case 5: print("explosion");

                //juice:
                //glasBruch_Sound.pitch = Random.Range(minPitch, maxPitch)
                //glasBruch_Sound.Play();
                //effect4_Sound.pitch = Random.Range(minPitch, maxPitch);
                //effect4_Sound.Play();
                //effect4_Effect.Play();

                //effect:
                Collider[] objectsInExplosionRadius = Physics.OverlapSphere(transform.position, explosionRadius);   //all Objects in explosion Range
                foreach (Collider i in objectsInExplosionRadius)
                {
                    Rigidbody rig = i.GetComponent<Rigidbody>();
                    if (rig != null && rig.isKinematic == false) // this is for explosion to not blast walls of the scene off
                    {
                        rig.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1f, ForceMode.Impulse);
                    }
                }
                Invoke("DestroyThis", timeToDestruct);
                break;

            case 6: print("float");
                
                //juice:
                //glasBruch_Sound.pitch = Random.Range(minPitch, maxPitch)
                //glasBruch_Sound.Play();
                //effect5_Sound.pitch = Random.Range(minPitch, maxPitch);
                //effect5_Sound.Play();
                //effect5_Effect.Play();

                //effect:
                if (collision.gameObject.tag != "LevelBoundaries")
                {
                    collision.gameObject.GetComponent<Rigidbody>().mass = 0;
                    collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                Invoke("DestroyThis", timeToDestruct);
                break;

            default:
                print("Incorrect potion effect.");
                break;
        }
    }
    void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
