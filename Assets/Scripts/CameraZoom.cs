using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Input.InputSystem inputSystem;
    private new Camera camera;
    public float maxZoom, minZoom, zoomStep;

    private void Awake()
    {
        inputSystem = Tools.InputSystem.Instance.Input;
        camera = gameObject.GetComponent<Camera>();
    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }
    private void OnDisable()
    {
        inputSystem.Disable();
    }

    private void Update()
    {
        ZoomScrolling();
    }
    
    private void ZoomScrolling()
    {
        var scroll = inputSystem.Player.Scroll.ReadValue<Vector2>() * zoomStep;

        if (camera.orthographicSize - scroll.y >= maxZoom && camera.orthographicSize - scroll.y <= minZoom)
        {
            camera.orthographicSize -= scroll.y;
        }
    }
}
