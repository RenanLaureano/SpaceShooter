using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseShip, ILifeController, IControllerShip
{
    [SerializeField] int Life;
    [SerializeField] new float Speed;
    [SerializeField] BaseWeapon weapon;

    [Header("Limits")]
    [SerializeField] private Transform LimitTop;
    [SerializeField] private Transform LimitBotton;
    [SerializeField] private Transform LimitRight;
    [SerializeField] private Transform LimitLeft;

    private int horizontal;
    private int vertical;
    private int initialLife;

    private Command buttonW;
    private Command buttonA;
    private Command buttonS;
    private Command buttonD;
    private Command noButtonHorizontal;
    private Command noButtonVertical;


    private void Awake()
    {
        TriggerService.Instance.AddListener(TriggerType.START_ROUND, ReciveLife);
    }

    void Start()
    {
        ShipRigidbody = GetComponent<Rigidbody2D>();
        ShipAnimator = GetComponent<Animator>();

        buttonW = new MoveForwardCommand(this);
        buttonA = new MoveLeftCommand(this);
        buttonS = new MoveBackCommand(this);
        buttonD = new MoveRightCommand(this);

        noButtonHorizontal = new StopHorizontalCommand(this);
        noButtonVertical = new StopVerticalCommand(this);

        initialLife = Life;
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
        vertical = direction;
    }

    public void HitDamage(int damage)
    {
        Life -= damage;

        float perc = (float)Life / (float)initialLife;
        TriggerService.Instance.FireEvent(TriggerType.CHANGE_LIFE, perc);

        if (Life <= 0)
        {
            Life = 0;
            Die();
        }
    }

    public void Die()
    {
        State = ShipState.dead;

        ExecuteNewCommand(noButtonVertical);
        ExecuteNewCommand(noButtonHorizontal);

        TriggerService.Instance.FireEvent(TriggerType.GAME_OVER, false);

        Destroy(gameObject);
    }

    public void HandleInput()
    {

        float posY = transform.position.y;

        if (Input.GetKey(KeyCode.W) && posY < LimitTop.position.y)
        {
            ExecuteNewCommand(buttonW);
        }
        else if (Input.GetKey(KeyCode.S) && posY > LimitBotton.position.y)
        {
            ExecuteNewCommand(buttonS);
        }
        else
        {
            ExecuteNewCommand(noButtonVertical);
        }

        float posX = transform.position.x;

        if (Input.GetKey(KeyCode.D) && posX < LimitRight.position.x)
        {
            ExecuteNewCommand(buttonD);
        }
        else if (Input.GetKey(KeyCode.A) && posX > LimitLeft.position.x)
        {
            ExecuteNewCommand(buttonA);
        }
        else
        {
            ExecuteNewCommand(noButtonHorizontal);
        }
    }

    private void ReciveLife(TriggerType type, object param)
    {
        WaveObejct wave = (WaveObejct)param;

        Life += wave.lifeRecive;
        if (Life > initialLife)
        {
            Life = initialLife;
        }

        float perc = (float)Life / (float)initialLife;
        TriggerService.Instance.FireEvent(TriggerType.CHANGE_LIFE, perc);
    }
}
