using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
	public float JumpForce = 250f;
	public bool Jump = false;
	public float floatingFactor = 0.5f;
	private float originalFactor;
	public bool Floating = false;
	public Transform groundCheck;
	public float maxHeight = 2f;
	private Rigidbody2D body;
	private RaycastHit2D lastCast;
	private bool grounded;
	// Use this for initialization
	void Awake () {
		body = this.GetComponent<Rigidbody2D>();
		originalFactor = body.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		grounded = lastCast = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		Debug.Log(grounded);

		if(Input.GetButtonDown("Jump") && grounded){
			Jump = true;
		}
		if(Input.GetButton("Jump")) {
			Floating = true;
		} else {
			Floating = false;
		}

		var pos = this.transform.position;
		this.transform.position.Set(pos.x, Mathf.Clamp(pos.y, lastCast.point.y, lastCast.point.y + this.maxHeight), pos.z);
	}

	// all physics code done in the fixed Update
	void FixedUpdate() {
		if(Jump) {
			body.AddForce(new Vector2(0f, JumpForce));
			Jump = false;
		}

		if(Floating) {
			this.body.gravityScale = this.originalFactor * this.floatingFactor;
		}
		else {
			this.body.gravityScale = this.originalFactor;
		}

	}
}
