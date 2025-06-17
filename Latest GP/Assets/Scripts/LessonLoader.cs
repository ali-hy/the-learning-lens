using Integration.Client;
using UnityEngine;

public class LessonLoader : MonoBehaviour
{
    public LessonItem lessonItemPrefab;
    public Transform lessonsContainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         StartCoroutine(Api.FetchLessons(PopulateLessons));
    }

    void PopulateLessons(Lesson[] lessons)
    {
        foreach (var lesson in lessons)
        {
            var panel = Instantiate(lessonItemPrefab, lessonsContainer);
            panel.SetLessonData(lesson);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
