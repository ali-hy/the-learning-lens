using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ObjectInfoPanel : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI weightText;
    public TextMeshProUGUI rarityText;
    public TextMeshProUGUI durabilityText;
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI tagsText;
    public Image iconImage;

    [Header("Panel Display Settings")]
    public float panelDisplayDuration = 10f;

    private Coroutine hideCoroutine;

    [Header("Panel Behavior")]
    public float followSpeed = 8f;
    public Vector3 positionOffset = new Vector3(0.5f, 0.3f, 0f);
    public bool facePlayer = true;
    public float fadeSpeed = 5f;

    private Transform parentObject;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Camera playerCamera;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        // Start invisible
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    void Start()
    {
        // Find the main camera (player's head)
        playerCamera = Camera.main;
        if (playerCamera == null)
            playerCamera = FindObjectOfType<Camera>();

        // Set up canvas
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = playerCamera;
        }

        // Get parent object reference
        parentObject = transform.parent;
    }

    void Update()
    {
        if (gameObject.activeInHierarchy && parentObject != null)
        {
            UpdatePanelPosition();

            if (facePlayer && playerCamera != null)
            {
                FacePlayer();
            }
        }
    }

    void UpdatePanelPosition()
    {
        Vector3 targetPosition = parentObject.position + positionOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    void FacePlayer()
    {
        Vector3 directionToPlayer = playerCamera.transform.position - transform.position;
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(-directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, followSpeed * Time.deltaTime);
        }
    }

    public void ShowPanel(ObjectInfo info)
    {
        PopulatePanel(info);
        gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }

    public void HidePanel()
    {
        StartCoroutine(FadeOut());
    }

    void PopulatePanel(ObjectInfo info)
    {
        if (titleText != null)
            titleText.text = info.objectName;

        if (descriptionText != null)
            descriptionText.text = info.description;

        if (weightText != null)
            weightText.text = $"Weight: {info.weight:F1} kg";

        if (rarityText != null)
            rarityText.text = $"Rarity: {info.rarity}";

        if (durabilityText != null)
            durabilityText.text = $"Durability: {info.durability}%";

        if (valueText != null)
            valueText.text = $"Value: {info.value} coins";

        if (tagsText != null && info.tags.Length > 0)
            tagsText.text = $"Tags: {string.Join(", ", info.tags)}";

        if (iconImage != null && info.icon != null)
        {
            iconImage.sprite = info.icon;
            iconImage.gameObject.SetActive(true);
        }
        else if (iconImage != null)
        {
            iconImage.gameObject.SetActive(false);
        }
    }

    System.Collections.IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    System.Collections.IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}