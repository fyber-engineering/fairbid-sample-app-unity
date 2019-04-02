using System;
using UnityEngine;
using UnityEngine.UI;

public class Animation
{

    public Button mRequestAdButton;
    public Button mShowAdButton;
    public Image mSpinnerBackground;
    public Image mSpinnerProgress;
    public Image mBackButton;
    private Button mCleanCallbackListButton;
    private LogsViewAdapter mLogsAdapter;
    public Toggle mTopToggle, mBottomToggle;

    public LogsViewAdapter LogsAdapter
    {
        get => mLogsAdapter;
        set
        {
            if (value != null)
            {
                mLogsAdapter = value;
                mLogsAdapter.OnFirstLogLine += () => mCleanCallbackListButton.interactable = true;
            }
        }
    }

    public Button CleanCllBackListButton
    {
        get => mCleanCallbackListButton;
        set
        {
            if (value != null)
            {
                this.mCleanCallbackListButton = value;
                mCleanCallbackListButton.interactable = false;
                mCleanCallbackListButton.onClick.AddListener(OnCleanCallbacksList);
            }
        }
    }

    public void startRequestAnimation()
    {
        setSpinnerState(true);
        setShowButtonState(false);
    }

    public void resetAnimation()
    {
        setSpinnerState(false);
        setShowButtonState(false);
    }

    public void onAdAvailableAnimation()
    {
        setSpinnerState(false);
        setShowButtonState(true);

    }

    private void setSpinnerState(bool state)
    {
        this.mSpinnerBackground.enabled = state;
        this.mSpinnerProgress.enabled = state;
    }

    private void setShowButtonState(bool state)
    {
        mShowAdButton.interactable = state;
    }

    private void OnCleanCallbacksList()
    {
        Debug.Log("OnCleanCallbacksList");
        mCleanCallbackListButton.interactable = false;
        mLogsAdapter.clearAllList();
    }

    public void addLog(String log)
    {
        this.mLogsAdapter.addLogLine(DateTime.Now.ToString("HH:mm:ss") +" - " + log);
    }

    public bool isTopToggleSelcted()
    {
        if(mTopToggle != null)
        {
            return mTopToggle.isOn;
        }
        return false;
    }
}
