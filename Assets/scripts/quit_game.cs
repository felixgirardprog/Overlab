using UnityEngine;
using UnityEngine.SceneManagement;

public class quit_game : MonoBehaviour
{
    public void Quitter()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f; // Assurez-vous de remettre l'échelle du temps à 1f lors de la quitter le jeu
        
    }
}
