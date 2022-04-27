using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GetNewIngredients : MonoBehaviour
{
    Transform mixingStationPosition;
    [SerializeField] GameObject mixingStationPrefab; // table with all the ingredients saved here
    GameObject currentMixingStation; // table with all the ingredients saved here
    Vector3 mixingStationRotation;
    public bool wasActivated = false;
    //Vector3 rot = transform.rotation.eulerAngles;


    void Start()
    {
        currentMixingStation = mixingStationPrefab;
        mixingStationPosition = mixingStationPrefab.transform;
        mixingStationRotation = mixingStationPosition.rotation.eulerAngles;
        currentMixingStation = Instantiate(mixingStationPrefab, mixingStationPosition.position, Quaternion.Euler(mixingStationRotation));
        currentMixingStation.SetActive(true);
    }

    void Update()
    {
        if (wasActivated)
        {
            ResetTheTable();
            wasActivated = false;
        }
    }
    private void OnMouseDown() // doesnt work, dont know why, makes no sense
    {
        ResetTheTable();
        Debug.Log("I was pressed");
    }

    public void ResetTheTable()
    {
        Destroy(currentMixingStation);
        Invoke("NewSupplies", 1f);
    }
    void NewSupplies()
    {
        //Instantiate(mixingStationPrefab, mixingStationPosition.position, Quaternion.Euler(mixingStationRotation));
        //currentMixingStation = GameObject.FindWithTag("mixingStation"); 
        currentMixingStation = Instantiate(mixingStationPrefab, mixingStationPosition.position, Quaternion.Euler(mixingStationRotation));
        currentMixingStation.SetActive(true);
        Debug.Log("a new mixing setup was created");
    }
}
