
using System.Collections.Generic;
using System;
using UnityEngine;

namespace InnoLab
{
    namespace Anim
    {
        namespace Curve
        {
            using CurveFunc = Func<float, float>;
            public enum CurveType
            {
                LinearIn,
                LinearOut,
                DoubleIn,
                DoubleOut,
                EaseInSine,
                EaseOutSine,
            }
            class Curve
            {
                /// <summary>
                /// 混合两个曲线
                /// </summary>
                /// <param name="f1">曲线1</param>
                /// <param name="f2">曲线2</param>
                /// <param name="t">\alpha</param>
                /// <returns></returns>
                public static CurveFunc Blend(CurveFunc f1, CurveFunc f2, float t)
                {
                    return (float x) => { return f1(x) * (1 - t) + f2(x) * t; };
                }
                
                /// <summary>
                /// 三次贝塞尔曲线
                /// </summary>
                /// <param name="p0"></param>   控制点p1 x
                /// <param name="p1"></param>   控制点p1 y
                /// <param name="p2"></param>   控制点p2 x
                /// <param name="p3"></param>   控制点p2 y
                /// <returns></returns>
                public static CurveFunc CubicBezier(float p0, float p1, float p2, float p3)
                {
                    return (float t) =>
                    {
                        // TODO replace with recursive_bezier (de Casteljau's algorithm)
                        return CubicBezier(p1, p3)(CubicBezier(p0,p2)(t)); // y(x(t))
                    };
                }

                private static CurveFunc CubicBezier(float p0, float p1)
                {
                    return (float t) =>
                    {
                        return (float)(p0 * 3 * Math.Pow(1 - t, 2) * t + p1 * 3 * Math.Pow(t, 2) * (1 - t) +  Math.Pow(t, 3));
                    };
                }

                private static Vector2 recursive_bezier(List<Vector2> control_points, float t)
                {
                    // TODO Implement de Casteljau's algorithm

                }

                /// <summary>
                /// 获取预设曲线
                /// </summary>
                /// <param name="curveType"></param>
                /// <returns></returns>
                public static CurveFunc GetFunc(CurveType curveType)
                {
                    return curve[curveType];
                }

                private static readonly Dictionary<CurveType, CurveFunc> curve = new Dictionary<CurveType, CurveFunc>()
            {
                { CurveType.LinearIn, (float t) => { return t; } },
                { CurveType.LinearOut, (float t) => { return 1 - t; } },
                { CurveType.DoubleIn, (float t) => { return t*t; } },
                { CurveType.DoubleOut, (float t) => { return 1 - t*t; } },
                { CurveType.EaseInSine, (float t) => { return CubicBezier(0.47f, 0, 0.745f, 0.715f)(t); } },
                { CurveType.EaseOutSine, (float t) => { return CubicBezier(0.39f, 0.575f, 0.565f, 1.0f)(t); } },
            };
            }
        }

    }
}