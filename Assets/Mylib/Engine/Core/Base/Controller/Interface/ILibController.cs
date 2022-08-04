using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GNL.URP_MyLib
{
    public interface ILibController
    {
        void StateChanging();
        void SubStateChanging();
        void CheckingAllfind();

        void Update_State();
    }
}