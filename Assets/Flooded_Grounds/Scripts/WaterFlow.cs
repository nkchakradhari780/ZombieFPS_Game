using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    private const float speed = 0.001f;
    Renderer rend;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * speed;
        rend.material.SetTextureOffset("_BaseMap", new Vector2(offset, 0));
    }
}
