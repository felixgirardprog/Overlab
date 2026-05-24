using UnityEngine;
using TMPro;

public class FinDePartie : MonoBehaviour
{
    public PlayerMovement pepe;
    public SimpleTimer timer;

    public TMP_InputField nomInput;   // ← Référence à l’InputField
    public GameObject panelFin;       // ← Le panneau UI à afficher

    void Start()
    {
        panelFin.SetActive(false);    // Masqué au début
    }

    public void AfficherFin()
    {
        panelFin.SetActive(true);     // On affiche l’UI
        Time.timeScale = 0f;          // Pause du jeu (optionnel)
    }

    public void ValiderNom()
    {
        string nomDuJoueur = nomInput.text;

        SaveSystem save = Object.FindFirstObjectByType<SaveSystem>();

        save.AjouterPartie(
            nomDuJoueur,
            pepe.atomouille,
            pepe.molecouille,
            (int)timer.GetTime()
        );

        save.Sauvegarder();

        Debug.Log("Partie sauvegardée pour : " + nomDuJoueur);

        Time.timeScale = 1f;
        panelFin.SetActive(false);
    }
}