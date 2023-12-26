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
                private static List<string> m_anim_id = new List<string>();
                private static Dictionary<string, Coroutine> m_anims = new Dictionary<string, Coroutine>();
                
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
                public void StartAnim(Anim anim)
                {   
                    m_anim_id.Add(anim.GetName());

                    Coroutine temp = this.StartCoroutine(_StartAnime(anim.GetAnimFunc(),
                                                                     anim.GetDuration(),
                                                                     anim.GetAction(),
                                                                     anim.GetOnComplete(),
                                                                     anim.GetName())
                                                        );
                    
                    m_anims.Add(anim.GetName(), temp);
                    //Debug.Log(m_animes.Count);
                }

                public void StopAnim(string name)
                {
                    m_anim_id.Remove(name);
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
                        if (!m_anim_id.Contains(name))
                        {
                            StopCoroutine(m_anims[name]);
                            m_anims.Remove(name);
                        }
                        Debug.Log(curveFunc(x));
                        action(curveFunc(x));
                        x += step;
                        yield return new WaitForFixedUpdate();
                    }

                    if (onComplete != null) onComplete();
                    Debug.Log(m_anims.Count);
                    m_anim_id.Remove(name);
                    m_anims.Remove(name);
                }
            }

        }
    }
}
