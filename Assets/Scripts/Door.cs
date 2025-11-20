/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    [SerializeField] private bool isRotatingDoor = true;
    [SerializeField] private float speed = 1f;
    [Header("Rotation Configs")]
    [SerializeField] private float RotationAmount = 90f;
    [SerializeField] private float ForwardDirection = 0;    //this going to be the number that we compare our Dot product with to determine forward direction

    private Vector3 StartRotation;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        //since "Forward" actually is pointing to the Z axis, we can just use transform.forward
        //since "Forward" actually is pointing into the door frame, choose  a direction to think about as "forward"
        Forward = transform.forward;
    }

    public void Open(Vector3 UserPosition)
    {
        if(!isOpen)
        {
            if(AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }
            if(isRotatingDoor)
            {
                float dot = Vector3.Dot(Forward,(UserPosition - transform.position).normalized);
                Debug.Log($"Dot: {dot.ToString("N3")}");
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
        }
    }

/*
    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if(ForwardAmount >= ForwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0,startRotation.eulerAngles.y - RotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0,startRotation.eulerAngles.y + RotationAmount, 0));
        }
        isOpen = true;
        float time = 0;

        if(time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
    */
    /*
    private IEnumerator DoRotationOpen(float ForwardAmount)
{
    Quaternion startRotation = transform.rotation;

    float startYaw = startRotation.eulerAngles.y;
    Quaternion endRotation;

    if (ForwardAmount >= ForwardDirection)
        endRotation = Quaternion.Euler(0, startYaw - RotationAmount, 0);
    else
        endRotation = Quaternion.Euler(0, startYaw + RotationAmount, 0);

    isOpen = true;

    float time = 0f;
    while (time < 1f)
    {
        transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
        yield return null;
        time += Time.deltaTime * speed;
    }

    transform.rotation = endRotation;   // snap exactly at end
}


    public void Close()
    {
        if(isOpen)
        {
            if(AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }
            if(isRotatingDoor)
            {
                AnimationCoroutine = StartCoroutine(DoRotationClose());
            }
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        isOpen = false;
        float time = 0;
        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation,endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
}

using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public bool isOpen = false;

    [SerializeField] private bool isRotatingDoor = true;
    [SerializeField] private float speed = 1f;

    [Header("Rotation Configs")]
    [SerializeField] private float RotationAmount = 90f;
    [SerializeField] private float ForwardDirection = 0f;

    private Vector3 StartRotation;
    private Vector3 Forward;
    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        Forward = transform.forward;
    }

    public void Open(Vector3 userPosition)
    {
        if (isOpen) return;

        if (AnimationCoroutine != null)
            StopCoroutine(AnimationCoroutine);

        if (isRotatingDoor)
        {
            float dot = Vector3.Dot(Forward, (userPosition - transform.position).normalized);
            AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
        }
    }

    private IEnumerator DoRotationOpen(float forwardAmount)
    {
        Quaternion startRot = transform.rotation;
        float startYaw = startRot.eulerAngles.y;

        Quaternion endRot = (forwardAmount >= ForwardDirection)
            ? Quaternion.Euler(0, startYaw - RotationAmount, 0)
            : Quaternion.Euler(0, startYaw + RotationAmount, 0);

        isOpen = true;

        float time = 0f;
        while (time < 1f)
        {
            yield return null;
            time += Time.deltaTime * speed;
            transform.rotation = Quaternion.Slerp(startRot, endRot, time);
        }

        transform.rotation = endRot; // snap exactly at end
    }

    public void Close()
    {
        if (!isOpen) return;

        if (AnimationCoroutine != null)
            StopCoroutine(AnimationCoroutine);

        if (isRotatingDoor)
            AnimationCoroutine = StartCoroutine(DoRotationClose());
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(StartRotation);

        isOpen = false;

        float time = 0f;
        while (time < 1f)
        {
            yield return null;
            time += Time.deltaTime * speed;
            transform.rotation = Quaternion.Slerp(startRot, endRot, time);
        }

        transform.rotation = endRot; // final snap
    }
}
*/
using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public bool isOpen = false;

    [SerializeField] private bool isRotatingDoor = true;
    [SerializeField] private float speed = 2f;

    [Header("Rotation Configs")]
    [SerializeField] private float RotationAmount = 90f;

    private Vector3 StartRotationEuler;
    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotationEuler = transform.rotation.eulerAngles;
    }

    public void Open(Vector3 userPosition)
    {
        if (isOpen) return;

        if (AnimationCoroutine != null)
            StopCoroutine(AnimationCoroutine);

        if (isRotatingDoor)
        {
            // Determine which side the player is on relative to the door's forward
            Vector3 toPlayer = (userPosition - transform.position).normalized;
            float side = Vector3.Dot(transform.right, toPlayer); // right vector dot player

            AnimationCoroutine = StartCoroutine(DoRotationOpen(side));
        }
    }

    private IEnumerator DoRotationOpen(float side)
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot;

        // If side > 0, player is on the right, else left
        if (side >= 0)
            endRot = Quaternion.Euler(StartRotationEuler + new Vector3(0, RotationAmount, 0));
        else
            endRot = Quaternion.Euler(StartRotationEuler + new Vector3(0, -RotationAmount, 0));

        isOpen = true;

        float time = 0f;
        while (time < 1f)
        {
            yield return null;
            time += Time.deltaTime * speed;
            transform.rotation = Quaternion.Slerp(startRot, endRot, time);
        }

        transform.rotation = endRot;
    }

    public void Close()
    {
        if (!isOpen) return;

        if (AnimationCoroutine != null)
            StopCoroutine(AnimationCoroutine);

        if (isRotatingDoor)
            AnimationCoroutine = StartCoroutine(DoRotationClose());
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(StartRotationEuler);

        isOpen = false;

        float time = 0f;
        while (time < 1f)
        {
            yield return null;
            time += Time.deltaTime * speed;
            transform.rotation = Quaternion.Slerp(startRot, endRot, time);
        }

        transform.rotation = endRot;
    }
}


