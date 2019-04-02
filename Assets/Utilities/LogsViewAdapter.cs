using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LogsViewAdapter : MonoBehaviour
{
    public RectTransform prefab;
    public ScrollRect scrollView;
    public RectTransform content;
    List<RawLogView> mListView = new List<RawLogView>();
    public event Action OnFirstLogLine;

    void Start()
    {
    }

    public void addLogLine(String textToAdd)
    {
        var instance = Instantiate(prefab.gameObject) as GameObject;
        instance.transform.SetParent(content, false);
        RawLogView view = InitilalizeItemView(instance, textToAdd);
        if(mListView.Count == 0)
        {
            OnFirstLogLine.Invoke();
        }
        mListView.Add(view);

    }

    RawLogView InitilalizeItemView(GameObject viewGameOBject, String textToAdd)
    {
        RawLogView view = new RawLogView(viewGameOBject.transform, textToAdd);
        return view;
    }

    public class RawLogView
    {
        public Text mTextView;
        public RawLogView(Transform rootView,String text)
        {
            mTextView = rootView.Find("LogText").GetComponent<Text>();
            mTextView.text = text;
        }

    }


    public void clearAllList()
    {
        mListView.Clear();
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

    }
}
