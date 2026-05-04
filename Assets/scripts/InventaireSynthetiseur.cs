using UnityEngine;

public class InventaireSynthetiseur : MonoBehaviour
{
    public int Hydrogen;
    public int Azote;
    public int Carbone;
    public int Oxygene;
    public void clear()
    {
        Hydrogen = 0;
        Azote = 0;
        Carbone = 0;
        Oxygene = 0;
    }
}
