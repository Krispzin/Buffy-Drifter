using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;

    public AudioSource audioSource;

    private float CurrentSpeed = 0;
    public float MaxSpeed = 0;
    public float BoostSpeed = 0;
    private float RealSpeed = 0;

    private float steerDirection;
    private float driftTime;

    bool driftLeft = false;
    bool driftRight = false;
    float outwardsDriftForce = 50000;

    public bool isSliding = false;

    private bool touchingGround;

    public ParticleSystem[] leftDrift;
    public ParticleSystem[] rightDrift;
    public Color drift1;
    public Color drift2;
    public Color drift3;

    public float BoostTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        steer();
        groundNormalRotation();
        drift();
        boost();
    }

    private void move()
    {
        RealSpeed = transform.InverseTransformDirection(rb.velocity).z;

        if (Input.GetKey(KeyCode.W))
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxSpeed, Time.deltaTime * 0.5f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, -MaxSpeed / 1.75f, 1f * Time.deltaTime);
        }
        else
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, Time.deltaTime * 1.5f);
        }

        Vector3 vel = transform.forward * CurrentSpeed;
        vel.y = rb.velocity.y;
        rb.velocity = vel;
    }
    private void steer()
    {
        steerDirection = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        Vector3 steerDirVect;

        float steerAmount;

        if (driftLeft && !driftRight)
        {
            steerDirection = Input.GetAxis("Horizontal") < 0 ? -1.5f : -0.5f;
            transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, Quaternion.Euler(0, -20f, 0), 8f * Time.deltaTime);

            if(isSliding && touchingGround)
                rb.AddForce(transform.right * outwardsDriftForce * Time.deltaTime, ForceMode.Acceleration);
        }
        else if (!driftLeft && driftRight)
        {
            steerDirection = Input.GetAxis("Horizontal") > 0 ? 1.5f : 0.5f;
            transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, Quaternion.Euler(0, 20f, 0), 8f * Time.deltaTime);

            if (isSliding && touchingGround)
                rb.AddForce(transform.right * -outwardsDriftForce * Time.deltaTime, ForceMode.Acceleration);
        }
        else
        {
            transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, Quaternion.Euler(0, 0f, 0), 8f * Time.deltaTime);
        }

        steerAmount = RealSpeed > 30 ? RealSpeed / 4 * steerDirection : steerAmount = RealSpeed / 1.5f * steerDirection;

        steerDirVect = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + steerAmount, transform.eulerAngles.z);

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, steerDirVect, 3 * Time.deltaTime);
    }

    private void groundNormalRotation()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.75f))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up * 2, hit.normal) * transform.rotation, 7.5f * Time.deltaTime);
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }

    }

    private void drift()
    {
        if (Input.GetKeyDown(KeyCode.Space) && touchingGround)
        {
            audioSource.Play();
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hop");
            if (steerDirection > 0)
            {
                driftRight = true;
                driftLeft = false;
            }
            else if (steerDirection < 0)
            {
                driftRight = false;
                driftLeft = true;
            }
        }

        if (Input.GetKey(KeyCode.Space) && touchingGround && CurrentSpeed > 20 && Input.GetAxis("Horizontal") != 0)
        {
            driftTime += Time.deltaTime;
            playPauseSmoke = true;
            for (int i = 0; i < leftDrift.Length; i++)
            {
                leftDrift[i].Play();
                rightDrift[i].Play();
            }
        }
        
        if (!Input.GetKey(KeyCode.Space) || RealSpeed < 20)
        {
            driftLeft = false;
            driftRight = false;
            isSliding = false;

            if (driftTime > 1.5 && driftTime < 5)
            {
                BoostTime = 0.75f;
            }
            if (driftTime > 4 && driftTime < 7)
            {
                BoostTime = 1.5f;
            }
            if (driftTime >= 7)
            {
                BoostTime = 2.5f;
            }

            driftTime = 0;

            for (int i = 0; i < leftDrift.Length; i++)
            {
                leftDrift[i].Stop();
                rightDrift[i].Stop();
            }
            audioSource.Stop();
            playPauseSmoke = false;

        }
    }

    private void boost()
    {
        BoostTime -= Time.deltaTime;
        if(BoostTime > 0)
        {
            MaxSpeed = BoostSpeed;
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxSpeed, 1 * Time.deltaTime);
        }
        else
        {
            MaxSpeed = BoostSpeed - 20;
        }
    }

    [HideInInspector] public bool playPauseSmoke = false;
}
