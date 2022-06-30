using UnityEngine;
using DG.Tweening;

public class PlayerEffectAnimations : MonoBehaviour
{
    public float knockback;
    private Rigidbody _rb;
    private Animator _anim;
    private PlayerController _playerController;
    public GameObject tap;
    public GameObject levelCompleted;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }
    public void GameStart()
    {
        _playerController.canMove = true;
        Run();
        tap.SetActive(false);
    }
    public void Run()
    {
        _anim.SetBool("Run", true);
    }
    public void MovementStop()
    {
        _playerController.canMove = false;
        _rb.velocity = Vector3.zero;
        levelCompleted.SetActive(true);
        _anim.Play("Dance");
    }
    public void OnCollisionEnter(Collision other)
    {
        //Anims for colliding with obstacles.
        if (other.collider.CompareTag("Card"))
        {
            transform.DOShakeScale(0.2f, Vector3.right * 4, 20, 50, true).SetEase(Ease.InOutSine);
            _rb.AddForce(0, 0, -knockback);
        }
    }
}
