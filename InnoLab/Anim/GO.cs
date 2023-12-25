using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InnoLab
{
    namespace Anim
    {
        namespace GO
        {
            using CurveFunc = Func<float, float>;

            public class GO : MonoBehaviour
            {
                private static GameObject m_gameObject;
                private static GO _instance = null;
                private static List<string> m_anime_id = new List<string>();
                private static Dictionary<string, Coroutine> m_animes = new Dictionary<string, Coroutine>();
                
                public static GO Instance
                {
                    get
                    {
                        if (_instance == null)
                        {
                            m_gameObject = new GameObject("InnoLab Anime");
                            _instance = m_gameObject.AddComponent<GO>();
                        }
                        return _instance;
                    }
                }
                public void Start()
                {
                    DontDestroyOnLoad(m_gameObject);
                }
                public void StartAnim(Anim anime)
                {   
                    m_anime_id.Add(anime.GetName());

                    Coroutine temp = this.StartCoroutine(_StartAnime(anime.GetAnimFunc(),
                                                                     anime.GetDuration(),
                                                                     anime.GetAction(),
                                                                     anime.GetOnComplete(),
                                                                     anime.GetName())
                                                        );
                    
                    m_animes.Add(anime.GetName(), temp);
                    //Debug.Log(m_animes.Count);
                }

                public void StopAnim(string name)
                {
                    m_anime_id.Remove(name);
                }

                private IEnumerator _StartAnime(CurveFunc curveFunc, 
                                                float duration, 
                                                Action<float> action,
                                                Action onComplete,
                                                string name)
                {
                    float deltaTime = Time.fixedDeltaTime;
                    float count = duration / deltaTime;
                    float step = 1 / count;

                    float x = 0;
                    for (int i = 0; i < count; i++)
                    {
                        if (!m_anime_id.Contains(name))
                        {
                            StopCoroutine(m_animes[name]);
                            m_animes.Remove(name);
                        }
                        action(curveFunc(x));
                        x += step;
                        yield return new WaitForFixedUpdate();
                    }

                    if (onComplete != null) onComplete();
                    Debug.Log(m_animes.Count);
                    m_anime_id.Remove(name);
                    m_animes.Remove(name);
                }
            }

        }
    }
}
