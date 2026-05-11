using UnityEngine;

public class Moleculeobject : MonoBehaviour
{
    public Molecule moleculeinfo;

    public string GetName() => moleculeinfo.moleculeName;
    public string GetChemicalFormula() => moleculeinfo.chemicalFormula;
    public string GetDescription() => moleculeinfo.description;
}
