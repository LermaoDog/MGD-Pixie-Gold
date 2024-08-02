using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandstormAI : MonoBehaviour
{
    private Transform target;
    public float speed;

    public Animator anim;
    public ZyPlayerMove PlayerMove;
    public AdvancedSliding PlayerSlide;
    public GameObject gameOverScreen;

    public IEnumerator Die;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PlayerMove = FindObjectOfType<ZyPlayerMove>();
        PlayerSlide = FindObjectOfType<AdvancedSliding>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("sandstorm collision");
            PlayerMove.speed = 0f;
            PlayerMove.jumpVelocity = 0f;
            PlayerMove.enabled = false;
            PlayerSlide.enabled = false;

            StartCoroutine(PlayerMove.Die());
            gameOverScreen.SetActive(true);

            this.enabled = false;
        }
    }
}
