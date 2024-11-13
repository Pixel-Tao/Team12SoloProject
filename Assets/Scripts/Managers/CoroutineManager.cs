using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoroutineState
{
    Running, // 실행 중
    Paused, // 일시 정지
    Stopped // 중지됨
}

public class CoroutineManager : IManager
{
    // 코루틴 실행을 위한 빈 MonoBehaviour 클래스
    private class CoroutineExecutor : MonoBehaviour
    {
    }

    // 코루틴을 실행하기 위한 MonoBehaviour
    private MonoBehaviour coroutineExecutor;
    // 코루틴을 관리하기 위한 딕셔너리 (코루틴 키, 코루틴)
    private Dictionary<string, Coroutine> coroutineDictionary = new Dictionary<string, Coroutine>();

    // 코루틴의 상태를 추적하기 위한 딕셔너리 (코루틴 키, 상태)
    private Dictionary<string, CoroutineState> coroutineStates = new Dictionary<string, CoroutineState>();

    public void Init()
    {
        // 코루틴 실행을 위한 GameObject와 MonoBehaviour 생성
        GameObject obj = new GameObject("@CoroutineExecutor");
        Object.DontDestroyOnLoad(obj); // 씬 전환 시 파괴되지 않도록 설정
        coroutineExecutor = obj.AddComponent<CoroutineExecutor>();
    }
    public void Clear()
    {
        StopAllCoroutines();
        if (coroutineExecutor != null)
            Object.Destroy(coroutineExecutor.gameObject);
    }

    /// <summary>
    /// 코루틴 시작
    /// </summary>
    /// <param name="key">코루틴을 구분하는 키</param>
    /// <param name="routine">실행할 코루틴</param>
    public void StartCoroutine(string key, IEnumerator routine)
    {
        if (coroutineDictionary.ContainsKey(key))
        {
            Debug.LogWarning($"Coroutine '{key}' is already running");
            return;
        }

        coroutineStates[key] = CoroutineState.Running;
        Coroutine coroutine = coroutineExecutor.StartCoroutine(RunCoroutine(key, routine));
        coroutineDictionary.Add(key, coroutine);
    }

    /// <summary>
    /// 코루틴 중지
    /// </summary>
    /// <param name="key">중지할 코루틴의 키</param>
    public void StopCoroutine(string key)
    {
        if (coroutineDictionary.TryGetValue(key, out Coroutine coroutine))
        {
            coroutineExecutor.StopCoroutine(coroutine);
            coroutineDictionary.Remove(key);
            coroutineStates[key] = CoroutineState.Stopped;
        }
    }

    /// <summary>
    /// 코루틴 일시 정지
    /// </summary>
    /// <param name="key">일시 정지할 코루틴의 키</param>
    public void PauseCoroutine(string key)
    {
        if (coroutineStates.ContainsKey(key))
        {
            coroutineStates[key] = CoroutineState.Paused;
        }
    }

    /// <summary>
    /// 코루틴 재개
    /// </summary>
    /// <param name="key">재개할 코루틴의 키</param>
    public void ResumeCoroutine(string key)
    {
        if (coroutineStates.ContainsKey(key))
        {
            coroutineStates[key] = CoroutineState.Running;
        }
    }

    /// <summary>
    /// 모든 코루틴 중지
    /// </summary>
    public void StopAllCoroutines()
    {
        foreach (var coroutine in coroutineDictionary.Values)
        {
            coroutineExecutor.StopCoroutine(coroutine);
        }
        coroutineDictionary.Clear();
        coroutineStates.Clear();
    }

    // 코루틴 실행 래퍼 메서드
    private IEnumerator RunCoroutine(string key, IEnumerator routine)
    {
        while (coroutineStates[key] != CoroutineState.Stopped)
        {
            if (coroutineStates[key] == CoroutineState.Paused)
            {
                // 일시 정지 상태일 때는 대기
                yield return null;
            }
            else
            {
                // 코루틴의 다음 단계로 진행
                if (routine.MoveNext())
                {
                    yield return routine.Current;
                }
                else
                {
                    // 코루틴이 완료되면 루프 종료
                    break;
                }
            }
        }
        // 코루틴이 완료되거나 중지되면 관리 딕셔너리에서 제거
        coroutineDictionary.Remove(key);
        coroutineStates.Remove(key);
    }
}