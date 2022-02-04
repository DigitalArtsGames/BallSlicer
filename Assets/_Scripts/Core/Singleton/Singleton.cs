using UnityEngine;

namespace DartsGames.Core
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this as T;

            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}