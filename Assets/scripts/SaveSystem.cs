using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class Partie
{
    public int atome;
    public int molecule;
    public int temps;
}

[System.Serializable]
public class Joueur
{
    public string nom;
    public List<Partie> parties = new List<Partie>();
}

[System.Serializable]
public class JoueurList
{
    public List<Joueur> joueurs = new List<Joueur>();
}

public class SaveSystem : MonoBehaviour
{
    [HideInInspector]
    public JoueurList data = new JoueurList();

    private string path;

    void Awake()
    {
        path = Application.persistentDataPath + "/stats.json";
        Charger();
    }

    public void AjouterPartie(string nomJoueur, int atome, int molecule, int temps)
    {
        Joueur joueur = data.joueurs.Find(j => j.nom == nomJoueur);

        if (joueur == null)
        {
            joueur = new Joueur();
            joueur.nom = nomJoueur;
            data.joueurs.Add(joueur);
        }

        Partie p = new Partie
        {
            atome = atome,
            molecule = molecule,
            temps = temps
        };

        joueur.parties.Add(p);

        Sauvegarder();
    }

    public void Sauvegarder()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Sauvegarde effectuée : " + path);
    }

    public void Charger()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<JoueurList>(json);
            Debug.Log("Données chargées");
        }
        else
        {
            Debug.Log("Aucun fichier trouvé, création d'un nouveau");
            data = new JoueurList();
        }
    }
}
