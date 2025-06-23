using UnityEngine;

using Integration.Client;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LessonItem : MonoBehaviour
{
    Lesson LessonData;

    [SerializeField]
    private string BuildableModelAddress = string.Empty;
    [SerializeField]
    private string ReferenceModelAddress = string.Empty;

    // Handle asset loading and instantiation
    public GameObject ReferenceModel = null;
    public GameObject BuildableModel = null;

    // Components
    TextMeshProUGUI TmpTitle;
    TextMeshProUGUI TmpDescription;
    Image Preview;
    Image DifficultyIndicator;

    // Sprites
    Sprite[] DifficultySprites;

    void Awake()
    {
        // Initialize all of the components
        TmpTitle = transform.Find("Lesson Title").GetComponent<TextMeshProUGUI>();
        TmpDescription = transform.Find("Lesson Description").GetComponent<TextMeshProUGUI>();
        Preview = transform.Find("Preview").GetComponent<Image>();
        DifficultyIndicator = transform.Find("Difficulty Indicator").GetComponent<Image>();

        DifficultySprites = Resources.LoadAll<Sprite>("Stars");
        Array.Reverse(DifficultySprites);
    }

    private void Start()
    {
        var menuManager = FindAnyObjectByType<MenuManager>();
        GetComponent<Button>().onClick.AddListener(() =>
        {
            menuManager.SwitchToTrainingScene(this);
        });      
    }

    public void SetLessonData(Lesson lesson)
    {
        LessonData = lesson;

        TmpTitle.SetText(lesson.Title);
        TmpDescription.SetText(lesson.Description);

        if (lesson.Difficulty < DifficultySprites.Length)
            DifficultyIndicator.sprite = DifficultySprites[lesson.Difficulty];

        Sprite lessonPreview = Resources.Load<Sprite>(lesson.Preview);
        if (lessonPreview != null)
            Preview.sprite = lessonPreview;
    }

    public IEnumerator LoadBuildableModel(Action<GameObject> onComplete = null)
    {
        var modelLoader = LoadAddressable<GameObject>(BuildableModelAddress, onComplete);
        yield return modelLoader;
    }

    public IEnumerator LoadReferenceModel(Action<GameObject> onComplete = null)
    {
        var modelLoader = LoadAddressable<GameObject>(ReferenceModelAddress, onComplete);
        yield return modelLoader;
    }

    private IEnumerator LoadAddressable<T>(string address, Action<T> onComplete = null)
    {
        if (string.IsNullOrEmpty(address))
        {
            Debug.LogError("Addressable asset address is not set.");
            yield break;
        }

        var loadedAsset = Addressables.LoadAssetAsync<T>(address);
        yield return loadedAsset;

        if (loadedAsset.Status == AsyncOperationStatus.Succeeded)
        {
            T loadedObject = loadedAsset.Result;
            onComplete?.Invoke(loadedObject);
        }
        else
        {
            Debug.LogError($"Failed to load addressable asset from address: {address}");
        }
    }
}
