using UnityEngine;

public class Startgameonclick : MonoBehaviour
{
    
    public void Startgame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
