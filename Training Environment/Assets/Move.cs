using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private Rigidbody m_Rigidbody;

	// Use this for initialization
	void Start () {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        m_Rigidbody.velocity = new Vector3(Random.Range(-0.07f, -0.03f), 0f, Random.Range(0.2f, 0.3f));
        
	}
}
