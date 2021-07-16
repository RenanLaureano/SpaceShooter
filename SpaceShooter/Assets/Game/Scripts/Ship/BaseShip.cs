using UnityEngine;

public abstract class BaseShip : MonoBehaviour
{

    public enum ShipState {
        alive,
        dead,
    }

    protected float Speed { get; set; }
    protected ShipState State { get; set; }
    protected Rigidbody2D ShipRigidbody { get; set; }
    protected Animator ShipAnimator { get; set; }

    public abstract void MoveHorizontal(int direction);
    public abstract void MoveVertical(int direction);

}
