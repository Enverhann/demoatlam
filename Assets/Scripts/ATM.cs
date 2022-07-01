using UnityEngine;
using TMPro;

public class ATM : MonoBehaviour
{
    public TextMeshPro scoreText;
    public static int score =0;
    float colSize;
    private Stacking _stacking;
    void Awake()
    {
        _stacking= GameObject.Find("Stack").GetComponent<Stacking>();
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
            for (int i = 0; i < _stacking.collectedObjects.Count; i++)
            {
                _stacking.collectedObjects[i].GetComponent<BoxCollider>().enabled = true;
            }
        }
        else if (other.tag == "Gold" || other.tag == "Diamond" || other.tag == "Money")
        {
            //Deactivate gathered objects when collide with ATM machine.
            int index = _stacking.collectedObjects.IndexOf(other.transform);

            for (int i = index; i < _stacking.collectedObjects.Count; i++)
            {
                score += ((int)_stacking.collectedObjects[i].GetComponent<Collecting>().type);

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