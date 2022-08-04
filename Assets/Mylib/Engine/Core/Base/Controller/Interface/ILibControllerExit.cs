using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GNL.URP_MyLib
{
    public interface ILibControllerExit
    {
        void ExitSerialize(LibEdStateUtilities.GameStates lastState);
    }
}