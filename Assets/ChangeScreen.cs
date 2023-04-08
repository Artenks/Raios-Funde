using System.Collections.Generic;
using UnityEngine;

public class ChangeScreen : MonoBehaviour
{
    public Animator Animator;

    public GameObject ObjectToAppear;
    public GameObject ObjectToDisappear;

    public bool AppearList;
    public List<GameObject> ObjectsToAppear;

    public bool DisappearList;
    public List<GameObject> ObjectsToDesappear;


    public bool UsesFinishReturn;
    public FinishReturn FinishReturn;

    public float TimeToAwait;
    public TimeForUpdate Timer;
    private void TimerToChange()
    {
        if (Timer.StartTheCount(TimeToAwait))
        {
            if (!UsesFinishReturn)
            {
                if (AppearList)
                {
                    foreach (GameObject obj in ObjectsToAppear)
                    {
                        obj.SetActive(true);
                    }
                }
                else
                {
                    ObjectToAppear.SetActive(true);
                }
            }
            else
            {
                FinishReturn.OnClickReturn();
            }
            if (DisappearList)
            {
                foreach (GameObject obj in ObjectsToDesappear)
                {
                    obj.SetActive(false);
                }
            }
            else
            {
                ObjectToDisappear.SetActive(false);

            }

            this.GetComponent<ChangeScreen>().enabled = false;
        }
    }

    private void OnEnable()
    {
        Animator.SetTrigger("Exit");
    }

    private void Update()
    {
        TimerToChange();
    }
}
