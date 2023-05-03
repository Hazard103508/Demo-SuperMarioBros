using UnityEngine;

namespace UnityShared.Behaviours.Controllers.Players
{
    public class Platformer2DPlayerController : MonoBehaviour
    {
        private float moveInput;
        private Rigidbody2D rb;
        private PhysicMaterial pm;

        public bool usePhysics;
        [Space]
        [Header("Movement")]
        public float moveSpeed = 10;
        public float acceleration = 3;
        public float decceleration = 3;
        public float velPower = 2;
        [Space]
        public float frictionAmount;
        [Space]
        [Header("Jump")]
        public bool isJump;
        public bool jumpInputRelease;
        [Space]
        public float jumpForce;
        [Range(0, 1)]
        public float jumpCutMultiplier;
        [Space]
        public float jumpCoyoteTime;
        public float jumpBufferTime;
        [Space]
        public float fallGravityMultiplier;
        private float gravityScale;
        [Space]
        [Header("Check")]
        public Transform groundCheckPoint;
        public Vector2 groundCheckSize;
        public LayerMask groundLayer;
        [Space]
        public float lastGroundTime;
        public float lastJumpTime;


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            gravityScale = rb.gravityScale;
        }

        private void Update()
        {
            #region INPUT HANDLER
            moveInput = Input.GetAxis("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.J))
                OnJumpInput();

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.J))
                OnJumpUpInput();
            #endregion

            #region COLLISION CHECKS
            //Ground Check
            if (Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0f, groundLayer) && !isJump) //checks if set box overlaps with ground
                lastGroundTime = jumpCoyoteTime; //if so sets the lastGrounded to coyoteTime

            if (isJump && rb.velocity.y < 0)
                isJump = false;
            #endregion

            #region JUMP
            if (lastGroundTime > 0 && lastJumpTime > 0 && !isJump)
            {
                isJump = true;
                Jump();
            }
            #endregion

            #region TIMERS
            lastGroundTime -= Time.deltaTime;
            lastJumpTime -= Time.deltaTime;
            #endregion

            #region GRAVITY
            if (rb.velocity.y < 0)
                rb.gravityScale = gravityScale * fallGravityMultiplier;
            else
                rb.gravityScale = gravityScale;
            #endregion
        }
        void FixedUpdate()
        {
            PhysicsMovement();

            #region FRICTION
            if (lastGroundTime > 0 && Mathf.Abs(moveInput) < 0.01f)
            {
                float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
                amount *= Mathf.Sign(rb.velocity.x);
                rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
            }
            #endregion

        }
        void PhysicsMovement()
        {
            //rb.velocity = new Vector2(moveInput * moveSpeed * Time.deltaTime, rb.velocity.y);

            // calcular la dirección en la que queremos movernos y con la velocidad deseada.
            float targetSpeed = moveInput * moveSpeed;
            // calcular la diferencia entre la velocidad actual y la velocidad deseada.
            float speedDif = targetSpeed - rb.velocity.x;
            // cambiar aceleracion segun la situacion.
            float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : decceleration;
            // se aplica aceleracion a la velocidad, mientras mas velocidad mas aceleracion.
            // luego se lo multiplica por el Sign para obtener la direccion.
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

            rb.AddForce(Vector2.right * movement);
        }
        void NoPhysicsMovement()
        {
            float targetSpeed = moveInput * moveSpeed;
            float speedDif = targetSpeed - rb.velocity.x;
            float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : decceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

            transform.Translate(movement * Time.deltaTime, 0, 0);
        }

        void Friction()
        {
            if (lastGroundTime > 0 && Mathf.Abs(moveInput) < 0.01f)
            {
                float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), frictionAmount);
                amount *= Mathf.Sign(rb.velocity.x);
                rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
            }
        }

        void Jump()
        {
            lastGroundTime = 0;
            lastJumpTime = 0;
            isJump = true;
            jumpInputRelease = false;
            float force = jumpForce;
            if (rb.velocity.y < 0)
                force -= rb.velocity.y;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        public void OnJumpInput()
        {
            lastJumpTime = jumpBufferTime;
        }

        public void OnJumpUpInput()
        {
            if (rb.velocity.y > 0 && isJump)
                rb.AddForce((1 - jumpCutMultiplier) * rb.velocity.y * Vector2.down, ForceMode2D.Impulse);
            jumpInputRelease = true;
            lastJumpTime = 0;
        }

        private void Reset()
        {

        }
    }
}