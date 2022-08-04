using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine;



namespace Com.GNL.URP_MyLibProjectTest
{
    public static class Utilities
    {


        public enum LAYER
        { 
            ACTIVE_OBJECT = 28,
            WALL = 29,
            GROUND = 30
        }
        public enum TAG // free to place
        {
            // Entity
            Player,
            ENEMY_LEAD,
            ENEMY,
            // Objects on ENtity
            MY_WEAPON,
            ENM_WEAPON,
            // Controller
            //UI,
            //CONTROLLER,
            //CAMERA,
            //SUBSTATE,
            //JOYPAD,
            //DETACHED,
            COUNT
        }

        public enum FIND_GO 
        {
            // Mainmenu
            LoadingLayer_main,
            // Mainmenu_Multiplayer
            LoadingLayer,
            IF_CreateRoom,
            IF_JoinRoom,

            // MAIN_GP
            ETY_Ball,
            Player,
            PhotonController,
            Enemy
        }

        public static Transform SpawnerTransform;

    }
}