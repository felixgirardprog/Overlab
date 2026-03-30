using UnityEngine;

[CreateAssetMenu(fileName = "New Molecule", menuName = "Chemistry/Molecule")]
public class Molecule : ScriptableObject
{
    public string moleculeName;
    public string chemicalFormula;
    [TextArea(3, 6)]
    public string description;

    // Example: store atoms as a simple string or something better later
    public int hydrogen;
    public int azote;
    public int carbone;
    public int oxygene;
}