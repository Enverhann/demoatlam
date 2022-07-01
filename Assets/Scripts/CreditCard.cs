using System.Collections.Generic;
using UnityEngine;

public class CreditCard : MonoBehaviour
{
	private Stacking _stacking;

	public List<Transform> droppedItems;

	float colSize;
	public float flipTime;
	private Vector3 _startPos;
	public bool creditCard=false;

	void Awake()
	{
		_stacking = GameObject.Find("Stack").GetComponent<Stacking>();
		_startPos = transform.position;
	}

	void Update()
	{
		Movement();
	}

	void Movement()
    {
		if(creditCard)
			transform.position = new Vector3(_startPos.x - Mathf.PingPong(Time.time*5, flipTime ), transform.position.y, transform.position.z);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Stack")
		{
			for (int i = 0; i < _stacking.collectedObjects.Count; i++)
			{
				_stacking.collectedObjects[i].GetComponent<BoxCollider>().enabled = true;
			}
		}
		else if (other.tag == "Gold" || other.tag == "Diamond" || other.tag == "Money")
		{
			//Deactivate gathered objects when collide with credit card.
			int index = _stacking.collectedObjects.IndexOf(other.transform);

			for (int i = index; i < _stacking.collectedObjects.Count; i++)
			{

				_stacking.collectedObjects[i].gameObject.SetActive(false);

				if (index - 1 >= 0)
				{
					_stacking.Previous = _stacking.collectedObjects[index - 1];
				}
				else
				{
					_stacking.Previous = GameObject.Find("Collector").transform;
				}

				if (_stacking.ParentCollider.size.z > colSize)
				{
					_stacking.ParentCollider.size -= new Vector3(0, 0, other.transform.localScale.z);
					_stacking.ParentCollider.center -= new Vector3(0, 0, other.transform.localScale.z / 2);
				}
			}
		}
	}
}