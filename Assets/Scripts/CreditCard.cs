using System.Collections.Generic;
using UnityEngine;

public class CreditCard : MonoBehaviour
{
	private Stacking stacking;

	public List<Transform> droppedItems;

	float colSize;
	public float flipTime;
	private Vector3 startPos;
	public bool creditCard=false;

	void Awake()
	{
		stacking = GameObject.Find("Stack").GetComponent<Stacking>();
		startPos = transform.position;
	}

	void Update()
	{
		Movement();
	}

	void Movement()
    {
		if(creditCard)
			transform.position = new Vector3(startPos.x - Mathf.PingPong(Time.time*5, flipTime ), transform.position.y, transform.position.z);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Stack")
		{
			for (int i = 0; i < stacking.collectedObjects.Count; i++)
			{
				stacking.collectedObjects[i].GetComponent<BoxCollider>().enabled = true;
			}
		}
		else if (other.tag == "Gold" || other.tag == "Diamond" || other.tag == "Money")
		{
			//Deactivate gathered objects when collide with credit card.
			int index = stacking.collectedObjects.IndexOf(other.transform);

			for (int i = index; i < stacking.collectedObjects.Count; i++)
			{

				stacking.collectedObjects[i].gameObject.SetActive(false);

				if (index - 1 >= 0)
				{
					stacking.Previous = stacking.collectedObjects[index - 1];
				}
				else
				{
					stacking.Previous = GameObject.Find("Collector").transform;
				}

				if (stacking.ParentCollider.size.z > colSize)
				{
					stacking.ParentCollider.size -= new Vector3(0, 0, other.transform.localScale.z);
					stacking.ParentCollider.center -= new Vector3(0, 0, other.transform.localScale.z / 2);
				}
			}
		}
	}
}