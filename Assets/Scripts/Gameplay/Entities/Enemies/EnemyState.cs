public abstract class EnemyState
{
    protected EnemyStateHandler stateMachine;
    protected Enemy self;

    protected EnemyState(Enemy enemy, EnemyStateHandler stateMachine)
    {
        this.self = enemy;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public virtual void Exit() { }

    public virtual void Pause() { }

    public virtual void Resume() { }

    public override string ToString()
    {
        return this.GetType().Name;
    }


}