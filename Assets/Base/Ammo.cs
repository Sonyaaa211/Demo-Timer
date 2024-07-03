using Hung.Pooling;
using UnityEngine;

public abstract class Ammo : MonoBehaviour, ICast<Vector3, Transform>, IPoolable, IToggleable
{
    [SerializeField] private float lifetime;
    [SerializeField] protected float timer;

    protected float Lifetime 
    { 
        get            
        {
            if (lifetime == 0)
            {
                lifetime = 3;
            }
            return lifetime;
        } 
    }
    public Transform Storage { get; set; }

    private void OnEnable()
    {
        ResetLifetime();
    }

    internal void RandomAppear()
    {
        ToggleOff();
        Invoke("ToggleOn", Random.Range(0, 0.75f));
    }

    public void Cast(Vector3 position, Transform parent)
    {
       // ResetLifetime();
        transform.SetParent(parent, false);
        transform.position = position;
        ToggleOn();  
    }

    public void Cast(Vector3 position)
    {
        //ResetLifetime();
        transform.position = position;
        ToggleOn();
    }

    public void Precast()
    {
        ToggleOff();
    }

    protected abstract void LiveAction();

    internal virtual void ResetLifetime()
    {
        timer = Lifetime;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            LiveAction();
        }
        else
        {           
            BackToPool();                     
        }
    }

    public virtual void BackToPool()
    {
        Precast();
        if (Storage != null)
        {
            transform.SetParent(Storage);
        }
        else
        {

        }
    }

    public void ToggleOn()
    {
        gameObject.SetActive(true);
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }
}
