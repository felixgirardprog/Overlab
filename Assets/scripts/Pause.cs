using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool paused = false;// variable pour savoir si le jeu est en pause ou pas
    public GameObject timer;
    public GameObject player;
    private float currentTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // pause du jeu avec echap
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timer.GetComponent<SimpleTimer>().StopTimer();
            Debug.Log("Game Paused");
            if (!paused)
            {
                timer.GetComponent<SimpleTimer>().pauseTimer();
                player.GetComponent<PlayerMovement>().PauseGame();

                paused = true;
            }
            else
            {
                timer.GetComponent<SimpleTimer>().resumeTimer();
                player.GetComponent<PlayerMovement>().PauseGame();
                paused = false;
            }
        }
    }
}
