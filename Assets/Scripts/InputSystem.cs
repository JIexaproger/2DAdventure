using UnityEngine;
namespace Tools
{
    public class InputSystem : MonoBehaviour
    {
        public static Tools.InputSystem Instance { get; private set; }
        public Input.InputSystem Input;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitInput();
        }

        private void InitInput() => Input = new Input.InputSystem();
    }
}
