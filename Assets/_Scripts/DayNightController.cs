using UnityEngine;

public class DayNightController : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 1f;
    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * _scrollSpeed/200;
        _renderer.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
