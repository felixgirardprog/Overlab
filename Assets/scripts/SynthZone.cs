using System.Collections.Generic;
using UnityEngine;

public class SynthZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Atome atome = collision.GetComponent<Atome>();
    }
    public List<Atome> atomsInside = new List<Atome>();
    public PlayerMovement playerMovement;
    public AudioManager audioManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Atome atome = collision.GetComponent<Atome>();
        if (atome != null)
        {
            if (!atomsInside.Contains(atome))
            {
                atomsInside.Add(atome);
                switch(atome.atomeType)
                {
                    case "H": playerMovement.RemoveH(); break;
                    case "O": playerMovement.RemoveO(); break;
                    case "C": playerMovement.RemoveC(); break;
                    case "N": playerMovement.RemoveN(); break;
                }
            }
        }
    }

    public void AjoutAtome(Atome atome)
    {
        if (!atomsInside.Contains(atome))
        {
            atomsInside.Add(atome);
            switch (atome.atomeType)
            {
                case "H": playerMovement.RemoveH(); break;
                case "O": playerMovement.RemoveO(); break;
                case "C": playerMovement.RemoveC(); break;
                case "N": playerMovement.RemoveN(); break;
            }
        }
    }

    public void RemoveAtome(Atome atome)
    {
        if (atomsInside.Contains(atome))
        {
            atomsInside.Remove(atome);
            switch (atome.atomeType)
            {
                case "H": playerMovement.AddH(); break;
                case "O": playerMovement.AddO(); break;
                case "C": playerMovement.AddC(); break;
                case "N": playerMovement.AddN(); break;
            }
        }
    }

    public void ClearAtoms()
    {

        foreach (Atome atome in atomsInside)
        {
            atome.Delete();
        }
        atomsInside.Clear();
    }

    public void Delete()
    {
        ClearAtoms();
        audioManager.Play(AudioManager.SoundType.Destroy);
    }
}