using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public int health = 2;
    public Transform Explosion;
    public AudioClip HitSound;
    

    void OnCollisionEnter2D(Collision2D theCollision)
    {
       // Debug.Log("Hit" + theCollision.gameObject.name);

        if (theCollision.gameObject.name.Contains("Laser"))
        {
            LaserBehaviour Laser
                = theCollision.gameObject.GetComponent("LaserBehaviour")
                as LaserBehaviour;
            health -= Laser.damage;
            Destroy(theCollision.gameObject);

            GetComponent<AudioSource>().PlayOneShot(HitSound);
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
            GameController Controller =
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            Controller.KilledEnemy();
            Controller.IncreaseScore(10);

            if(Explosion)
            {
                GameObject Exploder = 
                    ((Transform)Instantiate(Explosion, this.transform.position, this.transform.rotation)).gameObject;
                Destroy(Exploder, 2.0f);
            }
        }
    }
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
