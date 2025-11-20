using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVController : MonoBehaviour
{
    public GameObject screenSprite;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public GameObject ui;

    private bool isOn = false;
    private bool isNear = false;

    private void Update()
    {
        if(isNear && Input.GetKeyDown(KeyCode.E))
        {
            ToggleTV();
        }
    }
    private void ToggleTV()
    {
        isOn = !isOn;
        screenSprite.SetActive(isOn);

        if (isOn) 
        {
            videoPlayer.Play();
            audioSource.Play();
        }
        else 
        {
            videoPlayer.Stop();
            audioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            Debug.Log("TV triggered");
            isNear = true;
            ui.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<PlayerMovement>(out PlayerMovement player)) return;
        isNear = false;
        ui.SetActive(false);
    }
}
