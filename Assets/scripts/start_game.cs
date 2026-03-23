using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_game : MonoBehaviour
{
    public void Start_game_func()
    {
        Debug.Log("starting game");
        SceneManager.LoadScene("SampleScene");
    }
}
