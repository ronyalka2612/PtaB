using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.GNL.URP_MyLib
{
    public class LibMasterScenesController : LibSingletonController<LibMasterScenesController>
    {
        [Header("Camera when loading")]
        public GameObject CameraLoading; 

        [Header("Loading Setting")]
        [SerializeField] public GameObject LoadingContent;
        [SerializeField] public GameObject LoadingTransition;
        [SerializeField] private GameObject LoadingInterface;
        [SerializeField] private Image LoadingProgressBar;
        //[MyBox.ReadOnly] 
        [Range(0.01f, 10.00f)]
        [SerializeField] private float LoadingTransitionTstTime = 1.333333f;
        //[MyBox.ReadOnly] 
        [Range(0.01f , 10.00f)]
        [SerializeField] private float LoadingContenTime = 1.149425f;



        [Header("Scenes Must Loaded as Additive")]

        public LibEdSceneUtilities.ScenesAdditive[] SceAddDefault;


        private LibMasterSceneConstruct CurSceneCall = new LibMasterSceneConstruct();
        private List<AsyncOperation> SubScenesToLoad;

        private List<MonoBehaviourMyBase> _listControllerDefault = new List<MonoBehaviourMyBase>();

        private Animator _loadingAnimationContent;
        private Animator _loadingAnimationTransition;


        private bool _isOnload;
        private LibEdStateUtilities.GameStates _tempChangeState;
        private List<LibEdStateUtilities.GameSubStates> _tempChangeSubState;



        private void OnValidate()
        {
            InitiateAwake();
        }

        private void InitiateAwake()
        {

        }




        private void Awake()
        {
            InitiateAwake();

            SubScenesToLoad = new List<AsyncOperation>();
            LoadingInterface.SetActive(true);

            _loadingAnimationContent = LoadingContent.GetComponent<Animator>();
            _loadingAnimationTransition = LoadingTransition.GetComponent<LibMasterTransitionController>().TST_USE.GetComponent<Animator>();

        }

        private void Start()
        {
            
            StateFunc.ClearState();

        }

        #region === State Changing ===

        public virtual void StateChanging()
        {
            
        }

        public virtual void SubStateChanging()
        {
            
        }

        public override void CheckingAllfind()
        {
            if (LoadingInterface == null)
            {
                StateFunc.SetFindAll(false);
            }
            else 
            {
                StateFunc.SetFindAll(true);
            }
        }




        #endregion === State Changing ===

        #region === State Update ===

        private void Update()
        {
            StateFunc.StateUpdate(Update_State, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
            CheckInload();
        }

        public virtual void Update_State()
        {
            //Debug.Log("Test 2");
        }
        private void FixedUpdate()
        {
            StateFunc.StateUpdate(Update_StateFU, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public virtual void Update_StateFU()
        {

        }

        private void LateUpdate()
        {
            StateFunc.StateUpdate(Update_StateLU, StateFunc.StateChanging(StateChanging, SubStateChanging, CheckingAllfind));
        }

        public virtual void Update_StateLU()
        {

        }



        #endregion === State Update ===

        #region === State Called In Update ===

        #region === Change Scenes ===
        private void CheckInload()
        {
            if (_isOnload)
            {
                //[Obsolete("Use SceneManager.sceneCount and SceneManager.GetSceneAt(int index) to loop the all scenes instead.")]
                //Scene[] currentScene = SceneManager.GetAllScenes();
                int currentSceneCount = SceneManager.sceneCount;
                bool isAllReady = false;
               
                if (currentSceneCount - 1 == CurSceneCall.SceneAdditive.Length)
                {
                    int j = 0;
                    int trueCheck = 0;
                    for (int i = 0; i < currentSceneCount; i++)
                    {
                        if (SceneManager.GetSceneAt(i).name == CurSceneCall.SceneAdditive[j].ToString()) 
                        {
                            trueCheck++;
                        }
                        else if (SceneManager.GetSceneAt(i).name == LibUtilities.SCANE_MANAGER)
                        {
                            continue;
                        }
                    }
                    if (trueCheck == CurSceneCall.SceneAdditive.Length)
                    {
                        isAllReady = true;
                    }
                }
                else 
                {
                    isAllReady = true;
                }


                if (isAllReady)
                {
                    StartCoroutine(ChangeState2(_tempChangeState, _tempChangeSubState));
                    _isOnload = false;

                    //make buuton can process
                    LibMasterInputController.InstanceLibMaster.LibAttrb.IsInputEnable = true;
                    LibMasterGameController.InstanceLibMaster.SetNeedPauseWhenPause(false);
                    LibMasterGameController.InstanceLibMaster.SetSloMoDefault();
                    VirtualInputManager.Instance.InputAttr.NormalizeInput();
                }
                
            }
        }

        private bool _isWithLoad = true;
        private bool _isOnChangeState = false;
        //public void SetChangeScene(LibMasterSceneCall scene, bool isWithLoad = true)
        public void SetChangeScene(LibMasterSceneConstruct scene, bool isWithLoad = true)
        {
            if (_isOnChangeState)
                return;
            LibMasterGameController.InstanceLibMaster.SetNeedPauseWhenPause(true);
            LibMasterInputController.InstanceLibMaster.LibAttrb.IsInputEnable = false;
            VirtualInputManager.Instance.InputAttr.NormalizeInput();

            _isWithLoad = isWithLoad;
            LoadingContent.GetComponent<Animator>().SetFloat(LibUtilities.SPEED_MULTIPLIER, 1.00f/LoadingContenTime);
            LoadingTransition.GetComponent<LibMasterTransitionController>().TST_USE.GetComponent<Animator>().SetFloat(LibUtilities.SPEED_MULTIPLIER, 1.00f / LoadingTransitionTstTime);

            _tempChangeState = scene.State;
            _tempChangeSubState = scene.Substates;

            _isOnChangeState = true;
            StartCoroutine(ChangeState(scene));
            // Harus di split, terpanggil saat Scen yg udah ada, 
            // dan  langkah kedua cobauntuk kondisi yang manggil find dihapus saja biar di cek selalu

            //StartCoroutine(ChangeState2(scene, changeStateReady, changeSubStateReady));

        }


        private IEnumerator ChangeState(LibMasterSceneConstruct scene)
        {

            //LoadingInterface.SetActive(true);
            //LoadingTransition.SetActive(true);
            if (VirtualStateManager.Instance.CurState == LibEdStateUtilities.GameStates.STARTER)
            {
                //diawal game, buat semua jadi gelap
                LoadingContent.GetComponent<CanvasGroup>().alpha = 1;
                LoadingTransition.GetComponent<LibMasterTransitionController>().TST_USE.GetComponent<CanvasGroup>().alpha = 1;
            }
            else
            {
                //transition animasiactive
                _loadingAnimationTransition.SetTrigger(LibUtilities.LOADING_TRIGGER_TRANSITION.TST_START.ToString());
                yield return new WaitForSeconds(LoadingTransitionTstTime);
                LoadingTransition.GetComponent<LibMasterTransitionController>().TST_USE.GetComponent<CanvasGroup>().alpha = 1;

            }
            LoadingProgressBar.fillAmount = 0;
            float allProgres = 0;

            //loading bar active animation
            _loadingAnimationContent.SetTrigger(LibUtilities.LOADING_TRIGGER_CONTENT.ANIM_START.ToString());
            yield return new WaitForSeconds(LoadingContenTime);
            LoadingContent.GetComponent<CanvasGroup>().alpha = 1 ;

            //Active SetActive the ScnManagers
            yield return SceneManager.GetSceneByName(LibUtilities.SCANE_MANAGER);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName( LibUtilities.SCANE_MANAGER));

            if (_isWithLoad)
            {
                // load scn baru
                foreach (LibEdSceneUtilities.ScenesAdditive scn in scene.SceneAdditive)
                {
                    LoadScene(SubScenesToLoad, scn);
                }
            
                LoadingProgressBar.fillAmount = 3f / 100f;
                // unload scn lama
                foreach (LibEdSceneUtilities.ScenesAdditive scn in CurSceneCall.SceneAdditive)
                {
                    UnLoadScene(SubScenesToLoad, scn);

                }
                LoadingProgressBar.fillAmount = 6f / 100f;
            }

            //delete just for in unity
            // delete when playing "_isWithLoad "false
            // for delete scene no need/ just playing with scene on list that condition _isWithLoad false
#if UNITY_EDITOR

            string[] nameScnNoNeed = null;
            if (!_isWithLoad)
            {
                //Debug.Log("cekcekcek SceneManager.sceneCount " + SceneManager.sceneCount);
                string[] nameScn = null;
                int countScn = 0;
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    foreach (LibEdSceneUtilities.ScenesAdditive scn in scene.SceneAdditive)
                    {

                        if (scn.ToString().Equals(SceneManager.GetSceneAt(i).name))
                        {
                            //Debug.Log("cekcekcek 2 Scn name : " + SceneManager.GetSceneAt(i).name);
                            countScn++;
                        }
                        if (countScn > 0)
                        {
                            if (nameScn == null)
                            {
                                nameScn = new string[countScn];
                                nameScn[0] = SceneManager.GetSceneAt(i).name;
                                //Debug.Log("cekcekcek 22 nameScn : " + nameScn[0]);
                            }
                            else if (nameScn.Length != countScn)
                            {
                                string[] temp = new string[countScn];
                                temp[countScn - 1] = SceneManager.GetSceneAt(i).name;
                                for (int j = 0; j < nameScn.Length; j++)
                                {
                                    temp[j] = nameScn[j];
                                }
                                nameScn = null;
                                nameScn = temp;
                            }
                        }
                    }
                }
                int countScnNoNeed = 0;
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    
                    if (SceneManager.GetSceneAt(i).name != LibUtilities.SCANE_MANAGER && nameScn[countScnNoNeed] != SceneManager.GetSceneAt(i).name)
                    {
                        countScnNoNeed++;
                        if (countScnNoNeed > 0)
                        {
                            if (nameScnNoNeed == null)
                            {
                                nameScnNoNeed = new string[countScnNoNeed];
                                nameScnNoNeed[0] = SceneManager.GetSceneAt(i).name;
                                //Debug.Log("cekcekcek 3 nameScnNoNeed[" + countScnNoNeed + "] : " + SceneManager.GetSceneAt(i).name + " will removed");
                            }
                            else if (nameScnNoNeed.Length != countScnNoNeed)
                            {
                                string[] temp = new string[countScnNoNeed];
                                temp[countScnNoNeed - 1] = SceneManager.GetSceneAt(i).name;
                                for (int j = 0; j < nameScnNoNeed.Length; j++)
                                {
                                    temp[j] = nameScnNoNeed[j];
                                }
                                nameScnNoNeed = null;
                                nameScnNoNeed = temp;
                                //Debug.Log("cekcekcek 3 nameScnNoNeed["+ countScnNoNeed + "] : " + SceneManager.GetSceneAt(i).name + " will removed");
                            }
                        }
                        if (nameScn.Length == countScnNoNeed)
                        {
                            break;
                        }
                    }
                }
                for (int i = 0; i < countScnNoNeed; i++)
                {
                    //if (!SceneManager.GetSceneByName(nameScnNoNeed[i]).isLoaded)
                    //{
                    //    while (!SceneManager.GetSceneByName(nameScnNoNeed[i]).isLoaded)
                    //    {
                    //        Debug.Log("cekcekcek 4 Scn name : " + nameScnNoNeed[i] + " wait to be remove");
                    //    }
                    //    Debug.Log("cekcekcek 4 Scn name : " + nameScnNoNeed[i] + " done waiting");
                    //}
                    SceneManager.UnloadSceneAsync(nameScnNoNeed[i]);
                    Debug.Log("cekcekcek 4 Scn name : " + nameScnNoNeed[i] + " removed");
                }
            }
#endif

            // ubah curentscen value menjadi nextScne
            CurSceneCall.SceneAdditive = scene.SceneAdditive;
            if (_isWithLoad)
            {
                // tambahkan scen additive yang ada pada scene list default
                if (SceAddDefault != null && SceAddDefault.Length > 0)
                {
                    foreach (LibEdSceneUtilities.ScenesAdditive scn in SceAddDefault)
                    {
                        SubScenesToLoad.Add(SceneManager.LoadSceneAsync(scn.ToString(), LoadSceneMode.Additive));

                    }
                }

                LoadingProgressBar.fillAmount = 10f / 100f;

                //load Scene sampai Done , bukan berarti bisa di find, karna ada bug nya gak bisa di find
                allProgres = LoadingProgressBar.fillAmount;
                float totalLoadProgress = 0;
                float findingProgress = 10f / 100f;
                for (int i = 0; i < SubScenesToLoad.Count; ++i)
                {
                    while (!SubScenesToLoad[i].isDone)
                    {
                        yield return null;
                    }

                    totalLoadProgress++;
                    LoadingProgressBar.fillAmount = allProgres + (totalLoadProgress * (1 - (allProgres + findingProgress)) / (SubScenesToLoad.Count));
                }
                LoadingProgressBar.fillAmount = 100 / 100f;

            }
            //set cam loading true,
            CameraLoading.SetActive(true);

            //if (_isWithLoad)
            {
                // set the active in the ready scene of object to false, sampai nunggu bisa di find
                int currentSceneCount = SceneManager.sceneCount;

                int j = 0;

#if UNITY_EDITOR
                //int k = 0;
                //bool isNoNeedCheck = false;
#endif
                for (int i = 0; i < currentSceneCount; i++)
                {
#if UNITY_EDITOR
                    
                    //if (!isNoNeedCheck && SceneManager.GetSceneAt(i).name == nameScnNoNeed[k])
                    //{ 
                    //    k++;
                    //    if (nameScnNoNeed.Length == k)
                    //        isNoNeedCheck = true;
                    //    break;
                    //}
#endif
                    if (SceneManager.GetSceneAt(i).name == CurSceneCall.SceneAdditive[j].ToString())
                    {
                        foreach (GameObject obj in SceneManager.GetSceneAt(i).GetRootGameObjects())
                        {
                            obj.SetActive(false);
                        }
                    }
                    else if (SceneManager.GetSceneAt(i).name == LibUtilities.SCANE_MANAGER)
                    {
                        continue;
                    }
                    if (i == currentSceneCount - 1)
                    {
                        j++;
                    }
#if UNITY_EDITOR
                    
#endif
                }
            }
            //yield return new WaitForEndOfFrame();
            _isOnload = true;

            yield return null;


        }

        public virtual void LoadScene(List<AsyncOperation> _SubScenesToLoad, LibEdSceneUtilities.ScenesAdditive scn)
        {
            _SubScenesToLoad.Add(SceneManager.LoadSceneAsync(scn.ToString(), LoadSceneMode.Additive));
        }

        public virtual void UnLoadScene(List<AsyncOperation> _SubScenesToLoad, LibEdSceneUtilities.ScenesAdditive scn)
        {
            _SubScenesToLoad.Add(SceneManager.UnloadSceneAsync(scn.ToString()));
        }

        private IEnumerator ChangeState2(LibEdStateUtilities.GameStates changeStateReady, List<LibEdStateUtilities.GameSubStates> changeSubStateReady)
        {
            //change the state to the reeady state
            VirtualStateManager.Instance.CurState = changeStateReady;
            VirtualStateManager.Instance.CurSubStateActive = changeSubStateReady;




            //set the active in the ready scene of object to true, and set active false for camera loading
            int currentSceneCount = SceneManager.sceneCount;
            int j = 0;
            for (int i = 0; i < currentSceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == CurSceneCall.SceneAdditive[j].ToString())
                {
                    foreach (GameObject obj in SceneManager.GetSceneAt(i).GetRootGameObjects())
                    {
                        obj.SetActive(true);
                    }
                }
                else if (SceneManager.GetSceneAt(i).name == LibUtilities.SCANE_MANAGER)
                {
                    continue;
                }

                if (i == currentSceneCount - 1)
                {
                    j++;
                }
            }
            CameraLoading.SetActive(false);
            //Wait to active first before finding
            yield return new WaitForEndOfFrame();

            //set all controller findall ke false, agar tidak bisa masuk diupdate langsung
            for (int i = 0; i < _listControllerDefault.Count; i++)
            {
                _listControllerDefault[i].StateFunc.SetFindAll(false);
            }


            //Active SetActive the New one
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(CurSceneCall.SceneAdditive[0].ToString()));

            //Find object by return yeild till frema
            yield return new WaitForEndOfFrame();

            //ensure to make all controll need to Find is done
            float totalLoadProgress = 0;
            for (int i = 0; i < _listControllerDefault.Count; i++)
            {
                _listControllerDefault[i].CheckingAllfind();
                totalLoadProgress++;
                LoadingProgressBar.fillAmount += (totalLoadProgress / _listControllerDefault.Count);
            }

            // activited animasi loading bar
            _loadingAnimationContent.SetTrigger(LibUtilities.LOADING_TRIGGER_CONTENT.ANIM_END.ToString());
            yield return new WaitForSeconds(LoadingContenTime);
            LoadingContent.GetComponent<CanvasGroup>().alpha = 0;


            // activited animasi transition 
            _loadingAnimationTransition.SetTrigger(LibUtilities.LOADING_TRIGGER_TRANSITION.TST_END.ToString());
            yield return new WaitForSeconds(LoadingTransitionTstTime);
            LoadingTransition.GetComponent<LibMasterTransitionController>().TST_USE.GetComponent<CanvasGroup>().alpha = 0;

            // Done
            LoadingProgressBar.fillAmount = 0;
            SubScenesToLoad.Clear();

            _isOnChangeState = false;
            yield return null;
        }

        #endregion === Change Scenes ===

        #endregion === State Called In Update ===

    }

}