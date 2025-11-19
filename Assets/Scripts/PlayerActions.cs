using UnityEngine;
using TMPro;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private TextMeshPro UseText;
    [SerializeField] private Transform Camera;
    [SerializeField] private float MaxUseDistance = 5f;
    [SerializeField] private LayerMask UseLayerMask;

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
        if(Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayerMask))
        {
            if(hit.collider.TryGetComponent<Door>(out Door door))
            {
                if(door.isOpen)
                {
                    door.Close();
                }
                else
                {
                    door.Open(transform.position);
                }
            }
        }
    }

    private void Update()
    {
        if(Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayerMask)&&
           hit.collider.TryGetComponent<Door>(out Door door))
        {
            if(door.isOpen)
            {
                UseText.text = "Press E to Close Door";
            }
            else
            {
                UseText.text = "Press E to Open Door";
            }
            UseText.gameObject.SetActive(true);
            UseText.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.01f;
            UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
    }
}
