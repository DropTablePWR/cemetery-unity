using Models;

namespace Backend
{
    public class CemeteryItem : BaseMonoBehaviour
    {
        private Cemetery _cemetery;

        public void SetCemetery(Cemetery cemetery)
        {
            _cemetery = cemetery;
        }

        public int GetCemeteryId()
        {
            return _cemetery.id;
        }
    }
}