using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameParameters GameParameters;
    [SerializeField] private Team _team;
    public Team Team => _team;
    public GameObject Bullet;
    public Transform Canon;
    public float Speed { get; set; }
    public Vector3 PositionToGo { get; set; }
    public Transform Target { get; set; }

    private StateMachine<Tank> _stateMachine;
    public string NextState { get; set; }
    public float TimerShoot { get; set; }
    public bool CanShoot => TimerShoot <= 0;

    public GridController GridController;

    private void Awake()
    {
        _stateMachine = new StateMachine<Tank>();
        _stateMachine.Init(this);
    }

    void Start()
    {
        Speed = GameParameters.TankSpeed;

        _stateMachine.ChangeState(TankStates.Idle);
    }

    public void Update()
    {
        _stateMachine.Update();
        
        if (TimerShoot > 0)
        {
            TimerShoot -= Time.deltaTime;
        }
    }

    public void Move(Vector3 pos)
    {
        if (Vector2.Distance(pos, transform.position) > 0.1f)
        {
            Cell cellBelow = GridController.currentFlowField.GetCellFromWorldPosition(transform.position);

            Debug.Log(Vector2.Distance(pos, transform.position));
            Vector2 targetDir = cellBelow.BestDirection.Vector;
            Vector3 dir = targetDir;
            float angle = Vector2.SignedAngle(targetDir, transform.up);
            transform.position += dir.normalized * Speed * Time.deltaTime;
            //if (angle > 1f || angle < -1f)
            //{
            //    transform.Rotate(new Vector3(0, 0, (GameParameters.TankTurnSpeed * -Mathf.Sign(angle)) * Time.deltaTime));
            //}
            //else
            //{
            //    transform.position += transform.up * Speed * Time.deltaTime;
            //}
        }
    }
}
