using UnityEngine;

public class CollectableConverter : MonoBehaviour
{
    private Stacking _stacking;

    public Mesh[] mesh;

    private void Awake()
    {
        _stacking = GameObject.Find("Stack").GetComponent<Stacking>();
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
            //Convert money to gold, gold to diamond with changing materials and mesh.
            switch (other.GetComponent<Collecting>().type.ToString())
            {
                case "Money":
;
                    other.GetComponent<MeshFilter>().mesh = mesh[1];
                    other.GetComponent<MeshRenderer>().material = _stacking.materials[1];
                    //other.transform.localScale = new Vector3(2, 2, 2);
                    other.GetComponent<Collecting>().type = Collecting.CollectableTypes.Gold;
                    break;

                case "Gold":

                    other.GetComponent<MeshFilter>().mesh = mesh[2];
                    other.GetComponent<MeshRenderer>().material = _stacking.materials[2];
                    //other.transform.localScale = new Vector3(2, 2, 2);
                    other.GetComponent<Collecting>().type = Collecting.CollectableTypes.Diamond;
                    break;

                case "Diamond":

                    break;
            }
        }
    }
}
