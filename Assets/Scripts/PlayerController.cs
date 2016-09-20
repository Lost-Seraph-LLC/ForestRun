using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour {
	public float JumpForce = 250f;
    public float floatingFactor = 0.4f;

    private float originalFactor;
	private Rigidbody2D body;
	private Collider2D bodyCollider;

    private bool Jump = false;
    private bool Floating = false;
    private bool grounded = false;
    private bool hurt = false;

	// Use this for initialization
	void Awake () {
		body = this.GetComponent<Rigidbody2D>();
		bodyCollider = this.GetComponent<Collider2D>();
		originalFactor = body.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {

		if((Input.GetButtonDown("Jump") ||  Input.GetMouseButtonDown(0)) && grounded){
			Jump = true;
		}
		if(Input.GetButton("Jump") || Input.GetMouseButton(0)) {
			Floating = true;
		} else {
			Floating = false;
		}
	}

	// all physics code done in the fixed Update
	void FixedUpdate() {
		grounded = Physics2D.IsTouchingLayers(bodyCollider , 1 << LayerMask.NameToLayer("Ground"));
		hurt = Physics2D.IsTouchingLayers(bodyCollider , 1 << LayerMask.NameToLayer("Enemy"));
		
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

		if(hurt)
        {
            Debug.Log(hurt);
        }

    }
}
