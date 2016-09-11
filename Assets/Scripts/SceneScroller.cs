using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SceneScroller : MonoBehaviour {
    public float scrollSpeed;
    private MeshRenderer textureRenderer;
    void Start ()
    {
        textureRenderer = this.GetComponent<MeshRenderer>();
    }

    void Update ()
    {
        // float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        // transform.position = startPosition + Vector3.forward * newPosition;

        float x = Mathf.Repeat(Time.time * this.scrollSpeed, 1);
        Vector2 offset = new Vector2(x, 0);

        textureRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);

    }
}
