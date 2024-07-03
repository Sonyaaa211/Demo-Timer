using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hung.Pooling
{
    public interface ICommonPoolable: IPoolable
    {

    }

    [CreateAssetMenu(menuName = "Hung/SO Singleton/Common Pool")]
    public class CommonPool : SOSingleton<CommonPool>
    {
        [SerializeReference] public List<ICommonPoolable> pool;

        public void Spawn<T>(out T spawnling) where T: ICommonPoolable
        {
            spawnling = pool[0].gameObject.GetComponent<T>();
        }
    }
}

