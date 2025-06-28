using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Header("Timer Display")]
    public TextMeshProUGUI timerText; // Drag your UI Text component here
    public TextMeshProUGUI timerCurrent;

    [Header("Score Thresholds (in seconds)")]
    [SerializeField] private float beginnerTime = 120f; // 2 minutes
    [SerializeField] private float amateurTime = 60f;   // 1 minute  
    [SerializeField] private float masterTime = 30f;    // 30 seconds

    [Header("Timer Settings")]
    public bool startOnAwake = true;
    public bool countUp = true; // true for count up, false for countdown

    [Header("Score Display")]
    public TextMeshProUGUI score;


    // Public properties that can be accessed by other scripts
    public float CurrentTime { get; private set; }
    public bool IsRunning { get; private set; }

    private float startTime;

    public enum ScoreLevel
    {
        None,
        Beginner,
        Amateur,
        Master
    }

    void Start()
    {
        if (startOnAwake)
        {
            StartTimer();
        }

        UpdateTimerDisplay();
    }

    void Update()
    {
        if (IsRunning)
        {
            if (countUp)
            {
                CurrentTime = Time.time - startTime;
            }
            else
            {
                CurrentTime = startTime - Time.time;
                if (CurrentTime <= 0)
                {
                    CurrentTime = 0;
                    StopTimer();
                }
            }

            UpdateTimerDisplay();
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        CurrentTime = 0;
        IsRunning = true;
    }

    public void StopTimer()
    {
        IsRunning = false;
    }

    public void ResetTimer()
    {
        CurrentTime = 0;
        IsRunning = false;
        UpdateTimerDisplay();
    }

    public void PauseTimer()
    {
        IsRunning = false;
    }

    public void ResumeTimer()
    {
        startTime = Time.time - CurrentTime;
        IsRunning = true;
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(CurrentTime / 60);
            int seconds = Mathf.FloorToInt(CurrentTime % 60);
            int milliseconds = Mathf.FloorToInt((CurrentTime * 100) % 100);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
    }

    // Call this method from other scripts to get the score at that exact moment
    public ScoreLevel GetCurrentScore()
    {
        return GetScoreLevel(CurrentTime);
    }

    public ScoreLevel GetScoreLevel(float time)
    {
        if (time <= masterTime)
            return ScoreLevel.Master;
        else if (time <= amateurTime)
            return ScoreLevel.Amateur;
        else if (time <= beginnerTime)
            return ScoreLevel.Beginner;
        else
            return ScoreLevel.None;
    }

    public string GetScoreLevelString()
    {
        return GetCurrentScore().ToString();
    }

    public void DisplayScore()
    {
           score.text = GetScoreLevelString();
    }

    public float GetTimeForLevel(ScoreLevel level)
    {
        switch (level)
        {
            case ScoreLevel.Beginner: return beginnerTime;
            case ScoreLevel.Amateur: return amateurTime;
            case ScoreLevel.Master: return masterTime;
            default: return 0f;
        }
    }
    public void GetCurrentTime()
    {
        PauseTimer();
        int minutes = Mathf.FloorToInt(CurrentTime / 60);
        int seconds = Mathf.FloorToInt(CurrentTime % 60);
        int milliseconds = Mathf.FloorToInt((CurrentTime * 100) % 100);

        timerCurrent.text = "Play Time: " + string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}