using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    public float speed = 10;
    public float jumpForce = 10;
    public float mouseSensitivity = 2;
    public Transform cam;
    public GameObject laser;
    [Range(0.1f,0.01f)]
    public float fireDelay = 0.1f;
    private float lastFireTime = 0f;
    
    private Rigidbody rb;
    private bool canJump = true;
    
    private bool dead = false;

    private AudioSource laserSound;
    
    // Start is called before the first frame update
    void Start()
    {
        laserSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }
    
    void OnApplicationFocus(bool hasFocus)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!dead)
        {
            lastFireTime += Time.deltaTime;
            if (Input.GetButton("Fire1") && lastFireTime > fireDelay)
            {
                laserSound.Play();
                lastFireTime = 0;
                Instantiate(laser, cam.position + Vector3.down * 0.2f + cam.forward * 0.5f, cam.rotation);
            }
            var mouseMovement = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            var angles = cam.eulerAngles;
            angles += mouseMovement * mouseSensitivity;
            if (angles.x > 90 && angles.x < 180)
            {
                angles.x = 90;
            }
            if (angles.x > 180 && angles.x < 270)
            {
                angles.x = 270;
            }
            cam.eulerAngles = angles;
            
            if (Input.GetKey(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            RaycastHit infos;
            bool touchedGound = Physics.SphereCast(transform.position, 0.05f, -transform.up, out infos, 0.1f);

            if (touchedGound)
            {
                canJump = true;
            }

            if (canJump && Input.GetButton("Jump"))
            {
                var v = rb.velocity;
                v.y = 0;
                rb.velocity = v;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canJump = false;
            }

            Vector3 movement = Vector3.zero;
            var forward = cam.forward;
            forward.y = 0;
            forward = forward.normalized;
            movement += forward * speed * Input.GetAxis("Vertical");
            movement += cam.right * speed * Input.GetAxis("Horizontal");
            movement.y = rb.velocity.y;
            rb.velocity = movement;
        }
    }

    public void Die()
    {
        if (!dead)
        {
            dead = true;
            
            cam.position = cam.position - cam.up;
            
            var angles = cam.eulerAngles;
            angles.z += -90;
            cam.eulerAngles = angles;
        }
    }
}
