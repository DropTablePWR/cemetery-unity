using UnityEngine;

namespace Backend
{
    public class App : Singleton<App>
    {
        [SerializeField] private BackendManager _backend;
               
        public BackendManager GetBackend() => _backend;
    }
}