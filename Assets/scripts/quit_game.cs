using UnityEngine;
using UnityEngine.SceneManagement;

public class quit_game : MonoBehaviour
{
    public void Quitter()
    {
        Debug.Log("quitting game");
        SceneManager.LoadScene("Menu");
    }
}
