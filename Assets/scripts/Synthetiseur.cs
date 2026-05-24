using UnityEngine;

public class Synthetiseur : MonoBehaviour
{
    public GameObject menu_synthetiseur;
    public GameObject chrono;
    public GameObject energy;
    public GameObject hotbar;
    public GameObject menu;
    public GameObject commande_vaisseau;
    public GameObject commande_vaisseau_menu;

    public void OpenSynthetiseur()
    {
        chrono.SetActive(false);
        energy.SetActive(false);
        hotbar.SetActive(false);
        commande_vaisseau.SetActive(false);
        commande_vaisseau_menu.SetActive(false);
        menu_synthetiseur.SetActive(true);
        menu.SetActive(true);
    }

    public void CloseSynthetiseur()
    {
        chrono.SetActive(true);
        energy.SetActive(true); 
        hotbar.SetActive(true);
        commande_vaisseau.SetActive(true);
        commande_vaisseau_menu.SetActive(true);
        menu_synthetiseur.SetActive(false);
        menu.SetActive(false);

    }

}
