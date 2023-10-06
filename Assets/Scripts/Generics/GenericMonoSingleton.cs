using UnityEngine;

/* Used by singletons which are present throughout the game */

public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = (T)this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
