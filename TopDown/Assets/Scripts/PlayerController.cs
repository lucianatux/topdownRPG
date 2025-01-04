using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float moveSpeed;
  private bool isMoving;
  private Vector2 input;
  private Animator animator;

  private void Awake()
  {
    animator= GetComponent<Animator>();
  }

  private void Update()
  {
    // If not moving, get input
    // Si no se está moviendo, obtiene la entrada
    if (!isMoving)
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // No diagonal movement
        // Evita el movimiento diagonal
        if (input.x != 0) input.y = 0;

        // If input exists, calculate target position
        // Si hay entrada, calcula la posición objetivo
        if (input != Vector2.zero)
        {
          animator.SetFloat("moveX", input.x);
          animator.SetFloat("moveY", input.y);
          var targetPos = transform.position;
          targetPos.x += input.x;
          targetPos.y += input.y;

          StartCoroutine(Move(targetPos)); // Start moving
          // Inicia el movimiento
        }
    }
    animator.SetBool("isMoving", isMoving);
  }

  IEnumerator Move(Vector3 targetPos)
  {
    isMoving = true; // Start movement
    // Inicia el movimiento

    while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        // Mueve hacia el objetivo
        yield return null;
    }

    transform.position = targetPos; // Snap to target
    // Ajusta a la posición objetivo

    isMoving = false; // End movement
    // Finaliza el movimiento
  }
}
