/*using UnityEngine;
using TMPro;

public class PlayerActions : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshPro useText;
    [SerializeField] private TextMeshPro FindKeyText;

    [Header("Raycast Settings")]
    [SerializeField] private Transform Camera;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private LayerMask useLayerMask;

    private PlayerControls inputActions;
    private Door targetedDoor;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Use.performed += ctx => OnUse();
    }
    private void OnDisable()
    {
        inputActions.Player.Use.performed -= ctx => OnUse();
        inputActions.Player.Disable();
    }

    private void OnUse()
    {
        if(!Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, maxDistance, useLayerMask)) return;
        if(!hit.collider.TryGetComponent<Door>(out Door door)) return;

        //key not collected
        if(!KeyPickup.keyCollected)
        {
            return;
        }

        //key is collected -> operator door
        if(door.isOpen) door.Close();
        else door.Open(transform.position);
    }

    private void Update()
    {
        //hide UI by default
        useText.gameObject.SetActive(false);
        FindKeyText.gameObject.SetActive(false);

        if(!Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, maxDistance, useLayerMask)) return;
        if(!hit.collider.TryGetComponent<Door>(out Door door)) return;


        //key not collected - show FindKeyText
        if(!KeyPickup.keyCollected)
        {
            FindKeyText.text = "Find the key to open the door";
            PositionUIText(FindKeyText, hit);
            FindKeyText.gameObject.SetActive(true);
            return;
        }

        //key collected - show Open/Close text
        useText.text = door.isOpen ? "Press E to Close Door" : "Press E to Open Door";

        PositionUIText(useText, hit);
        useText.gameObject.SetActive(true);
    }

    private void PositionUIText(TextMeshPro ui, RaycastHit hit)
    {
        ui.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.01f;
        ui.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
    }
}
*/
using UnityEngine;
using TMPro;

public class PlayerActions : MonoBehaviour
{
    [Header("UI (Screen Space)")]
    [SerializeField] private TextMeshProUGUI useText;
    [SerializeField] private TextMeshProUGUI findKeyText;

    [Header("Raycast Settings")]
    [SerializeField] private Transform cam;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private LayerMask useLayerMask;

    private PlayerControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Use.performed += ctx => OnUse();
    }

    private void OnDisable()
    {
        inputActions.Player.Use.performed -= ctx => OnUse();
        inputActions.Player.Disable();
    }

    private void Update()
    {
        // Hide UI by default
        useText.gameObject.SetActive(false);
        findKeyText.gameObject.SetActive(false);

        // Raycast forward
        if (!Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, maxDistance, useLayerMask))
            return;

        // Check if hitting a door
        if (!hit.collider.TryGetComponent<Door>(out Door door))
            return;

        // If key NOT collected → show find key UI
        if (!KeyPickup.keyCollected)
        {
            findKeyText.text = "Find a key to open door";
            findKeyText.gameObject.SetActive(true);
            return;
        }

        // Key collected → show open/close UI
        useText.text = door.isOpen ? "Press E to Close Door" : "Press E to Open Door";
        useText.gameObject.SetActive(true);
    }

    private void OnUse()
    {
        if (!Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, maxDistance, useLayerMask))
            return;

        if (!hit.collider.TryGetComponent<Door>(out Door door))
            return;

        // Key not collected → ignore interaction
        if (!KeyPickup.keyCollected)
            return;

        // If collected → toggle door
        if (door.isOpen)
            door.Close();
        else
            door.Open(transform.position);
    }
}
