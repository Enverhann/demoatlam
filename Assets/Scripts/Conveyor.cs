using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Conveyor : MonoBehaviour
{
    [SerializeField] private GameObject materialHolder;

    private float speed = 0.1f;
    private new Renderer renderer;
    private float offset;
    public static int score;
    private PlayerEffectAnimations _player;
    public Button nextLevelButton;
    private Stacking stacking;
    void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerEffectAnimations>();
        stacking = GameObject.Find("Stack").GetComponent<Stacking>();
        renderer = materialHolder.GetComponent<Renderer>();
    }

    void Update()
    {
        score = ATM.score;
    }

    public void MaterialOffset()
    {
        offset = speed * Time.time;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stack")
        {
            for (int i = 0; i < stacking.collectedObjects.Count; i++)
            {
                stacking.collectedObjects[i].GetComponent<BoxCollider>().enabled = true;
            }
            stacking.ParentCollider.enabled = false;
        }
        else if (other.tag == "Gold" || other.tag == "Diamond" || other.tag == "Money")
        {
            //Conveyor moves gathered objects to the left.
            other.GetComponent<BoxCollider>().enabled = false;
            other.transform.parent = GameObject.Find("Finish").transform;
            other.transform.DOLocalMoveZ(0, 0.1f);
            other.transform.DOLocalMoveX(-18, 3);

            stacking.collectedObjects.Remove(other.transform);
            ATM.score += ((int)other.GetComponent<Collecting>().type);
            Destroy(other.gameObject, 3);
        }
        else if (other.tag == "Collector")
        {
            //Level completed, next level button appears.
            _player.MovementStop();
            nextLevelButton.gameObject.SetActive(true);
        }
    }
}
