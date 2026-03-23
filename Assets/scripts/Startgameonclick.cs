using UnityEngine;

public class Startgameonclick : MonoBehaviour
{
    
    public void Startgame()
    {
        Debug.Log("starting game");
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
