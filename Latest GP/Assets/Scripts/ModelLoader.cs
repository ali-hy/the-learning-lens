using UnityEngine;
using UnityEngine.Events;

public class ModelLoader : MonoBehaviour
{
    GameObject referenceModel = null;
    GameObject buildableModel = null;

    EvaluationManager evaluationManager = null;
    
    public UnityEvent modelsLoaded;

    private void Awake()
    {
        evaluationManager = FindAnyObjectByType<EvaluationManager>();

        modelsLoaded.AddListener(InstantiateModels);
        modelsLoaded.AddListener(evaluationManager.OnModelLoaded);
        StartCoroutine(LessonSceneLoader.LessonItem.LoadBuildableModel(SetBuildableModel));
        StartCoroutine(LessonSceneLoader.LessonItem.LoadReferenceModel(SetReferenceModel));
    }

    private void OnDisable()
    {
        modelsLoaded.RemoveAllListeners();
    }

    private void InstantiateModels ()
    {
        Instantiate(referenceModel, transform);
        Instantiate(buildableModel, transform);
    }

    private void SetBuildableModel (GameObject gameObject)
    {
        buildableModel = gameObject;
        if (buildableModel != null & referenceModel != null)
        {
            modelsLoaded.Invoke();
        }
    }

    private void SetReferenceModel (GameObject gameObject)
    {
        referenceModel = gameObject;
        if (referenceModel != null && buildableModel != null)
        {
            modelsLoaded.Invoke();
        }
    }
}
