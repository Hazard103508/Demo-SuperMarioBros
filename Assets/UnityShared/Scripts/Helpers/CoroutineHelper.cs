using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityShared.Patterns;

namespace UnityShared.Helpers
{
    public class CoroutineHelper : Singleton<CoroutineHelper>
    {
        /// <summary>
        /// Rotate to quaternion in time lapse
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="space"></param>
        /// <param name="targetRotation"></param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        public void RotateTowardsTarget(Transform transform, Space space, Quaternion targetRotation, float duration, bool isPausable = true, Action callBack = null) =>
            StartCoroutine(CO_RotateTowardsTarget(transform, space, targetRotation, duration));
        /// <summary>
        /// Rotate to quaternion in time lapse
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="space"></param>
        /// <param name="targetRotation"></param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public IEnumerator CO_RotateTowardsTarget(Transform transform, Space space, Quaternion targetRotation, float duration, bool isPausable = true, Action callBack = null)
        {
            float timePassed = 0.0f;

            bool worldSpace = space == Space.World;

            Quaternion startRotation = worldSpace ? transform.rotation : transform.localRotation;

            while (timePassed < duration)
            {
                if (transform == null)
                {
                    yield break;
                }

                var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;

                timePassed += deltaTime;

                float factor = timePassed / duration;

                if (space == Space.Self)
                {
                    transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, factor);
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(startRotation, targetRotation, factor);
                }
                yield return null;
            }


            if (transform != null)
            {
                if (space == Space.Self)
                {
                    transform.localRotation = targetRotation;
                }
                else
                {
                    transform.rotation = targetRotation;
                }
            }

