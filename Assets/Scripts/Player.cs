using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


enum Direction
{
    UP,
    DOWN,
    RIGHT,
    LEFT,
}

enum Weapon
{
    KNIFE,
    GUN,
}

enum State{
    ALIVE,
    DEAD,
}

public class Player : MonoBehaviour
{
    // Direction dir = Direction.UP;
    Rigidbody2D rbody;
    [SerializeField] GameObject knife;
    [SerializeField] GameObject bullet;
    [SerializeField] int MAX_VELOCITY;
    [SerializeField] float KNIFE_VELOCITY;
    [SerializeField] float BULLET_VELOCITY;
    [SerializeField] float KNIFE_COOLDOWN;
    [SerializeField] float BULLET_COOLDOWN;
    [SerializeField] int MAX_HEALTH;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI scoreText;
    public float health;
    float knifeTime = 0;
    float bulletTime = 0;
    float direction = 0;
    State state = State.ALIVE;
    [SerializeField] Weapon weapon = Weapon.KNIFE;

    // Start is called before the first frame update
    void Start()
    {
        health = MAX_HEALTH;
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.DEAD)
            return;
        HandleKeyboard();
        HandleMouse();
        knifeTime += Time.deltaTime;
        bulletTime += Time.deltaTime;

        if (state != State.DEAD && health <= 0){
            state = State.DEAD;
            GameOver();
        }

        // scoreText.text = "10000000000";
    }

    void HandleKeyboard(){
        if (Input.GetKey(KeyCode.W))
        {
            Vector2 vel = rbody.velocity;
            rbody.velocity = new Vector2(vel.x, MAX_VELOCITY);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vector2 vel = rbody.velocity;
            rbody.velocity = new Vector2(vel.x, -MAX_VELOCITY);
        }
        else
        {
            Vector2 vel = rbody.velocity;
            rbody.velocity = new Vector2(vel.x, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector2 vel = rbody.velocity;
            rbody.velocity = new Vector2(-MAX_VELOCITY, vel.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector2 vel = rbody.velocity;
            rbody.velocity = new Vector2(MAX_VELOCITY, vel.y);
        }
        else
        {
            Vector2 vel = rbody.velocity;
            rbody.velocity = new Vector2(0, vel.y);
        }

        Vector2 vel2 = rbody.velocity;
        if (vel2.magnitude > 0)
            rbody.rotation = direction = Vector2.SignedAngle(new Vector2(-vel2.x, vel2.y), new Vector2(0, 1));
        rbody.angularVelocity = 0;
    }

    void HandleMouse()
    {
        if (Input.GetMouseButton(0)){
            // Debug.Log(0);
            if (weapon == Weapon.KNIFE){
                if (knifeTime >= KNIFE_COOLDOWN){
                    ThrowKnife();
                    knifeTime = 0;
                }
            }
            else if (weapon == Weapon.GUN)
            {
                // Debug.Log("Gun");
                if (bulletTime >= BULLET_COOLDOWN){
                    FireBullet();
                    bulletTime = 0;
                }
            }
        }
        if (Input.GetMouseButtonDown(1)){
            switch (weapon)
            {
                case Weapon.KNIFE:
                    weapon = Weapon.GUN;
                    break;
                case Weapon.GUN:
                    weapon = Weapon.KNIFE;
                    break;
                default:
                    break;
            }
        }
    }

    void ThrowKnife(){
        Vector2 v = new Vector2(Mathf.Cos(Mathf.Deg2Rad*(direction+90)), Mathf.Sin(Mathf.Deg2Rad*(direction+90)));
        v = v*KNIFE_VELOCITY;
        GameObject knifeG = Instantiate(knife, transform.position, Quaternion.identity);
        knifeG.GetComponent<Rigidbody2D>().rotation = direction;
        knifeG.GetComponent<Rigidbody2D>().velocity = v;
    }

    void FireBullet(){
        Vector2 v = new Vector2(Mathf.Cos(Mathf.Deg2Rad*(direction+90)), Mathf.Sin(Mathf.Deg2Rad*(direction+90)));
        v = v*BULLET_VELOCITY;
        GameObject bulletG = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletG.GetComponent<Rigidbody2D>().rotation = direction;
        bulletG.GetComponent<Rigidbody2D>().velocity = v;
    }

    void GameOver(){
        SceneManager.LoadScene("GameOver");
    }

    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "EnemyBullet"){
            BulletHit();
        } else if (collision.gameObject.tag == "Enemy"){
            BulletHit();
        }
    }

    public void BulletHit(){
        health -= 4;
        healthText.text = health+"";
        Debug.Log("Bullet Hit");
    }
}
