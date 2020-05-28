using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    Front = 0,

    Back,

    Left,

    Right
}

public abstract class Actor : MonoBehaviour

{

    /// <summary>

    /// debug模式，程序测试

    /// </summary>

    public bool _debug;



    /// <summary>

    /// 玩家id

    /// </summary>

    public int _uid;



    /// <summary>

    /// 玩家名字

    /// </summary>

    public string _name;



    /// <summary>

    /// 移动速度

    /// </summary>

    public float _moveSpeed;



    /// <summary>

    /// 是否正在移动

    /// </summary>

    public bool _isMoving;



    /// <summary>

    /// 坐标

    /// </summary>

    public Vector3 _pos;



    /// <summary>

    /// 当前状态

    /// </summary>

    public ActorState _curState { set; get; }



    public ActorStateType _stateType;



    public Direction _direction = Direction.Front;



    /// <summary>

    /// 状态机集合

    /// </summary>

    public Dictionary<ActorStateType, ActorState> _actorStateDic = new Dictionary<ActorStateType, ActorState>();



    /// <summary>

    /// 动画控制器

    /// </summary>

    [HideInInspector]

    public Animator _animator;



    private Transform _transform;



    void Awake()

    {

        _transform = this.transform;

        _animator = GetComponent<Animator>();

        InitState();

        InitCurState();

    }



    /// <summary>

    /// 初始化状态机

    /// </summary>

    protected abstract void InitState();



    /// <summary>

    ///  初始化当前状态

    /// </summary>

    protected abstract void InitCurState();





    /// <summary>

    /// 改变状态机

    /// </summary>

    /// <param name="stateType"></param>

    /// <param name="param"></param>

    public void TransState(ActorStateType stateType)

    {

        if (_curState == null)

        {

            return;

        }

        if (_curState.StateType == stateType)

        {

            return;

        }

        else

        {

            ActorState _state;

            if (_actorStateDic.TryGetValue(stateType, out _state))

            {

                _curState.Exit();

                _curState = _state;

                _curState.Enter(this);

                _stateType = _curState.StateType;

            }

        }

    }



    /// <summary>

    /// 更新状态机

    /// </summary>

    public void UpdateState()

    {

        if (_curState != null)

        {

            _curState.Update();

        }

    }



    /// <summary>

    /// 移动 数据（状态）驱动表现

    /// </summary>

    public virtual void Move()

    {

        //TODO 移动相关状态

        _animator.SetInteger("Dir", (int)_direction);

        if (_debug)

        {

            //数据层位置

            _transform.position = _pos;

        }

        else

        {

            //表现层位置

            _transform.position = Vector3.Lerp(_transform.position, _pos, 100 * Time.deltaTime);

        }

    }



    /// <summary>

    /// 改变方向

    /// </summary>

    /// <param name="dir"></param>

    public void ChangeDir(Direction dir)

    {

        _direction = dir;

        if (_direction == Direction.Left)

        {

            _transform.localScale = new Vector3(-1, 1, 1);

        }

        else

        {

            _transform.localScale = new Vector3(1, 1, 1);

        }

    }



    /// <summary>

    /// 播放动画

    /// </summary>

    /// <param name="name"></param>

    /// <param name="dir"></param>

    public void PlayAnim(string name)

    {

        _animator.SetBool("Idle", false);

        _animator.SetBool("Run", false);

        _animator.SetBool(name, true);

        _animator.SetInteger("Dir", (int)_direction);

    }

}



