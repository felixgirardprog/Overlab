using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool paused = false;
    public GameObject timer;
    public GameObject player;
    public GameObject monImage; // Assurez-vous de glisser l'UI Image ici dans l'inspecteur Unity
    public GameObject menupause;
    private Animator animateurmenu;
    public GameObject reprendrebutton;
    public Animator volume;
    public GameObject slider;

    void Start()
    {
        // On s'assure que l'image est cachée au lancement du jeu
        if (monImage != null)
            monImage.SetActive(false);
        animateurmenu = menupause.GetComponent<Animator>();
        paused = false; // Assurez-vous que le jeu commence en mode non-pausé
    }
    public void TogglePause()
    {
        paused = !paused; // Alterne entre true et false

        // Gestion de l'image (Menu de pause)
        if (monImage != null)
        {
            monImage.SetActive(paused);
        }

        // Gestion des autres composants
        if (paused)
        {
            ActivateSlider();
            timer.GetComponent<SimpleTimer>().pauseTimer();
            player.GetComponent<PlayerMovement>().PauseGame();
            animateurmenu.SetBool("Pause", true); // Assurez-vous d'avoir un trigger "Pause" dans votre Animator pour le menu de pause
            Time.timeScale = 0f; // Optionnel : fige physiquement le temps dans Unity
            
        }
        else
        {
            timer.GetComponent<SimpleTimer>().resumeTimer();
            player.GetComponent<PlayerMovement>().PauseGame();
            animateurmenu.SetBool("Pause", false);
            Time.timeScale = 1f; // Relance le temps
            DeactivateSlider();
        }
    }
    public void DeactivateSlider()
    {
        slider.SetActive(false);
    }

    public void ActivateSlider()
    {
        slider.SetActive(true);
    }

    public void Reprendre()
    {
        paused = false;
        if (monImage != null)
        {
            monImage.SetActive(false);
        }
        timer.GetComponent<SimpleTimer>().resumeTimer();
        player.GetComponent<PlayerMovement>().PauseGame();
        animateurmenu.SetBool("Pause", false);
        Time.timeScale = 1f; // Relance le temps
    }
}
    
