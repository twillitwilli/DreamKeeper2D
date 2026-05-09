using UnityEngine;

namespace SoT.AbstractClasses
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                // if the instance doesnt exist, will print debug log claiming its Null
                if (_instance == null)
                    Debug.Log(typeof(T).ToString() + " is NULL");

                return _instance;
            }
        }

        public virtual void Awake()
        {
            // Cast as T
            //_instance = (T)this;
            _instance = this as T;
        }
    }
}