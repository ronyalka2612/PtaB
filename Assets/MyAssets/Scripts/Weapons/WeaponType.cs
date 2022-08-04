using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Com.GNL.URP_MyLibProjectTest
{
    [CreateAssetMenu(fileName = "WpnAtch_", menuName = "GNL/Weapon/WeaponType")]
    [System.Serializable]
    public class WeaponType : ScriptableObject
    {
        public WeaponUtilities.WeaponType Name;

        //public int enemyLength = 5;
        //public int[] enemyArray;

        //void Reset()
        //{
        //    enemyArray = new int[enemyLength];

        //}

        //void OnValidate()
        //{
        //    if (enemyArray.Length != enemyLength)
        //    {
        //        Array.Resize(ref enemyArray, enemyLength);
        //    }
        //    int x = 0;
        //    while (x < enemyLength)
        //    {
        //        enemyArray[x] = x;
        //        x++;
        //    }
        //}

        ////yang dibawah in bisa juga, tapi yang ada dokumentasi dari unity yang diaatas , dan dipanggil saat di editor, yang dibawah paki function start, yang dimana masuk / jalan dalam build
        //public GameObject[] mainSlider;

        //public void Start()
        //{
        //    //Adds a listener to the main slider and invokes a method when the value changes.
        //    mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        //    mainSlider = new GameObject[enemyLength];
        //}

        //// Invoked when the value of the slider changes.
        //public void ValueChangeCheck()
        //{
        //    //Debug.Log(mainSlider.value);
        //    if (mainSlider.Length != enemyLength)
        //    {
        //        Array.Resize(ref mainSlider, enemyLength);
        //    }
        //}
    }
}