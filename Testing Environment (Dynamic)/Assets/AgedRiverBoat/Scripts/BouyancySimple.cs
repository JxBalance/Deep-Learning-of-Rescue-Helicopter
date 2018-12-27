using UnityEngine;
using System.Collections;

public class BouyancySimple : MonoBehaviour {

	public float frequencyMin = 1.0f;
	public float frequencyMax = 2.0f;
	public float magnitude = 0.0025f;
	private float randomInterval;
	
	void Start () {
		randomInterval = Random.Range(frequencyMin, frequencyMax);
	}
	
	void Update () {
		Vector3 pos = transform.position;
		pos.y += (Mathf.Cos(Time.time * randomInterval) * magnitude);
		transform.position = pos;

		//Vector3 angle = transform.eulerAngles;
		//angle.y += (Mathf.Cos(Time.time * randomInterval) * 2);
		//transform.eulerAngles = angle;
	}

}
