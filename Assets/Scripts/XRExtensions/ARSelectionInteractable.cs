using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
//處理輸入事件的組件 //可用於管理事件 such as raycast
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.XR.ARSubsystems;
namespace UnityEngine.XR.Interaction.Toolkit.AR
{
public class ARSelectionInteractable : ARBaseGestureInteractable
{
        //public Button Delete_State;
        private int onstate=0;
        private int i=0;
        ////進入DestroyItemList所需要依照name來判斷進入哪種判斷式中
        private string name;
        [SerializeField, Tooltip("The visualization GameObject that will become active when the object is selected.")]
        GameObject m_SelectionVisualization;
        /// <summary>
        /// The visualization <see cref="GameObject"/> that will become active when the object is selected.
        /// </summary>
        public GameObject selectionVisualization
        {
            get => m_SelectionVisualization;
            set => m_SelectionVisualization = value;
        }

        bool m_GestureSelected;
        
        /// <inheritdoc />
        public override bool IsSelectableBy(IXRSelectInteractor interactor) => interactor is ARGestureInteractor && m_GestureSelected;

        /// <inheritdoc />
        protected override bool CanStartManipulationForGesture(TapGesture gesture) => true;
        /// <inheritdoc />
        protected override void OnEndManipulation(TapGesture gesture)
        {
            //var changetext = Delete_State.GetComponentInChildren<Text>();
            base.OnEndManipulation(gesture);
            if (gesture.isCanceled)
                return;
            if (gestureInteractor == null)
                return;
            if (gesture.targetObject == gameObject)
            {
                m_GestureSelected = !m_GestureSelected;
                //當被選擇後在點第二下時，物件將會消失
                if(onstate==1)
                {   
                    if(gesture.targetObject.tag == "cabinet1")
                    {
                        name="cabinet1";
                        Destroy(gesture.targetObject);
                        DataHandler.Instance.DestroyItemList(name);
                    }
                    else if(gesture.targetObject.tag == "chair1")
                    {
                        name="chair1";
                        Destroy(gesture.targetObject);
                        DataHandler.Instance.DestroyItemList(name);
                    }
                    else if(gesture.targetObject.tag == "chair2")
                    {
                        name="chair2";
                        Destroy(gesture.targetObject);
                        DataHandler.Instance.DestroyItemList(name);
                    }
                    else if(gesture.targetObject.tag == "chair3")
                    {
                        
                        name="chair3";
                        Destroy(gesture.targetObject);
                        DataHandler.Instance.DestroyItemList(name);
                    }
                    else if(gesture.targetObject.tag == "chair4")
                    {
                        name="chair4";
                        Destroy(gesture.targetObject);
                        DataHandler.Instance.DestroyItemList(name);
                    }
                    else if(gesture.targetObject.tag == "sofa1")
                    {
                        name="sofa1";
                        Destroy(gesture.targetObject);
                        DataHandler.Instance.DestroyItemList(name);
                    }
                    else if(gesture.targetObject.tag == "sofa2")
                    {
                        name="sofa2";
                        Destroy(gesture.targetObject);
                        DataHandler.Instance.DestroyItemList(name);
                    }
                    else if(gesture.targetObject.tag == "sofa3")
                    {
                        name="sofa3";
                        Destroy(gesture.targetObject);
                        DataHandler.Instance.DestroyItemList(name);
                    }
                    else if(gesture.targetObject.tag == "sofa4")
                    {
                        name="sofa4";
                        Destroy(gesture.targetObject);
                        DataHandler.Instance.DestroyItemList(name);
                    }
                    else if(gesture.targetObject.tag == "table1")
                    {
                        name="table1";
                        Destroy(gesture.targetObject);
                        DataHandler.Instance.DestroyItemList(name);
                    }
                    else if(gesture.targetObject.tag == "table2")
                    {
                        name="table2";
                        Destroy(gesture.targetObject);
                        DataHandler.Instance.DestroyItemList(name);
                    }
                    onstate=0;
                }
                onstate =1;   
            }
            else
            {
                m_GestureSelected = false;
                onstate=0;
            }     
        }
        /// <inheritdoc />
        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            base.OnSelectEntering(args);
            if (m_SelectionVisualization != null)
                m_SelectionVisualization.SetActive(true);
        }

        /// <inheritdoc />
        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            base.OnSelectExiting(args);
            if (m_SelectionVisualization != null)
                m_SelectionVisualization.SetActive(false);
        }
}
}
