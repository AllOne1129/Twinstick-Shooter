using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public float PlayerSpeed = 4.0f;
    private float CurrentSpeed = 0.0f;
    private Vector3 lastMovement = new Vector3();
    //발사될 레이저
    public Transform Laser;

    //우주선의 중심과 레이저와의 거리
    public float LaserDistance = .2f;

    //다시 발사할 때까지 기다려야 하는 시간(초)
    public float TimeBetweenFires = .3f;

    //값이 0보다 작거나 같으면 다시 발사 가능
    private float timeTilNextFire = 0.0f;

    public List<KeyCode> shootButton;

    public AudioClip ShootSound;
    private AudioSource AudioSource;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update () {
        if (!PauseMenuBehaviour.Paused)
        {
            Rotation();

            Movement();

            foreach (KeyCode element in shootButton)
            {
                if (Input.GetKey(element) && timeTilNextFire < 0)
                {
                    timeTilNextFire = TimeBetweenFires;
                    ShootLaser();
                    break;
                }
            }

            timeTilNextFire -= Time.deltaTime;
        }
	}

    void Rotation()
    {
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);

        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.y - worldPos.y;

        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));

        this.transform.rotation = rot;
    }

    void Movement()
    {
        Vector3 movement = new Vector3();
        movement.x += Input.GetAxis("Horizontal");
        movement.y += Input.GetAxis("Vertical");

        movement.Normalize();

        if(movement.magnitude>0)
        {
            CurrentSpeed = PlayerSpeed;
            this.transform.Translate(movement * Time.deltaTime * PlayerSpeed, Space.World);
            lastMovement = movement;
        }

        else
        {
            this.transform.Translate(lastMovement * Time.deltaTime * CurrentSpeed, Space.World);

            CurrentSpeed *= .9f;
        }

    }

    void ShootLaser()
    {
        AudioSource.PlayOneShot(ShootSound);
        Vector3 LaserPos = this.transform.position;
        float rotationAngle = transform.localEulerAngles.z - 90;
        LaserPos.x += (Mathf.Cos(rotationAngle) * Mathf.Deg2Rad * -LaserDistance);
        LaserPos.y += (Mathf.Sin(rotationAngle) * Mathf.Deg2Rad * -LaserDistance);

        Instantiate(Laser, LaserPos, this.transform.rotation);
    }
}
