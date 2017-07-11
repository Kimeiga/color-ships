using UnityEngine;
using System.Collections;

public class PlayerController: MonoBehaviour {

    public bool canMove = true;
    public bool canShoot = true;
    public float turnSpeed = 4;
    public float moveSpeed = 20;

    public float health;
    public float startingHealth;
    public GameObject playerExplosion;
    public GameObject[] bodyParts;
    public bool deathEffects;

    public Collider mainCollider;

    public Material bodyMaterial;
    public Material glowMaterial;

    public Rigidbody myRigidbody;
    public float maxVelocityChange = 10;

    public GameObject laser1;
    public GameObject laser2;
    public GameObject laser3;
    public GameObject laser4;

    public Laser laserReferenceScript;
    public Laser laserScript;
    public Transform laserEmitter;

    public bool shoot;
    public float fireRate = 0.1f;
    private float nextFire;
    public bool useScrollWheel;

    public string weaponSelected;
    

    public int ammunition;
    


    public int playerNumber;

    private Vector3 targetVelocity;
    private Vector3 velocity;
    private Vector3 velocityChange;
    private Vector3 targetAngularVelocity;
    private Vector3 angularVelocity;
    private Vector3 angularVelocityChange;

    void Awake() {
        myRigidbody.useGravity = false;
    }

	// Use this for initialization
	void Start () {

        mainCollider = GetComponent<Collider>();

        deathEffects = true;
        canMove = true;
        canShoot = true;

        laserReferenceScript = laser1.GetComponent<Laser>();

        health = startingHealth;
        nextFire = 0;

        ammunition = laserReferenceScript.startingAmmunition;
        weaponSelected = "laser";

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (canMove) {
            //Thrust Mechanic

            if (playerNumber == 1)
            {
                targetVelocity = new Vector3(0, 0, Input.GetAxis("P1 Thrust"));

            }
            if (playerNumber == 2)
            {
                targetVelocity = new Vector3(0, 0, Input.GetAxis("P2 Thrust"));
            }
            if (playerNumber == 3)
            {
                targetVelocity = new Vector3(0, 0, Input.GetAxis("P3 Thrust"));
            }
            if (playerNumber == 4)
            {
                targetVelocity = new Vector3(0, 0, Input.GetAxis("P4 Thrust"));


            }

            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= moveSpeed;

            velocity = myRigidbody.velocity;
            velocityChange = (targetVelocity - velocity);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

            myRigidbody.AddForce(velocityChange, ForceMode.VelocityChange);


            //Rotation Mechanic

            if (playerNumber == 1)
            {

                targetAngularVelocity = new Vector3(0, Input.GetAxis("P1 Turn"), 0);
            }
            if (playerNumber == 2)
            {

                targetAngularVelocity = new Vector3(0, Input.GetAxis("P2 Turn"), 0);

            }
            if (playerNumber == 3)
            {

                targetAngularVelocity = new Vector3(0, Input.GetAxis("P3 Turn"), 0);
            }
            if (playerNumber == 4)
            {
                targetAngularVelocity = new Vector3(0, Input.GetAxis("P4 Turn"), 0);
            }

            targetAngularVelocity *= turnSpeed;

            angularVelocity = myRigidbody.angularVelocity;
            angularVelocityChange = (targetAngularVelocity - angularVelocity);

            myRigidbody.AddTorque(angularVelocityChange, ForceMode.VelocityChange);



        }



    }

    void Update() {

        if (canShoot) {


            //Firing Mechanic

            if (playerNumber == 1)
            {

                shoot = Input.GetButtonDown("P1 Fire");
            }
            if (playerNumber == 2)
            {
                shoot = Input.GetButtonDown("P2 Fire");
            }
            if (playerNumber == 3)
            {
                shoot = Input.GetButtonDown("P34 Fire");
            }
            if (playerNumber == 4)
            {
                if (useScrollWheel)
                {

                    shoot = Input.GetAxis("Mouse ScrollWheel") < 0f ? true : false;
                }
                else
                {
                    
                    shoot = Input.GetButtonDown("P4 Fire");
                }
                
            }

            if (shoot && nextFire < Time.time && ammunition > 0)
            {

                if (weaponSelected == "laser")
                {
                    fireRate = laserReferenceScript.fireRate;
                }

                if (playerNumber == 1)
                {
                    Instantiate(laser1, laserEmitter.position, laserEmitter.rotation);
                    //laserScript = laser1.GetComponent<Laser>();
                    //laserScript.originator = gameObject;

                }
                if (playerNumber == 2)
                {
                    Instantiate(laser2, laserEmitter.position, laserEmitter.rotation);
                    //laserScript = laser2.GetComponent<Laser>();
                    //laserScript.originator = gameObject;
                }
                if (playerNumber == 3)
                {
                    Instantiate(laser3, laserEmitter.position, laserEmitter.rotation);
                    //laserScript = laser3.GetComponent<Laser>();
                    //laserScript.originator = gameObject;
                }
                if (playerNumber == 4)
                {
                    Instantiate(laser4, laserEmitter.position, laserEmitter.rotation);
                    //laserScript = laser4.GetComponent<Laser>();
                    //laserScript.originator = gameObject;
                }

                nextFire = fireRate + Time.time;
                ammunition--;
            }
        }

        if (health <= 0) {
            if (deathEffects) {
                Instantiate(playerExplosion, transform.position, Quaternion.identity);
                ParticleSystem explosionParticleSystem = playerExplosion.GetComponent<ParticleSystem>();
                explosionParticleSystem.startColor = bodyMaterial.color;

                canMove = false;
                canShoot = false;

                mainCollider.enabled = false;

                foreach (GameObject piece in bodyParts) {

                    Rigidbody rb = piece.GetComponent<Rigidbody>();

                    rb.isKinematic = false;
                    rb.useGravity = true;

                    Collider cl = piece.GetComponent<Collider>();

                    cl.isTrigger = false;

                    

                }


                deathEffects = false;
            }
            
        }
    }
}
