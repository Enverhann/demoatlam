using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stacking : MonoBehaviour
{
    public static Stacking Instance;

    public Material[] materials;

    [SerializeField] private Transform previous;
    [SerializeField] private Transform parent;
    private BoxCollider parentCollider;

    public List<Transform> collectedObjects;
    public List<Transform> activeObjects;

    public Transform Parent { get => parent; set => parent = value; }
    public BoxCollider ParentCollider { get => parentCollider; set => parentCollider = value; }
    public Transform Previous { get => previous; set => previous = value; }

    private void Awake()
    {
        DOTween.SetTweensCapacity(1250, 50);

        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        collectedObjects = new List<Transform>();
        activeObjects = new List<Transform>();
        parentCollider = parent.GetComponent<BoxCollider>();
    }

    private void LateUpdate()
    {
        if (collectedObjects.Count > 0)
        {
            CollectedMovement();

        }

        for (int i = 0; i < collectedObjects.Count; i++)
        {

            if (!collectedObjects[i].gameObject.activeSelf)
            {
                collectedObjects.RemoveAt(i);
            }

        }
    }

    public void CollectedMovement()
    {
        //Object anims when going sideways.
        for (int i = collectedObjects.Count - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                collectedObjects[i].DOMoveX(GameObject.Find("Collector").transform.position.x, 0.1f);
            }
            else
            {
                collectedObjects[i].DOMoveX(collectedObjects[i - 1].transform.position.x, 0.1f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Gold" || other.tag == "Diamond" || other.tag == "Money") 
        {
            //Stack objects to player.
            Transform jumper = other.transform;

            //Anim when object picked up.
            jumper.DOShakeScale(0.5f, Vector3.up * 2f, 20, 50, true).SetEase(Ease.InOutSine);

            other.GetComponent<Collider>().enabled = false;

            other.transform.position = previous.transform.position;
            other.transform.parent = parent;

            other.transform.localPosition += new Vector3(0, 0, previous.localScale.z);
            previous = other.transform;

            parentCollider.size += new Vector3(0, 0, other.transform.localScale.z);
            parentCollider.center += new Vector3(0, 0, other.transform.localScale.z / 2);

            other.GetComponent<Collecting>().isCollected = true;

            collectedObjects.Add(other.transform);

            switch (other.GetComponent<MeshRenderer>().sharedMaterial.name)
            {
                //Point value for money-gold-diamond.
                case "Money":

                    other.GetComponent<Collecting>().type = Collecting.CollectableTypes.Money;
                    break;

                case "Gold":

                    other.GetComponent<Collecting>().type = Collecting.CollectableTypes.Gold;
                    break;

                case "Diamond":

                    other.GetComponent<Collecting>().type = Collecting.CollectableTypes.Diamond;
                    break;
            }
        }
    }
}