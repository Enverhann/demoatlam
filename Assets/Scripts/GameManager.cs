using UnityEngine;


public class GameManager : MonoBehaviour
{
    public bool isGameStarted = false;
    [SerializeField]private PlayerEffectAnimations player;

    void Update()
    {   //Start game with mouse click.
        if (!isGameStarted && Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            player.GameStart();
            this.gameObject.SetActive(false);
        }
    }
}