            callBack?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="initialValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        public void ColorLerp(Image uiElement, Color initialValue, Color endValue, float duration, bool isPausable = true, Action callBack = null) =>
            StartCoroutine(CO_ColorLerp(uiElement, initialValue, endValue, duration, isPausable));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="initialValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public IEnumerator CO_ColorLerp(Image uiElement, Color initialValue, Color endValue, float duration, bool isPausable = true, Action callBack = null)
        {
            float timePassed = 0.0f;

            while (timePassed < duration)
            {
                if (uiElement == null)
                {
                    yield break;
                }

                var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;

                timePassed += deltaTime;

                float factor = timePassed / duration;

                uiElement.color = Color.Lerp(initialValue, endValue, factor);

                yield return null;
            }

            if (uiElement != null)
            {
                uiElement.color = endValue;
            }
            callBack?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="initialValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        public void ColorLerp(SpriteRenderer uiElement, Color initialValue, Color endValue, float duration, bool isPausable = true, Action callBack = null) =>
            StartCoroutine(CO_ColorLerp(uiElement, initialValue, endValue, duration, isPausable));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="initialValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public IEnumerator CO_ColorLerp(SpriteRenderer uiElement, Color initialValue, Color endValue, float duration, bool isPausable = true, Action callBack = null)
        {
            float timePassed = 0.0f;

            while (timePassed < duration)
            {
                if (uiElement == null)
                {
                    yield break;
                }

                var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;

                timePassed += deltaTime;

                float factor = timePassed / duration;

                uiElement.color = Color.Lerp(initialValue, endValue, factor);

                yield return null;
            }

            if (uiElement != null)
            {
                uiElement.color = endValue;
            }

            callBack?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="initialValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        public void MoveX(Transform transform, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null) =>
            StartCoroutine(CO_MoveX(transform, initialValue, endValue, duration, isPausable));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="initialValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public IEnumerator CO_MoveX(Transform transform, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null)
        {
            float timePassed = 0.0f;

            while (timePassed < duration)
            {
                if (transform == null)
                {
                    yield break;
                }

                var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;

                timePassed += deltaTime;

                float factor = timePassed / duration;

                transform.position = new Vector3(Mathf.Lerp(initialValue, endValue, factor), transform.position.y, transform.position.z);

                yield return null;
            }

            if (transform != null)
            {
                transform.position = new Vector3(endValue, transform.position.y, transform.position.z);
            }

            callBack?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="initialValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        public void MoveY(Transform transform, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null) =>
            StartCoroutine(CO_MoveY(transform, initialValue, endValue, duration, isPausable));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="initialValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public IEnumerator CO_MoveY(Transform transform, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null)
        {
            float timePassed = 0.0f;

            while (timePassed < duration)
            {
                if (transform == null)
                {
                    yield break;
                }

                var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;

                timePassed += deltaTime;

                float factor = timePassed / duration;

                transform.position = new Vector3(transform.position.x, Mathf.Lerp(initialValue, endValue, factor), transform.position.z);

                yield return null;
            }

            if (transform != null)
            {
                transform.position = new Vector3(transform.position.x, endValue, transform.position.z);
            }

            callBack?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="initialValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        public void MoveZ(Transform transform, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null) =>
            StartCoroutine(CO_MoveZ(transform, initialValue, endValue, duration, isPausable));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="initialValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public IEnumerator CO_MoveZ(Transform transform, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null)
        {
            float timePassed = 0.0f;

            while (timePassed < duration)
            {
                if (transform == null)
                {
                    yield break;
                }

                var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;

                timePassed += deltaTime;

                float factor = timePassed / duration;

                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(initialValue, endValue, factor));

                yield return null;
            }

            if (transform != null)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, endValue);
            }

            callBack?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="initialValue">Alpha range (value from 0 to 1)</param>
        /// <param name="endValue">Alpha range (value from 0 to 1)</param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        public void FadeUIElement(CanvasGroup uiElement, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null) =>
            StartCoroutine(CO_FadeUIElement(uiElement, initialValue, endValue, duration, isPausable));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="initialValue">Alpha range (value from 0 to 1)</param>
        /// <param name="endValue">Alpha range (value from 0 to 1)</param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public IEnumerator CO_FadeUIElement(CanvasGroup uiElement, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null)
        {
            endValue = Mathf.Clamp(endValue, 0, 1);

            float timePassed = 0.0f;

            while (timePassed < duration)
            {
                if (uiElement == null)
                {
                    yield break;
                }

                var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;

                timePassed += deltaTime;

                float factor = timePassed / duration;

                uiElement.alpha = Mathf.Lerp(initialValue, endValue, factor);

                yield return null;
            }

            if (uiElement != null)
            {
                uiElement.alpha = endValue;
            }

            callBack?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="initialValue">Alpha range (value from 0 to 1)</param>
        /// <param name="endValue">Alpha range (value from 0 to 1)</param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        public void FadeUIElement(TextMeshPro uiElement, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null) =>
            StartCoroutine(CO_FadeUIElement(uiElement, initialValue, endValue, duration, isPausable));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="initialValue">Alpha range (value from 0 to 1)</param>
        /// <param name="endValue">Alpha range (value from 0 to 1)</param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public IEnumerator CO_FadeUIElement(TextMeshPro uiElement, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null)
        {
            endValue = Mathf.Clamp(endValue, 0, 1);

            float timePassed = 0.0f;

            while (timePassed < duration)
            {
                if (uiElement == null)
                {
                    yield break;
                }

                var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;

                timePassed += deltaTime;

                float factor = timePassed / duration;

                uiElement.alpha = Mathf.Lerp(initialValue, endValue, factor);

                yield return null;
            }

            if (uiElement != null)
            {
                uiElement.alpha = endValue;
            }

            callBack?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="initialValue">Alpha range (value from 0 to 1)</param>
        /// <param name="endValue">Alpha range (value from 0 to 1)</param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        public void FadeColor(Image uiElement, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null) =>
            StartCoroutine(CO_FadeColor(uiElement, initialValue, endValue, duration, isPausable));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="initialValue">Alpha range (value from 0 to 1)</param>
        /// <param name="endValue">Alpha range (value from 0 to 1)</param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public IEnumerator CO_FadeColor(Image uiElement, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null)
        {
            endValue = Mathf.Clamp(endValue, 0, 1);

            float timePassed = 0.0f;

            while (timePassed < duration)
            {
                if (uiElement == null)
                {
                    yield break;
                }

                var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;

                timePassed += deltaTime;

                float factor = timePassed / duration;

                uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b, Mathf.Lerp(initialValue, endValue, factor));

                yield return null;
            }

            if (uiElement != null)
            {
                uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b, endValue);
            }

            callBack?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="initialValue">Alpha range (value from 0 to 1)</param>
        /// <param name="endValue">Alpha range (value from 0 to 1)</param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        public void FadeColor(SpriteRenderer uiElement, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null) =>
            StartCoroutine(CO_FadeColor(uiElement, initialValue, endValue, duration, isPausable));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="initialValue">Alpha range (value from 0 to 1)</param>
        /// <param name="endValue">Alpha range (value from 0 to 1)</param>
        /// <param name="duration"></param>
        /// <param name="isPausable"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public IEnumerator CO_FadeColor(SpriteRenderer uiElement, float initialValue, float endValue, float duration, bool isPausable = true, Action callBack = null)
        {
            endValue = Mathf.Clamp(endValue, 0, 1);

            float timePassed = 0.0f;

            while (timePassed < duration)
            {
                if (uiElement == null)
                {
                    yield break;
                }

                var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;

                timePassed += deltaTime;

                float factor = timePassed / duration;

                uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b, Mathf.Lerp(initialValue, endValue, factor));

                yield return null;
            }

            if (uiElement != null)
            {
                uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b, endValue);
            }

            callBack?.Invoke();
        }

        public void Timer(float timeLapse, Action callBack, Action<float> actionByFrame = null, bool isPausable = true) =>
            StartCoroutine(CO_Timer(timeLapse, callBack, actionByFrame, isPausable));

        public IEnumerator CO_Timer(float timeLapse, Action callBack, Action<float> actionByFrame = null, bool isPausable = true)
        {
            float timePassed = 0.0f;

            while (timePassed < timeLapse)
            {
                var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;

                timePassed += deltaTime;

                actionByFrame?.Invoke(timePassed);

                yield return null;
            }

            callBack?.Invoke();
        }

        public void Timer(float start, float end, Action callBack, Action<float> actionBySecond = null, Action<float> actionByFrame = null, bool isPausable = true) =>
            StartCoroutine(CO_Timer(start, end, callBack, actionBySecond, actionByFrame, isPausable));

        public IEnumerator CO_Timer(float start, float end, Action callBack, Action<float> actionBySecond = null, Action<float> actionByFrame = null, bool isPausable = true)
        {
            bool isIncreasingValue = start < end;

            float timePassed = start;

            float seconds = start;

            if (isIncreasingValue)
            {
                while (timePassed < end)
                {
                    var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;

                    timePassed += deltaTime;

                    if (timePassed >= seconds + 1)
                    {
                        actionBySecond?.Invoke(timePassed);
                        seconds++;
                    }

                    actionByFrame?.Invoke(timePassed);

                    yield return null;
                }
            }
            else
            {
                while (timePassed > end)
                {
                    var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;

                    timePassed -= deltaTime;

                    if (timePassed <= seconds - 1)
                    {
                        actionBySecond?.Invoke(timePassed);
                        seconds--;
                    }

                    actionByFrame?.Invoke(timePassed);

                    yield return null;
                }
            }

            callBack?.Invoke();
        }

        public void InvokeAtEndOfFrame(Action callBack) =>
            StartCoroutine(CO_InvokeAtEndOfFrame(callBack));
        public IEnumerator CO_InvokeAtEndOfFrame(Action callBack)
        {
            yield return new WaitForEndOfFrame();
            callBack.Invoke();
        }

        public void InvokeAtNextFrame(Action callBack) =>
            StartCoroutine(CO_InvokeAtNextFrame(callBack));
        public IEnumerator CO_InvokeAtNextFrame(Action callBack)
        {
            yield return null;
            callBack.Invoke();
        }

        public void InvokeAfterACertainNumberOfFrames(int frameLapse, Action callBack) =>
            StartCoroutine(CO_invokeAfterACertainNumberOfFrames(frameLapse, callBack));
        public IEnumerator CO_invokeAfterACertainNumberOfFrames(int frameLapse, Action callBack)
        {
            int count = 0;
            while (true)
            {
                count++;
                yield return null;

                if (count >= frameLapse)
                {
                    callBack.Invoke();
                    yield break;
                }
            }
        }

        public void Conditional(Func<bool> condition, Action callBack, Action actionByFrame = null, Action actionBySecond = null, bool isPausable = true) =>
            StartCoroutine(CO_Conditional(condition, callBack, actionByFrame, actionBySecond, isPausable));
        public IEnumerator CO_Conditional(Func<bool> condition, Action callBack, Action actionByFrame = null, Action actionBySecond = null, bool isPausable = true)
        {
            float steps = 1;
            float timePassed = 0;

            while (!condition())
            {
                var deltaTime = isPausable ? Time.deltaTime : Time.unscaledDeltaTime;
                timePassed += deltaTime;

                if (timePassed >= steps)
                {
                    actionBySecond?.Invoke();
                    steps++;
                }

                actionByFrame?.Invoke();

                yield return null;
            }

            callBack?.Invoke();
        }
    }
}