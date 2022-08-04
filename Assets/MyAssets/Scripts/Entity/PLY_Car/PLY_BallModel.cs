using Com.GNL.URP_MyLib;
using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.GNL.URPProduction
{


    public class PLY_BallModel : MonoBehaviour
    {
        public Vector2 finalDir;
        public float finalDirMag;
        public Vector3 newDir;

        public int lastMoveHor = 0;
        public int lastMoveVer = 0;

        private enum LastDirectionHor
        {
            none,
            right,
            left,
        }

        private enum LastDirectionVer
        {
            none,
            front,
            back
        }

        public void AnimUpdate(Vector3 Dir, float RotateSpeed, Vector2 DirectionSpd, GameObject[] Cones)
        {

            finalDir = new Vector2(DirectionSpd.x, DirectionSpd.y);
            finalDirMag = finalDir.magnitude;


            transform.Rotate(Dir, Time.deltaTime * RotateSpeed * finalDir.magnitude, Space.World);


            //Debug.DrawLine(this.transform.position, this.transform.position + newDir , Color.black);
            //Debug.DrawLine(this.transform.position, this.transform.position + Dir * 10, Color.cyan);
            //Debug.DrawLine(this.transform.position, Cones[(int)PLY_BallController.Direction.front].transform.position, Color.green);
            //Debug.DrawLine(this.transform.position, Cones[(int)PLY_BallController.Direction.left].transform.position, Color.blue);
            //Debug.DrawLine(this.transform.position, Cones[(int)PLY_BallController.Direction.right].transform.position, Color.yellow);
            //Debug.DrawLine(this.transform.position, Cones[(int)PLY_BallController.Direction.back].transform.position, Color.white);

            //Debug.DrawLine(this.transform.localPosition, this.transform.localPosition + newDir * 7, Color.black);
            //Debug.DrawLine(this.transform.localPosition, Cones[(int)PLY_BallController.Direction.front].transform.localPosition, Color.green);
            //Debug.DrawLine(this.transform.localPosition, Cones[(int)PLY_BallController.Direction.left].transform.localPosition, Color.blue);
            //Debug.DrawLine(this.transform.localPosition, Cones[(int)PLY_BallController.Direction.right].transform.localPosition, Color.yellow);
            //Debug.DrawLine(this.transform.localPosition, Cones[(int)PLY_BallController.Direction.back].transform.localPosition, Color.white);



        }
    }
}