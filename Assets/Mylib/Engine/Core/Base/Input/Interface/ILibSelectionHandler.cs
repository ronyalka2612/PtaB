using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GNL.URP_MyLib
{
    public interface ILibSelectionHandler
    {
        public void LibSetObjectSelection(RaycastHit hit);
    }
}