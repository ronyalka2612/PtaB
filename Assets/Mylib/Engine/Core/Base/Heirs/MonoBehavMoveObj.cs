using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Internal;

namespace Com.GNL.URP_MyLib
{
    public abstract class MonoBehavMoveObj : MonoBehaviourMyBase
    {
        public ObjectMoveMyLib _ObjectMoveMyLib;
        public abstract void Update_Obj();

        public abstract void Update_FU_Obj();
        public abstract void Update_LU_Obj();

        public virtual void Update ()
        {
            if(!LibMasterGameController.InstanceLibMaster.ItsNeedTobePauseWhenPause)
                Update_Obj();
        }
        public virtual void FixedUpdate()
        {
            if (!LibMasterGameController.InstanceLibMaster.ItsNeedTobePauseWhenPause)
                Update_FU_Obj();
        }


        public virtual void LateUpdate()
        {
            if (!LibMasterGameController.InstanceLibMaster.ItsNeedTobePauseWhenPause)
                Update_LU_Obj();
        }

        public MonoBehavMoveObj()
        {
            _ObjectMoveMyLib = new ObjectMoveMyLib(this);
        }
    }

    public class ObjectMoveMyLib
    {
        public MonoBehavMoveObj _MonoBehavMoveObj;

        public ObjectMoveMyLib(MonoBehavMoveObj monoBehavMoveObj)
        {
            _MonoBehavMoveObj = monoBehavMoveObj;
        }


    }
}
