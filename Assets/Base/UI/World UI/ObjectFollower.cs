using System;
using System.Collections.Generic;
using System.Linq;
using Hung.Pooling;
using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectFollower<T> : ObjectFollower where T: IFollowable
{
    internal abstract void SetFollow(T follow);

    internal abstract void Unfollow();
}

public class ObjectFollower : WorldUI, IFollow, IReset
{
    [field: SerializeField] public Transform target { get; set; }
   
    [SerializeField] private bool startStatic;
    [SerializeField] private List<Image> renders;

    protected delegate bool VisibleChecker();

    protected VisibleChecker VisibleCheck;

    Action following;

    public void OnReset()
    {
        renders = GetComponentsInChildren<Image>(includeInactive: true).ToList();
    }

    private new void Awake()
    {
        if (!startStatic)
        {
            base.Awake();
        
            StartFollow();
            following = OnFollow;
        }
        else
        {
            following = () => { };
        }      
    }

    void LateUpdate()
    {
        following();
    }

    protected virtual void OnFollowing()
    {
        LocateAt(target.position);
    }

    public void OnFollow()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            if (VisibleCheck != null && !VisibleCheck())
            {
                StartHidden();
                following = OnHidden;
            }
            else OnFollowing();
        }
        else 
        {
            StartHidden();
            following = OnHidden;
        }
    }

    public void OnHidden()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            if (VisibleCheck == null || VisibleCheck())
            {
                StartFollow();
                following = OnFollow;
            }           
        }
    }

    protected virtual void StartFollow()
    {
        ToggleOn();
    }

    protected virtual void StartHidden()
    {
        ToggleOff();
    }

    public override void ToggleOn()
    {
        renders.ForEach(image => image.enabled = true);
    }

    public override void ToggleOff()
    {
        renders.ForEach(image => image.enabled = false);
    }
}

public interface IFollow<T> where T: IFollowable
{
    T target { get; set; }

    Vector3 Offset { get; set; }
}

public interface IFollow
{
    Transform target { get; set; }

    Vector3 Offset { get; set; }
}

public interface IFollowable: IMono
{
    Transform FollowTarget { get; }
}

public interface IFollow2
{
    Transform firstTarget { get; set; }

    Transform secondTarget { get; set; }

    Vector3 offset { get; set; }
}