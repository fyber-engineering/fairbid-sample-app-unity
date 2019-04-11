//
//  Copyright 2019  Fyber N.V
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// Logs view adapter.
/// Helper class to manage the list of callbacks displayed on screen.
/// </summary>
public class LogsViewAdapter : MonoBehaviour {

    /// <summary>
    /// The log line.
    /// </summary>
    public RectTransform mLogLine;
    /// <summary>
    /// The scroll view.
    /// </summary>
    public ScrollRect mScrollView;
    /// <summary>
    /// The content
    /// </summary>
    public RectTransform mContent;
    /// <summary>
    /// The list of views representing the callbacks/logs
    /// </summary>
    List<RawLogView> mListView = new List<RawLogView>();
    /// <summary>
    /// Occurs when the first log line is added
    /// </summary>
    public event Action OnFirstLogLine;

    /// <summary>
    /// Adds the log line.
    /// </summary>
    /// <param name="textToAdd">the text to add.</param>
    public void addLogLine(String textToAdd) {
        var instance = Instantiate(mLogLine.gameObject) as GameObject;
        instance.transform.SetParent(mContent, false);
        RawLogView view = InitilalizeItemView(instance, textToAdd);
        if (mListView.Count == 0) {
            OnFirstLogLine.Invoke();
        }
        mListView.Add(view);
    }

    /// <summary>
    /// Initilalizes the item view.
    /// </summary>
    /// <returns>The item view.</returns>
    /// <param name="viewGameOBject">The game object.</param>
    /// <param name="textToAdd">Text to add.</param>
    RawLogView InitilalizeItemView(GameObject viewGameOBject, String textToAdd) {
        RawLogView view = new RawLogView(viewGameOBject.transform, textToAdd);
        return view;
    }

    /// <summary>
    /// Raw log view representation
    /// </summary>
    public class RawLogView {
        /// <summary>
        /// The text view.
        /// </summary>
        public Text mTextView;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:LogsViewAdapter.RawLogView"/> class.
        /// </summary>
        /// <param name="rootView">Root view.</param>
        /// <param name="text">Text.</param>
        public RawLogView(Transform rootView, String text) {
            mTextView = rootView.Find("LogText").GetComponent<Text>();
            mTextView.text = text;
        }

    }

    /// <summary>
    /// Clears all the logs.
    /// </summary>
    public void clearLogList() {
        mListView.Clear();
        foreach (Transform child in mContent) {
            Destroy(child.gameObject);
        }

    }
}
