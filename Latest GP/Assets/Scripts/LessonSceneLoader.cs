using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LessonSceneLoader
{
    public static LessonItem LessonItem = null;

    public static void LoadLessonScene(LessonItem lessonItem, Action LoadScene)
    {
        LessonItem = lessonItem;
        Debug.Log("Loading Lesson Scene...");
        LoadScene?.Invoke();
    }
}
