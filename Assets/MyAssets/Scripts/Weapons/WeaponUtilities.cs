using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.GNL.URP_MyLibProjectTest
{
    public static class WeaponUtilities
    {
        public enum WEAPON_ATTACH
        {
           SIDE
        }
        //
        // Summary:
        //     Pastikan sudah ada object/node pada model/rig sbeseperti dibawah ini.
        public enum WeaponAttach
        {
            HAND_RIGHT,
            HAND_RIGHT_RIGHTSIDE,
            HAND_RIGHT_LEFTSIDE,
            HAND_RIGHT_UPSIDE, 
            HAND_RIGHT_DOWNSIDE,
            HAND_LEFT,
            HAND_LEFT_RIGHTSIDE,
            HAND_LEFT_LEFTSIDE,
            HAND_LEFT_UPSIDE, 
            HAND_LEFT_DOWNSIDE,
            LEG_RIGHT,
            LEG_RIGHT_RIGHTSIDE,
            LEG_RIGHT_LEFTSIDE,
            LEG_RIGHT_FRONTSIDE,
            LEG_RIGHT_DOWNSIDE,
            LEG_LEFT,
            LEG_LEFT_RIGHTSIDE,
            LEG_LEFT_LEFTSIDE,
            LEG_LEFT_FRONTSIDE,
            LEG_LEFT_DOWNSIDE,
            BELLY,
            CHEST,
            SHOULDER_RIGHT,
            SHOULDER_LEFT,
            BACK,
            HEAD,
            HEAD_UPSIDE,
            HEAD_RIGHTSIDE,
            HEAD_LEFTSIDE,
            LEG_RIGHT_BACKSIDE,
            LEG_LEFT_BACKSIDE,
        }

        //public enum WeaponAttachValEqualExistBody
        //{
        //    HAND_RIGHT = WeaponAttachExistBody.HAND_RIGHT,
        //    HAND_RIGHT_RIGHTSIDE = WeaponAttachExistBody.HAND_RIGHT,
        //    HAND_RIGHT_LEFTSIDE = WeaponAttachExistBody.HAND_RIGHT,
        //    HAND_RIGHT_UPSIDE = WeaponAttachExistBody.HAND_RIGHT,
        //    HAND_RIGHT_DOWNSIDE = WeaponAttachExistBody.HAND_RIGHT ,
        //    HAND_LEFT = WeaponAttachExistBody.HAND_LEFT,
        //    HAND_LEFT_RIGHTSIDE = WeaponAttachExistBody.HAND_LEFT,
        //    HAND_LEFT_LEFTSIDE = WeaponAttachExistBody.HAND_LEFT,
        //    HAND_LEFT_UPSIDE = WeaponAttachExistBody.HAND_LEFT,
        //    HAND_LEFT_DOWNSIDE = WeaponAttachExistBody.HAND_LEFT,
        //    LEG_RIGHT = WeaponAttachExistBody.LEG_RIGHT,
        //    LEG_RIGHT_RIGHTSIDE = WeaponAttachExistBody.LEG_RIGHT,
        //    LEG_RIGHT_LEFTSIDE = WeaponAttachExistBody.LEG_RIGHT,
        //    LEG_RIGHT_UPSIDE = WeaponAttachExistBody.LEG_RIGHT,
        //    LEG_RIGHT_DOWNSIDE = WeaponAttachExistBody.LEG_RIGHT,
        //    LEG_LEFT = WeaponAttachExistBody.LEG_LEFT,
        //    LEG_LEFT_RIGHTSIDE = WeaponAttachExistBody.LEG_LEFT,
        //    LEG_LEFT_LEFTSIDE = WeaponAttachExistBody.LEG_LEFT,
        //    LEG_LEFT_UPSIDE = WeaponAttachExistBody.LEG_LEFT,
        //    LEG_LEFT_DOWNSIDE = WeaponAttachExistBody.LEG_LEFT,
        //    BELLY = WeaponAttachExistBody.BELLY,
        //    CHEST = WeaponAttachExistBody.CHEST,
        //    SHOULDER_RIGHT = WeaponAttachExistBody.SHOULDER_RIGHT,
        //    SHOULDER_LEFT = WeaponAttachExistBody.SHOULDER_LEFT,
        //    BACK = WeaponAttachExistBody.CHEST,
        //    HEAD = WeaponAttachExistBody.HEAD,
        //    HEAD_UPSIDE = WeaponAttachExistBody.HEAD,
        //    HEAD_RIGHTSIDE = WeaponAttachExistBody.HEAD,
        //    HEAD_LEFTSIDE = WeaponAttachExistBody.HEAD,
        //}

        public enum WeaponAttachExistBody
        {
            HAND_RIGHT,
            HAND_LEFT,
            LEG_RIGHT,
            LEG_LEFT,
            BELLY,
            CHEST, // BACK sama like chest
            SHOULDER_RIGHT,
            SHOULDER_LEFT,
            HEAD,
        }

        public enum WEAPON_ATTACH_EXIST_BODY
        {
            SIDE
        }

        public enum WeaponType
        {
            SWORD_HAND,
            SWORD_UNDER_ARM,
            TWO_HANDED_SWORD,
            GUNS
        }

        public enum WEAPON_TYPE
        {
            SWORD_HAND,
            SWORD_UNDER_ARM,
            TWO_HANDED_SWORD,
            GUNS
        }
    }
}