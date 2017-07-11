using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    public int startingAmmunition = 100;

    private float speed = 150f;
    public float damage = 30;
    public float fireRate = 0.1f;

    public GameObject originator;
    public PlayerController playerControllerScript;

    public Ray ray;
    private float range = 5;
    public RaycastHit hit;

    public GameObject laserHit;
    public ParticleSystem laserHitParticleSystem;

    public GameObject rayEmitter;

    public Rigidbody myRigidbody;

    public Material glowMaterial;

    public int playerNumber;
    
    
    // Use this for initialization
    void Start () {

        laserHitParticleSystem = laserHit.GetComponent<ParticleSystem>();
        laserHitParticleSystem.startColor = glowMaterial.color;


        myRigidbody.AddForce(transform.up * speed, ForceMode.VelocityChange);

	}

    void Update() {

    }



    // Update is called once per frame
    void FixedUpdate () {


        

    }

    void OnCollisionEnter( Collision collision ) {

        bool hitEnemy = false;

        if (playerNumber == 1) {
            if (collision.gameObject.tag == "Player 2" || collision.gameObject.tag == "Player 4" || collision.gameObject.tag == "Player 3")
            {
                hitEnemy = true;
            }
            else {
                hitEnemy = false;
            }
        }
        if (playerNumber == 2)
        {
            if (collision.gameObject.tag == "Player 1" || collision.gameObject.tag == "Player 4" || collision.gameObject.tag == "Player 3")
            {
                hitEnemy = true;
            }
            else
            {
                hitEnemy = false;
            }
        }
        if (playerNumber == 3)
        {
            if (collision.gameObject.tag == "Player 1" || collision.gameObject.tag == "Player 2" || collision.gameObject.tag == "Player 4")
            {
                hitEnemy = true;
            }
            else
            {
                hitEnemy = false;
            }
        }
        if (playerNumber == 4)
        {
            if (collision.gameObject.tag == "Player 1" || collision.gameObject.tag == "Player 2" || collision.gameObject.tag == "Player 3")
            {
                hitEnemy = true;
            }
            else
            {
                hitEnemy = false;
            }
        }


        if (hitEnemy == true) {

            PlayerController playerControllerScript = collision.gameObject.GetComponent<PlayerController>();
            playerControllerScript.health -= damage;

            Instantiate(laserHit, collision.contacts[0].point, Quaternion.identity);
            Destroy(gameObject);

        }

        if (collision.gameObject.tag == "Level") {

            Instantiate(laserHit, collision.contacts[0].point, Quaternion.identity);
            Destroy(gameObject);

        }

    }
}
