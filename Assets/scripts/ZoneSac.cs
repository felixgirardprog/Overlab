using UnityEngine;

public class ZoneSac : MonoBehaviour
{
    public Vector2 size;

    public Vector2 GetRandomPosition()
    {
        float x = Random.Range(-size.x / 2f, size.x / 2f);
        float y = Random.Range(-size.y / 2f, size.y / 2f);

        return new Vector2(x, y);
    }
}