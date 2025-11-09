using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] private ToolObject toolObject;


    public string GetName()
    {
        return toolObject.Name;
    }
    public string GetDescription()
    {
        return toolObject.Description;
    }
}
