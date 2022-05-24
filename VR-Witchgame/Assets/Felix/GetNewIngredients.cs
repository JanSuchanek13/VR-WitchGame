using System.Collections;
using UnityEngine;

public class GetNewIngredients : MonoBehaviour
{
    #region variables:
    [SerializeField] GameObject mixingStationPrefab; // pull table with all the ingredients on it in here

    Transform mixingStationPosition;
    GameObject currentMixingStation;
    Vector3 mixingStationRotation;
    bool inUse = false;
    #endregion

    void Start()
    {
        currentMixingStation = mixingStationPrefab;
        mixingStationPosition = mixingStationPrefab.transform;
        mixingStationRotation = mixingStationPosition.rotation.eulerAngles;
        currentMixingStation = Instantiate(mixingStationPrefab, mixingStationPosition.position, Quaternion.Euler(mixingStationRotation));
        currentMixingStation.SetActive(true);
    }

    private void OnTriggerEnter(Collider other) // this is so you can reset the table 
    {
        if(other.tag == "Player" && inUse == false)
        {
            inUse = true; // stops from resetting over and over when being touched longer.
            ResetTheTable();
            StartCoroutine(CoolDown());
            Debug.Log("Reset-Button was pressed! Starting CoolDown now.");
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(5);
        inUse = false;
        Debug.Log("Reset-Button-Cooldown is over, you may press again!");
    }

    public void ResetTheTable()
    {
        Destroy(currentMixingStation);
        Invoke("GetNewSupplies", 1f);
    }

    void GetNewSupplies()
    {
        currentMixingStation = Instantiate(mixingStationPrefab, mixingStationPosition.position, Quaternion.Euler(mixingStationRotation));
        currentMixingStation.SetActive(true);
        Debug.Log("a new mixing setup was created");
    }
}
