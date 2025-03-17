public interface IUpdate
{
    void PerformInitialUpdate();
    void PerformPreUpdate();
    void PerformUpdate();
    void PerformFinalUpdate();
    void PerformLateUpdate();
}
/*
 
 
 
 
     public void PerformInitialUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformPreUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformFinalUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformLateUpdate()
    {
        throw new System.NotImplementedException();
    }
    private void RegisterUpdate()
    {
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    private void UnregisterUpdate()
    {
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
 
 
 
 
 
 
 */
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