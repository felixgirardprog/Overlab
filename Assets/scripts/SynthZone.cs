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
            else
            {
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Atome atome = collision.GetComponent<Atome>();

        if (atome != null)
        {
            atomsInside.Remove(atome);
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
}