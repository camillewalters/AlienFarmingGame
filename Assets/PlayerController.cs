using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction playerMovement;
    Vector2 moveDirection = Vector2.zero;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;

    private void OnEnable()
    {
        playerMovement.Enable();
    }

    private void OnDisable()
    {
        playerMovement.Disable();
    }
    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");//Call Interact on the closest tile instead
        GameManager.Instance.SelectedTile.GetComponent<TileController>().Interact();
    }

    private void Update()
    {
        moveDirection = playerMovement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x *moveSpeed, moveDirection.y *moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collided with" + collision.name);
        GameManager.Instance.SelectedTile = collision.gameObject;
        collision.gameObject.GetComponent<TileController>().MakeLarger();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       // Debug.Log("Exited collsion with" + collision.name);
        collision.gameObject.GetComponent<TileController>().ReturnToNormalSize();
    }

}
