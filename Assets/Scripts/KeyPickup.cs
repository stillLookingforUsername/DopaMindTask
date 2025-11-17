using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject keyUI;
    private static bool keyCollected = false;

    private bool isNear = false;

    private void Update()
    {
        if(isNear && Input.GetKeyDown(KeyCode.E) && !keyCollected)
        {
            keyCollected = true;
            keyUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent<PlayerMovement>(out PlayerMovement player)) return;
        isNear = true;
        keyUI.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if(!other.TryGetComponent<PlayerMovement>(out PlayerMovement player)) return;
        isNear = false;
        keyUI.SetActive(false);
    }


}
