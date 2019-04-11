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

using UnityEngine;

/// <summary>
/// Helper class. A Simple Loading circle.
/// </summary>
public class LoadingCircle : MonoBehaviour {

    private RectTransform mRectComponent;
    private readonly float mRotatingSpeed = 200f;

    /// <summary>
    /// Start this instance.
    /// </summary>
    private void Start() {
        mRectComponent = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    private void Update() {
        mRectComponent.Rotate(0f, 0f, mRotatingSpeed * Time.deltaTime);
    }
}