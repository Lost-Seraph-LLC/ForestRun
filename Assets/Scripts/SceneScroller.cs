using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SceneScroller : MonoBehaviour {
    public float scrollSpeed;
    public List<Material> interchangableMaterials;
    private MeshRenderer textureRenderer;
    private bool rotated = true; 
    private int previousIndex = 0;

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
        if(interchangableMaterials.Count > 0 && x < 0.01 && !rotated)
        {
            int i = Random.Range(0, interchangableMaterials.Count);

            while(previousIndex == i) {
                i = Random.Range(0, interchangableMaterials.Count);
            }

            previousIndex = i;

            textureRenderer.sharedMaterial = interchangableMaterials[i];
            rotated = true;
        }

        rotated = rotated && x < 0.1;

        textureRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnApplicationQuit() {
        textureRenderer.sharedMaterial.SetTextureOffset("_MainTex", new Vector2());
    }
}
