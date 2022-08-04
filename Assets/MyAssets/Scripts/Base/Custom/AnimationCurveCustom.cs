
using MyBox;
using System;
using UnityEngine;

namespace Com.GNL.URP_MyLibProjectTest
{
    [Serializable]
    public class AnimationCurveCustom
    {
        [Separator("Curve Per Anim", true)]
        //[NamedArray(new string[] { "Foot_1", "Foot_2" })]
        public AnimationCurveExpand[] CurveExpand;

        public AnimationCurveCustom(int num)
        {
            CurveExpand = new AnimationCurveExpand[num];
            for (int i = 0; i < num; i++)
            {
                CurveExpand[i] = new AnimationCurveExpand();
            }
        }


        
    }
    [Serializable]
    public class AnimationCurveExpand
    {
        
        //[NamedArray(new string[] { "Foot_1", "Foot_2" })]
        public AnimationCurve Curve;
        public float Start;
        public float End;

        public AnimationCurveExpand()
        {
            Curve = new AnimationCurve();
        }

    }
}