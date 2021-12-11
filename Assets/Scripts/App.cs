using UnityEngine;

namespace Backend
{
    public class App : Singleton<App>
    {
        [SerializeField] private BackendManager _backend;
        [SerializeField] private LookAtManager _lookAt;
               
        public BackendManager GetBackend() => _backend;
        public LookAtManager GetLookAt() => _lookAt;
    }
}