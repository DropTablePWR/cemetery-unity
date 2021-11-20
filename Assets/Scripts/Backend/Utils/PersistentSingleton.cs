using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool singletonDestroyed = false;

    private static T instance;
    public static T Instance
    {
        get
        {
            if (singletonDestroyed) 
            {
                Debug.LogWarningFormat("[Singleton] Singleton was already destroyed by quiting game. Returning null");
                return null;
            }

            if (!instance)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    new GameObject(typeof(T).ToString()).AddComponent<T>();
                }

                DontDestroyOnLoad(instance);
            }

            return instance;
        }
    }
    protected virtual void Awake()
    {
        if (instance == null && !singletonDestroyed)
        {
            instance = this as T;
        }
        else if (instance != this) 
        {
            Destroy(this);
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance != this)
        {
            return;
        }

        singletonDestroyed = true;
        instance = null;
    }
}