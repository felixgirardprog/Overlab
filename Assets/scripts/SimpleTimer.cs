using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class SimpleTimer : MonoBehaviour
{
    public bool playing;
    public UnityEngine.UI.Text uiText;
    bool warned = false;
    private float startedPauseTime;
    private float startTime;

    void Awake()
    {
        if (uiText == null)
            uiText = GetComponent<Text>();
        if (uiText == null)
            uiText = GetComponentInChildren<Text>();
    }

    void Start()
    {
        playing = true;
        startTime = Time.time;
    }
  
    void Update ()
    {
        if (playing)
        {
            float gameTime = Time.time - startTime;
            int minutes = (int)(gameTime / 60f);
            int seconds = (int)(gameTime % 60f);
            int tenths = (int)(gameTime * 10f) % 10;

            if (uiText != null)
                if (minutes == 0)
                    uiText.text = string.Format("{0:#0}:{1}", seconds, tenths);
                else
                    uiText.text = string.Format("{0:#:}{1:00}:{2}", minutes, seconds, tenths);
            else if (!warned)
            {
                Debug.LogWarning("SimpleTimer: no UI Text assigned or found on the GameObject.", this);
                warned = true;
            }
        }
    }

    public void StopTimer()
    {
        playing = false;
    }

    public void pauseTimer()
    {
        playing = false;
        startedPauseTime = Time.time;
    }

    public void resumeTimer()
    {
        playing = true;
        startTime += Time.time - startedPauseTime; // Ajuste le temps de départ pour compenser la pause
    }


    public float GetTime()
    {
        return Time.time - startTime;
    }
    

}