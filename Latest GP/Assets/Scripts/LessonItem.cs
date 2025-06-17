using UnityEngine;

using Integration.Client;
using TMPro;
using UnityEngine.UI;
using System;

public class LessonItem : MonoBehaviour
{
    Lesson LessonData;

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

    // Update is called once per frame
    void Update()
    {

    }
}
