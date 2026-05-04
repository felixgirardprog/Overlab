using System.Collections.Generic;
using UnityEngine;

public class SacUI : MonoBehaviour
{
    public ZoneSac zoneSac;

    public GameObject prefabH;
    public GameObject prefabO;
    public GameObject prefabC;
    public GameObject prefabN;

    public RectTransform container;

    private List<GameObject> spawnedAtoms = new List<GameObject>();

    public void Refresh(int H, int N, int C, int O)
    {
        // clean UI
        foreach (GameObject obj in spawnedAtoms)
        {
            Destroy(obj);
        }
        spawnedAtoms.Clear();

        // Prepare atom types and counts
        var atomTypes = new List<(GameObject prefab, int count, string type)>
        {
            (prefabH, H, "H"),
            (prefabN, N, "N"),
            (prefabC, C, "C"),
            (prefabO, O, "O")
        };

        int[] counts = { H, N, C, O };
        int total = H + N + C + O;
        int[] spawned = { 0, 0, 0, 0 };

        // Round-robin spawn
        int spawnedTotal = 0;
        while (spawnedTotal < total)
        {
            for (int i = 0; i < atomTypes.Count; i++)
            {
                if (spawned[i] < counts[i])
                {
                    SpawnAtoms(atomTypes[i].prefab, 1, atomTypes[i].type);
                    spawned[i]++;
                    spawnedTotal++;
                }
            }
        }
    }

    void SpawnAtoms(GameObject prefab, int amount, string type)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(prefab, container);

            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.anchoredPosition = zoneSac.GetRandomPosition();

            Atome atome = obj.GetComponent<Atome>();
            atome.atomeType = type;

            spawnedAtoms.Add(obj);
        }
    }
}