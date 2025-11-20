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


