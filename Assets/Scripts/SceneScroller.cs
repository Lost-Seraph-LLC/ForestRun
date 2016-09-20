using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SceneScroller : MonoBehaviour {
    public float scrollSpeed;
    public List<Material> interchangableMaterials;
    private MeshRenderer textureRenderer;
    private bool rotated = true; 
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

        // Pattern has repeated...
        if(x < 0.01 && interchangableMaterials.Count > 0 && !rotated)
        {
            int i = Random.Range(0, interchangableMaterials.Count);
            textureRenderer.sharedMaterial = interchangableMaterials[i];
            rotated = true;
        }

        rotated = rotated && x > 0.01;

        textureRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
