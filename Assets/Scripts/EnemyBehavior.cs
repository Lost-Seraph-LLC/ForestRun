using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyBehavior : MonoBehaviour {
    public Vector2 MoveForce = new Vector2(1, 0);
    Rigidbody2D myBody;
    Collider2D myCollider;
    public EnemyPositionType PositionType;

    // Use this for initialization
    void Awake () {
        this.myBody = this.GetComponent<Rigidbody2D>();
        this.myBody.AddForce(MoveForce);
        this.myCollider = this.GetComponent<Collider2D>();
	}

    // Update is called once per frame
    void Update() {
        if (Physics2D.IsTouchingLayers(myCollider, 1 << LayerMask.NameToLayer("DeadZone")))
        {
            Destroy(this.gameObject);
        }
    }
}

public enum EnemyPositionType
{
    FLYING,
    GROUND,
    BOTH
}