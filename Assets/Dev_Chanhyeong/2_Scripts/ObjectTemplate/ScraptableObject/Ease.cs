using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using ObjectTemplate.Events;
using ObjectTemplate.Interface;

namespace ObjectTemplate.ScraptableObject{
    // XXX : 나머지 코드들 수정하기
    [System.Serializable]
    [CreateAssetMenu(fileName = "Ease", menuName = "Ease", order = 0)]
    public class Ease : ScriptableObject {

        public AnimationCurve ease = null;
        public float endTime = 1f;

        /// <summary>
        /// Ease가 시작할때 실행되는 이벤트입니다.
        /// </summary>
        /// <returns></returns>
        public UnityEvent easeEnter = new UnityEvent();

        /// <summary>
        /// Ease가 끝날때 실행되는 이벤트입니다.
        /// </summary>
        /// <returns></returns>
        public UnityEvent easeExit = new UnityEvent();

        /// <summary>
        /// Ease가 실행중일 때 실행되는 이벤트입니다..
        /// Animation Curve의 곡선 값을 전달합니다.
        /// </summary>
        /// <returns>Animation Curve의 t값을 반환합니다.</returns>
        public FloatEvent easeExcute = new FloatEvent();

        public void AddEaseActions(UnityAction onEnter, UnityAction<float> onExcute, UnityAction onExit){
            easeEnter.AddListener(onEnter);
            easeExcute.AddListener(onExcute);
            easeExit.AddListener(onExit);
        }

        public void AddEaseActions(IOnEase onEase){
            easeEnter.AddListener(onEase.OnEaseEnter);
            easeExcute.AddListener(onEase.OnEaseExcute);
            easeExit.AddListener(onEase.OnEaseExit);
        }

        public void AddEnterAction(UnityAction onEnter) => easeEnter.AddListener(onEnter);
        public void AddExcuteAction(UnityAction<float> onExcute) => easeExcute.AddListener(onExcute);
        public void AddExitAction(UnityAction onExit) => easeExit.AddListener(onExit);

        public void AddEnterAction(IOnEaseEnter onEnter) => easeEnter.AddListener(onEnter.OnEaseEnter);
        public void AddExcuteAction(IOnEaseExcute onExcute) => easeExcute.AddListener(onExcute.OnEaseExcute);
        public void AddExitAction(IOnEaseExit onExit) => easeExit.AddListener(onExit.OnEaseExit);

        public void RemoveEaseActions(UnityAction onEnter, UnityAction<float> onExcute, UnityAction onExit){
            easeEnter.RemoveListener(onEnter);
            easeExcute.RemoveListener(onExcute);
            easeExit.RemoveListener(onExit);
        }

        public void RemoveEaseActions(IOnEase onEase){
            easeEnter.RemoveListener(onEase.OnEaseEnter);
            easeExcute.RemoveListener(onEase.OnEaseExcute);
            easeExit.RemoveListener(onEase.OnEaseExit);
        }

        public void RemoveEnterAction(UnityAction onEnter) => easeEnter.RemoveListener(onEnter);
        public void RemoveExcuteAction(UnityAction<float> onExcute) => easeExcute.RemoveListener(onExcute);
        public void RemoveExitAction(UnityAction onExit) => easeExit.RemoveListener(onExit);

        public void RemoveEnterAction(IOnEaseEnter onEnter) => easeEnter.RemoveListener(onEnter.OnEaseEnter);
        public void RemoveExcuteAction(IOnEaseExcute onExcute) => easeExcute.RemoveListener(onExcute.OnEaseExcute);
        public void RemoveExitAction(IOnEaseExit onExit) => easeExit.RemoveListener(onExit.OnEaseExit);

        /// <summary>
        /// Ease를 시작하는 함수
        /// </summary>
        /// <param name="reverse">reverse값이 true가 된다면 t값이 역행</param>
        /// <returns></returns>
        public IEnumerator DoEase(bool reverse = false){
            float time = 0f;
            float ratio;

            easeEnter.Invoke();

            while(time <= endTime){
                time += Time.deltaTime;
                ratio = reverse ? (1 - time / endTime) : time / endTime;

                if (ratio > 1f) ratio = 1f;
                if (ratio < 0f) ratio = 0f; 

                easeExcute.Invoke(ease.Evaluate(ratio));

                yield return null;
            }

            easeExit.Invoke();
        }
        
        // FIXME : 이벤트 형식의 함수로 재지정
        public IEnumerator DoEase(Action<float> excutive){
            float time = 0f;
            float easeValue = 0f;

            easeEnter.Invoke();

            while(time <= endTime){
                time += Time.deltaTime;
                easeValue = ease.Evaluate(time * (1 / endTime));

                excutive(easeValue);
                easeExcute.Invoke(easeValue);

                yield return null;
            }

            easeExit.Invoke();
        }

        // FIXME : 이벤트 형식의 함수로 재지정
        public IEnumerator DoEase(Action declare, Action<float> excutive){
            float time = 0f;
            float easeValue = 0f;

            declare();
            easeEnter.Invoke();

            while(time <= endTime){
                time += Time.deltaTime;
                easeValue = ease.Evaluate(time * (1 / endTime));

                excutive(easeValue);
                easeExcute.Invoke(easeValue);

                yield return null;
            }

            easeExit.Invoke();
        }

        // FIXME : 이벤트 형식의 함수로 재지정
        public IEnumerator DoEase(Action<float> excutive, Action end){
            float time = 0f;
            float easeValue = 0f;

            easeEnter.Invoke();

            while(time <= endTime){
                time += Time.deltaTime;
                easeValue = ease.Evaluate(time * (1 / endTime));

                excutive(easeValue);
                easeExcute.Invoke(easeValue);

                yield return null;
            }

            end();
            easeExit.Invoke();
        }

        // FIXME : 이벤트 형식의 함수로 재지정
        public IEnumerator DoEase(Action declare, Action<float> excutive, Action end){
            float time = 0f;
            float easeValue = 0f;

            declare();
            easeEnter.Invoke();

            while(time <= endTime){
                time += Time.deltaTime;
                easeValue = ease.Evaluate(time * (1 / endTime));

                excutive(easeValue);
                easeExcute.Invoke(easeValue);

                yield return null;
            }

            end();
            easeExit.Invoke();
        }
    }
}