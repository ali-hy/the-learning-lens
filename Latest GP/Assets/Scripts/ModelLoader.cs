using UnityEngine;
using UnityEngine.Events;

public class ModelLoader : MonoBehaviour
{
    GameObject ReferenceModel = null;
    GameObject BuildableModel = null;

    EvaluationManager evaluationManager = null;
    
    public UnityEvent ModelsLoaded;

    private void Awake()
    {
        evaluationManager = FindAnyObjectByType<EvaluationManager>();

        ModelsLoaded.AddListener(InstantiateModels);
        ModelsLoaded.AddListener(evaluationManager.OnModelLoaded);
        StartCoroutine(LessonSceneLoader.LessonItem.LoadBuildableModel(SetBuildableModel));
        StartCoroutine(LessonSceneLoader.LessonItem.LoadReferenceModel(SetReferenceModel));
    }

    private void OnDisable()
    {
        ModelsLoaded.RemoveAllListeners();
    }

    private void InstantiateModels ()
    {
        Instantiate(ReferenceModel, transform);
        Instantiate(BuildableModel, transform);
    }

    private void SetBuildableModel (GameObject gameObject)
    {
        BuildableModel = gameObject;
        if (BuildableModel != null & ReferenceModel != null)
        {
            ModelsLoaded.Invoke();
        }
    }

    private void SetReferenceModel (GameObject gameObject)
    {
        ReferenceModel = gameObject;
        if (ReferenceModel != null && BuildableModel != null)
        {
            ModelsLoaded.Invoke();
        }
    }
}
