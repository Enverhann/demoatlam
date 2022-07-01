using UnityEngine;

public class Pendulum : MonoBehaviour
{
	public float speed = 1.5f;
	public float limit = 75f; //Limit in degrees of the movement
	public bool randomStart = false; 
	private float _random = 0;

	void Awake()
	{
		if (randomStart)
			_random = Random.Range(0f, 1f);
	}
	
	void Update()
	{
		//Pendulum obstacle swing sideways at 75* angle.
		float angle = limit * Mathf.Sin(Time.time + _random * speed);
		transform.localRotation = Quaternion.Euler(0, 0, angle);
	}
}
