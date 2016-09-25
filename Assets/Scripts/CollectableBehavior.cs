using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class CollectableBehavior : MonoBehaviour {
	private Animator anim;
	private Collider2D bodyCollider;
	private bool collected = false;
	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();
		bodyCollider = this.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		collected = Physics2D.IsTouchingLayers(bodyCollider , 1 << LayerMask.NameToLayer("Player"));

		if(collected) {
			anim.SetBool("Collected", true);
		}
	}
}
