using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour {
    private Transform Player;
    public float Speed = 2.0f;
	// Use this for initialization
	void Start () {
        Player = GameObject.Find("playerShip").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 Delta = Player.position - transform.position;
        Delta.Normalize();
        float MoveSpeed = Speed * Time.deltaTime;
        transform.position = transform.position + (Delta * MoveSpeed);
	}
}
