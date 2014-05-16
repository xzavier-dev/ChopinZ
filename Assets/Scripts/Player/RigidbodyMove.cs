using UnityEngine;
using System.Collections;

public enum MoveState_t {
    Grounded,
    JumpUp,
    JumpDown,
}


[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMove : MonoBehaviour {
    public float moveSpeed = 0;
    public float jumpSpeed = 0;
    public float gravity = 15;

    private bool _isGrounded = false;
    private Vector3 velocity = Vector3.zero;
    
    public MoveState_t moveState;
    private float _startJumpDownTime = 0;
    private float _startJumpUpTime = 0;
    private float _lastVelocityX = 0;
    private float _maxMoveSpeedOfAir;


	void Start () {
        ChangeMoveState( MoveState_t.JumpDown );
        _maxMoveSpeedOfAir = moveSpeed / 1.5f;
	}
	

	void Update () {

        float h = Input.GetAxis( "Horizontal" );

        switch(moveState) {
            case MoveState_t.Grounded:
                rigidbody.velocity = new Vector3( h * moveSpeed, 0, 0 );
                break;
            case MoveState_t.JumpUp:
                rigidbody.velocity = new Vector3( Mathf.Clamp(_lastVelocityX,_lastVelocityX,_maxMoveSpeedOfAir), jumpSpeed + gravity * Mathf.Sqrt( Time.time - _startJumpUpTime ), 0 );
                break;
        }

        if ( Input.GetKeyDown( KeyCode.Space ) ) {
            ChangeMoveState( MoveState_t.JumpUp );
        }
	
	}


    void ChangeMoveState(MoveState_t state) {
        switch (state ){
            case MoveState_t.Grounded:

                break;
            case MoveState_t.JumpDown:
                _startJumpDownTime = Time.time;
                break;
            case MoveState_t.JumpUp:
                if( moveState != MoveState_t.Grounded ){
                    return;
                }
                _startJumpUpTime = Time.time;
                _lastVelocityX = rigidbody.velocity.x;
               
                break;
        }
        moveState = state;
    }


    void OnCollisionEnter( Collision obj ) {
        if ( obj.gameObject.CompareTag( "Ground" ) ) {
            ChangeMoveState( MoveState_t.Grounded );
        }
    }

}
