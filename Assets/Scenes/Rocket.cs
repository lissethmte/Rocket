using UnityEngine;
using UnityEngine.InputSystem;

public class Rocket : MonoBehaviour
{
    private InputAction dashiAction;
    private InputAction toqueAction;
    private Rigidbody2D rb;

    private bool isDashing = false;
    private float dashTime = 0f;

    
    void Start()
    {
        toqueAction = InputSystem.actions.FindAction("pushito");
        dashiAction = InputSystem.actions.FindAction("dash");
        rb = GetComponent<Rigidbody2D>();
        rb.AddForceX(100.0f);
    }

   
    void Update()
    {
       if (!isDashing)
        {
            toqueAction.performed += context => { rb.AddForceY(500.0f); };
            dashiAction.canceled += context => { rb.AddForceY(-20.0f); };
            if (transform.position.y < -5.0f) { rb.AddForceY(10.0f); }
            if (transform.position.y > 5.0f) { rb.AddForceY(-5.0f); }
            if (rb.linearVelocityY > 12.0f) { rb.linearVelocityY = 0.0f; }

            dashiAction.performed += context =>
            {
                rb.AddForceX(200.0f);
                rb.linearVelocityY = 0f;
                isDashing = true;
                rb.gravityScale = 0f;

            };

            if (isDashing)
            {
                dashTime += Time.deltaTime;
                if(dashTime > 0.25f)
                {
                    isDashing = false;
                    dashTime = 0f;
                    rb.AddForceX(-200.0f);
                    rb.gravityScale = 1.01f;

                }
            }

       }
        
    }

}
