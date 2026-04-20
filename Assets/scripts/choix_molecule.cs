using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class choix_molecule : MonoBehaviour
{
    private int atome_min = 2;
    private int atome_max = 20;
    private float difficulty_modifier_max;
    private float difficulty_modifier_min;
    private int current_max;
    private int current_min;
    public string molecule_folder_path = "Molecules/";
    public TMP_Text molecule_formula;
    public TMP_Text molecule_name;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int RandomMoleculeSize(int time)
    {

            difficulty_modifier_max = Mathf.Clamp01(time / 600f);
            difficulty_modifier_min = Mathf.Clamp01(time / 1200f);
            Debug.Log("difficulty_modifier_max: " + difficulty_modifier_max);
            Debug.Log("difficulty_modifier_min: " + difficulty_modifier_min);

        current_max = Mathf.RoundToInt(Mathf.Lerp(atome_min, atome_max, difficulty_modifier_max));
        current_min = Mathf.RoundToInt(Mathf.Lerp(atome_min, atome_max, difficulty_modifier_min));

        return Random.Range(current_min, current_max+1);
    }

    public Molecule MoleculeChoisie(int time)
    {
        Molecule[] molecules = Resources.LoadAll<Molecule>(molecule_folder_path+RandomMoleculeSize(time));
        return molecules[Random.Range(0, molecules.Length)];
    }

    public void AfficherMolecule(Molecule molecule)
    {
        molecule_name.text = molecule.moleculeName;
        molecule_formula.text = molecule.chemicalFormula;
    }

}
