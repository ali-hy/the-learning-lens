using UnityEngine;

public class InteractableObjectInfo : MonoBehaviour
{
    [Header("Object Information")]
    public ObjectInfo objectInfo = new ObjectInfo();

    public ObjectInfo GetInfo()
    {
        return objectInfo;
    }

    // Optional: Validate data in editor
    void OnValidate()
    {
        if (string.IsNullOrEmpty(objectInfo.objectName))
        {
            objectInfo.objectName = gameObject.name;
        }
    }
}

