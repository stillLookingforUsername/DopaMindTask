using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door;
    public GameObject uiOpen;
    public GameObject uiFindKey;

    private bool isNear = false;
    private bool isOpen = false;

    private void Update()
    {
        if(!isNear) return;

        if(KeyPickup.keyCollected)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                isOpen = !isOpen;
                float targetRotationY = isOpen? 90f : 0f;
                door.localRotation = Quaternion.Euler(0f, targetRotationY, 0f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent<PlayerMovement>(out PlayerMovement player)) return;

        isNear = true;
        if(KeyPickup.keyCollected)
        {
            uiOpen.SetActive(true);
        }
        else
        {
            uiFindKey.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(!other.TryGetComponent<PlayerMovement>(out PlayerMovement player)) return;

        isNear = false;
        uiOpen.SetActive(false);
        uiFindKey.SetActive(false);
    }
}
