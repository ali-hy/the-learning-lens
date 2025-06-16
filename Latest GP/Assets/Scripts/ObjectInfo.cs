using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[System.Serializable]
public class ObjectInfo
{
    [Header("Basic Information")]
    public string objectName = "";

    [Header("Description")]
    [TextArea(3, 6)]
    public string description = "No description available.";

    [Header("Socket Detection")]
    public bool hideInfoWhenSocketed = true;

    private XRSocketInteractor currentSocket;
    private bool isInSocket = false;

    [Header("Visual")]
    public Sprite icon;

    [Header("Properties")]
    public float weight = 1.0f;
    public string rarity = "Common";

    [Header("Additional Info")]
    public string[] tags = { "Item" };

    [Header("Stats (Optional)")]
    public int durability = 100;
    public int value = 10;
}
