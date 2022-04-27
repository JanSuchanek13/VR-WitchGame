using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNewMixture : MonoBehaviour
{
    #region Variables
    [Header("General Variables:")]
    [SerializeField] GameObject mixture;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") // if player touches this object
        {

        }
    }
}
