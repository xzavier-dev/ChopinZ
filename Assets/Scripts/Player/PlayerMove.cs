using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour {
    public float moveSpeed = 0;
    public float jumpHeight = 0;
    public float gravity = 9.8f;

    private CharacterController _controller = null;

    void Start() {
        _controller = GetComponent<CharacterController>();
        if ( _controller == null ) {
            Debug.LogError( "ERROR: Cannot get character controller" );
            this.enabled = false;
        }
    }

    void Update() {
        float h = Input.GetAxis( "Horizontal" );
        Vector3 dir = Vector3.zero;
        if ( _controller.isGrounded ) {
            dir = new Vector3( h, 0, 0 );
            dir = transform.TransformDirection( dir );
            dir *= moveSpeed;

            if ( Input.GetKeyDown( KeyCode.Space ) ) {
                dir.y = jumpHeight;
            }

        }

        dir.y -= gravity * Time.deltaTime;

        _controller.Move( dir * Time.deltaTime);
    }

}
