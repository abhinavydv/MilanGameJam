using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public float speed;
    Transform player;
    float epsilon = 0.2f;
    [SerializeField] Player pl;

    Vector2 targetVector;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetVector = new Vector2(player.position.x, player.position.y);
        pl = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetVector, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.position) < epsilon){
            pl.BulletHit();
            DestroyBullet();
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")){
            pl.BulletHit();
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
