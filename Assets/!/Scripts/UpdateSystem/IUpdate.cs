public interface IUpdate
{
    void PerformInitialUpdate();
    void PerformPreUpdate();
    void PerformUpdate();
    void PerformFinalUpdate();
    void PerformLateUpdate();
}

/*
 
 
 public interface IUpdate
{
    bool NeedsInitialUpdate { get; set; }
    void PerformInitialUpdate();
    bool NeedsPreUpdate { get; set; }
    void PerformPreUpdate();
    bool NeedsUpdate { get; set; }
    void PerformUpdate();
    bool NeedsFinalUpdate { get; set; }
    void PerformFinalUpdate();
    bool NeedsLateUpdate { get; set; }
    void PerformLateUpdate();
}
     public bool NeedsInitialUpdate
    {
        get { return false; }
        set { }
    }
    public bool NeedsPreUpdate
    {
        get { return false; }
        set { }
    }
    public bool NeedsUpdate
    {
        get { return false; }
        set { }
    }
    public bool NeedsFinalUpdate
    {
        get { return false; }
        set { }
    }
    public bool NeedsLateUpdate
    {
        get { return false; }
        set { }
    }
 
 */