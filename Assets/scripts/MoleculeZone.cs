using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeZone : MonoBehaviour
{
    private Transform rectransform;
    public GameObject moleculePrefab;
    public Transform parentobject;
    public List<GameObject> moleculeCreer = new List<GameObject>();

    public void SpawnMolecul(Molecule properties)
    {
        if (rectransform == null)
        {
            rectransform = transform;
        }

        if (properties != null)
        {
            GameObject moleculeInstance = Instantiate(moleculePrefab, rectransform.position, Quaternion.identity, parentobject);
            moleculeInstance.transform.localScale = new Vector3(2f, 2f, 2f);
            moleculeInstance.name = properties.moleculeName;

            Moleculeobject moleculeObject = moleculeInstance.GetComponent<Moleculeobject>();

            moleculeObject.moleculeinfo = properties;

            moleculeCreer.Add(moleculeObject.gameObject);
        }
    }

    public void RemoveMolecule(Molecule molecule)
    {
        Debug.Log("searching molecule to destroy");
        foreach (GameObject moleculeInstance in moleculeCreer)
        {
            string nommolecule = moleculeInstance.GetComponent<Moleculeobject>().moleculeinfo.moleculeName;
            Debug.Log("Check if "+ nommolecule + " = "+molecule.moleculeName);
            Moleculeobject moleculeObject = moleculeInstance.GetComponent<Moleculeobject>();
            if (moleculeObject != null && moleculeObject.moleculeinfo == molecule)
            {
                Debug.Log("Destroying molecule: " + molecule.moleculeName);
                Destroy(moleculeInstance);
                moleculeCreer.Remove(moleculeInstance);
                break;
            }
        }
        Debug.Log("Molecule not found: " + molecule.moleculeName);
    }
}