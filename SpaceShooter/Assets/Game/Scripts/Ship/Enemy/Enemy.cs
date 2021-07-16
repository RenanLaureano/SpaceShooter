using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseShip, ILifeController, IControllerShip
{
    [SerializeField] int life;
    [SerializeField] int damageColission;
    [SerializeField] new float Speed;
    [SerializeField] BaseWeapon weapon;
    [SerializeField] float timeToChangeDirection;
    [SerializeField] GameObject ExplosionPrefab;
    
    float countTimeChange;

    private int horizontal;
    private int vertical;

    private Command moveForward;
    private Command moveLeft;
    private Command moveRight;
    private Command noButtonHorizontal;
    private Command noButtonVertical;


    void Start()
    {
        ShipRigidbody = GetComponent<Rigidbody2D>();
        ShipAnimator = GetComponent<Animator>();

        moveForward = new MoveForwardCommand(this);
        moveLeft = new MoveLeftCommand(this);
        moveRight = new MoveRightCommand(this);

        noButtonHorizontal = new StopHorizontalCommand(this);
        noButtonVertical = new StopVerticalCommand(this);

        countTimeChange = timeToChangeDirection;

        ExecuteNewCommand(moveForward);
    }

    private void Update()
    {
        if (State == ShipState.alive)
        {
            HandleInput();

            weapon.HandleInput();
        }
    }

    private void FixedUpdate()
    {
        ShipRigidbody.velocity = new Vector2(horizontal * Speed, vertical * Speed);
        ShipAnimator.SetInteger("Horizontal", horizontal);
    }

    private void ExecuteNewCommand(Command commandButton)
    {
        commandButton.Execute();
    }

    public override void MoveHorizontal(int direction)
    {
        horizontal = direction;
    }

    public override void MoveVertical(int direction)
    {
        vertical = -direction;
    }

    public void HitDamage(int damage)
    {
        life -= damage;

        if(life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        State = ShipState.dead;

        ExecuteNewCommand(noButtonVertical);
        ExecuteNewCommand(noButtonHorizontal);

        Instantiate(ExplosionPrefab, transform.position, transform.rotation);

        TriggerService.Instance.FireEvent(TriggerType.KILL_ENEMY, null);

        Destroy(gameObject);
    }

    public void HandleInput()
    {
        countTimeChange += Time.deltaTime;

        if(countTimeChange >= timeToChangeDirection)
        {
            if(Random.Range(0,2) == 1)
            {
                int rand = Random.Range(0, 100);

                if(rand <= 33)
                {
                    ExecuteNewCommand(moveLeft);
                }
                else if (rand <= 66)
                {
                    ExecuteNewCommand(moveRight);
                }
                else
                {
                    ExecuteNewCommand(moveForward);
                }
            }

            countTimeChange = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player" || collision.gameObject.layer == 6)
        {
            return;
        }

        collision.gameObject.SendMessage("HitDamage", damageColission, SendMessageOptions.DontRequireReceiver);

        Die();
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
