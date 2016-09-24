using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {
	public float JumpForce = 250f;
    public float floatingFactor = 0.4f;
	public float SecondsLeft = 300f;
	public float CollectionBoost = 10f;
	public float NoCollideDelay = 2f;
	public float HurtTimeReduction = 10f;
	public float CollectTimeIncrease = 20f;

    private float originalFactor;
	private Rigidbody2D body;
	private Collider2D bodyCollider;
	private Animator anim;

	private float TotalTime = 0;
	private float TimeTracker = 0;

	private float NoCollideTime = 0;
	private float NoCollectTime = 0;
    private bool Jump = false;
    private bool Floating = false;
    private bool grounded = false;
    private bool hurt = false;
	private bool collect = false;
	private bool isPlaying = false;

	void SetNoCollide() {
		NoCollideTime = Time.time + NoCollideDelay;
	}

	bool CanCollideSomething() {
		return NoCollideTime <= Time.time;
	}

	void SetNoCollectTime() {
		NoCollectTime = Time.time + NoCollideDelay;
	}

	bool CanCollectSomething() {
		return NoCollectTime <= Time.time;
	}

	public void SetTimer() {
		isPlaying = true;
		TimeTracker = Time.time + SecondsLeft;
		TotalTime = Time.time;
	}

	public float GetTimeLeft() {
		float t = TimeTracker - Time.time;
		return t > 0 ? t : 0;
	}

	public float GetTimeElapsed() {
		return Time.time - TotalTime;
	}

	// Use this for initialization
	void Awake () {
		body = this.GetComponent<Rigidbody2D>();
		bodyCollider = this.GetComponent<Collider2D>();
		originalFactor = body.gravityScale;
		anim = this.GetComponent<Animator>();
		TimeTracker = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.IsTouchingLayers(bodyCollider , 1 << LayerMask.NameToLayer("Ground"));
		hurt = Physics2D.IsTouchingLayers(bodyCollider , 1 << LayerMask.NameToLayer("Enemy")) && CanCollideSomething();
		collect = Physics2D.IsTouchingLayers(bodyCollider , 1 << LayerMask.NameToLayer("Collectable")) && CanCollectSomething();

		Floating = (Input.GetButton("Jump") || Input.GetMouseButton(0));

		if(Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0) && grounded) {
			Jump = true;
		}
			
		anim.SetBool("Jump", Floating && !grounded);

		if(isPlaying && GetTimeLeft() == 0) {
			anim.SetBool("Dead", true);
		}
		else {
			anim.SetBool("Dead", false);
		}		
		
		if(hurt)
        {
			Debug.Log("Damaged");
			TimeTracker -= HurtTimeReduction;
			SetNoCollide();
        }

		if(collect) 
		{
			Debug.Log("Collected");
			TimeTracker += CollectTimeIncrease;
			SetNoCollectTime();
		}
	}

	// all physics code done in the fixed Update
	void FixedUpdate() {

		if(Jump) {
			body.AddForce(new Vector2(0f, JumpForce));
			Jump = false;
		}

		if(Floating && !Jump) {
			this.body.gravityScale = this.originalFactor * this.floatingFactor;
		}
		else {
			this.body.gravityScale = this.originalFactor;
		}


    }
}
