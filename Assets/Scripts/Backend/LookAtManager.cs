using UnityEngine;

namespace Backend
{
    public class LookAtManager : BaseMonoBehaviour
    {
        public LayerMask mask;
        public GameObject placeUI;
        public GameObject removeUI;
        public GameObject pendingUI;

        public enum Mode
        {
            NONE,
            PENDING,
            EMPTY,
            FULL
        }

        private Mode mode = Mode.NONE;

        public void SetMode(Mode mode)
        {
            this.mode = mode;
        }

        private void HideUI(bool pending)
        {
            placeUI.SetActive(false);
            removeUI.SetActive(false);
            pendingUI.SetActive(pending);
            
        }

        private void Update()
        {
            if (mode != Mode.PENDING)
            {
                if (Physics.Raycast(transform.position, transform.forward, out var hit, Mathf.Infinity, mask))
                {
                    var obj = hit.collider.gameObject;
                    ItemSlot slot = obj.GetComponent<ItemSlot>();

                    if (slot.IsEmpty())
                    {
                        mode = Mode.EMPTY;
                        placeUI.SetActive(true);
                        removeUI.SetActive(false);
                        pendingUI.SetActive(false);

                        if (Input.GetKeyDown(KeyCode.Alpha1))
                        {
                            slot.RequestAddingFeature(1);
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha2))
                        {
                            slot.RequestAddingFeature(2);
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha3))
                        {
                            slot.RequestAddingFeature(3);
                        }
                    }
                    else
                    {
                        mode = Mode.FULL;
                        placeUI.SetActive(false);
                        removeUI.SetActive(true);
                        pendingUI.SetActive(false);
                        if (Input.GetKeyDown(KeyCode.Alpha1))
                        {
                            slot.RequestDeletingFeature();
                        }
                    }
                }
                else
                {
                    HideUI(false);
                    mode = Mode.NONE;
                }
            }
            else
            {
                HideUI(true);
            }
        }
    }
}