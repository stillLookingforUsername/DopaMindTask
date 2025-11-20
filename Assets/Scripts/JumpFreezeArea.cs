using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JumpFreezeArea : MonoBehaviour
{
    public GameObject jumpUI; //World space canvas
    public Transform lookTarget; //point on the wall to face
    public TextMeshProUGUI jumpCounterText; //reference to JumpCounter text  inside  the jumpUI

    private bool active = false;
    private int jumpCount = 0;

/*
    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent<PlayerMovement>(out PlayerMovement player)) return;

        //disable normal movement and enable jump only movement
        var pm = other.GetComponent<PlayerMovement>();
        if(pm != null) pm.enabled = false;

        var jo = other.GetComponent<JumpOnly>();
        if(jo != null) jo.enabled = true;

        //force player to look at the jump info
        other.transform.LookAt(lookTarget);
        jumpUI.SetActive(true);
        active = true;
        UpdateCounter();
    }
    */
    private void OnTriggerEnter(Collider other)
{
    if (!other.TryGetComponent<PlayerMovement>(out PlayerMovement player)) return;

    var pm = other.GetComponent<PlayerMovement>();
    if (pm != null) pm.enabled = false;

    var jo = other.GetComponent<JumpOnly>();
    if (jo != null)
    {
        jo.enabled = true;

        // 🔥 SUBSCRIBE HERE
        jo.OnJump += HandleJumpEvent;
    }

    other.transform.LookAt(lookTarget);
    jumpUI.SetActive(true);
    active = true;
    UpdateCounter();
}
/*

    private void OnTriggerExit(Collider other)
    {
        if(!other.TryGetComponent<PlayerMovement>(out PlayerMovement player)) return;
        
        var pm = other.GetComponent<PlayerMovement>();
        if(pm != null) pm.enabled = true;

        var jo = other.GetComponent<JumpOnly>();
        if(jo != null) jo.enabled = false;

        jumpUI.SetActive(false);
        active = false;
    }
    */private void OnTriggerExit(Collider other)
{
    if (!other.TryGetComponent<PlayerMovement>(out PlayerMovement player)) return;

    var pm = other.GetComponent<PlayerMovement>();
    if (pm != null) pm.enabled = true;

    var jo = other.GetComponent<JumpOnly>();
    if (jo != null)
    {
        jo.enabled = false;

        // 🔥 UNSUBSCRIBE HERE
        jo.OnJump -= HandleJumpEvent;
    }

    jumpUI.SetActive(false);
    active = false;
}

/*

    private void Update()
    {
        if(!active) return;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpCount++;
            UpdateCounter();
        }
    }
    */
    private void HandleJumpEvent()
{
    if(!active) return;
    jumpCount++;
    UpdateCounter();
}


    private void UpdateCounter()
    {
        if(jumpCounterText != null)
        {
            jumpCounterText.text = "Jumps Count: " + jumpCount.ToString();
        }
    }

}
