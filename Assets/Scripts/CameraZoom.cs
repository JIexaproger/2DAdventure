using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private InputSystem inputSystem;
    private new Camera camera;
    public int maxZoom, minZoom;

    private void Awake()
    {
        camera = gameObject.GetComponent<Camera>();
        inputSystem = new InputSystem();
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
        var scroll = inputSystem.Player.Scroll.ReadValue<Vector2>();

        if (camera.orthographicSize - scroll.y >= maxZoom && camera.orthographicSize - scroll.y <= minZoom)
        {
            camera.orthographicSize -= scroll.y;
        }
    }
}
