using UnityEngine;
using TMPro;

public class ATM : MonoBehaviour
{
    public TextMeshPro scoreText;
    public static int score =0;
    float colSize;
    private Stacking stacking;
    void Awake()
    {
        stacking= GameObject.Find("Stack").GetComponent<Stacking>();
        colSize = GameObject.Find("Stack").GetComponent<BoxCollider>().size.z;
    }
    void Update()
    {
        scoreText.text = score.ToString();
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
            //Deactivate gathered objects when collide with ATM machine.
            int index = stacking.collectedObjects.IndexOf(other.transform);

            for (int i = index; i < stacking.collectedObjects.Count; i++)
            {
                score += ((int)stacking.collectedObjects[i].GetComponent<Collecting>().type);

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