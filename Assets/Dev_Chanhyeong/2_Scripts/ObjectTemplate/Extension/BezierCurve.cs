using UnityEngine;

namespace ObjectTemplate.Extention{
    // TODO : 베지어 커브 에디터 제작해보기
    // FIXME : 베지어 커브 스크립트 함수명 및 활용할수 있도록 작성하기
    public class BezierCurve
    {
        // 2차 베지어 곡선
        public static Vector3 GetPointOnBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            Vector3 d = Mathf.Pow(1 - t, 2) * p0 + (2 * t) * (1 - t) * p1 + Mathf.Pow(t, 2) * p2; 
        
            return d;
        }

        // 3차 베지어 곡선
        public static Vector3 GetPointOnBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            Vector3 a = Vector3.Lerp(p0, p1, t);
            Vector3 b = Vector3.Lerp(p1, p2, t);
            Vector3 c = Vector3.Lerp(p2, p3, t);
            Vector3 d = Vector3.Lerp(a, b, t);
            Vector3 e = Vector3.Lerp(b, c, t);
            Vector3 pointOnCurve = Vector3.Lerp(d, e, t);
        
            return pointOnCurve;
        }

        public static Vector3[] GetPointsOnBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, int divCount){
            Vector3[] points = new Vector3[divCount];
            float tick = (1f / (float)divCount);
            for (int i = 0; i < divCount; i++){
                points[i] = GetPointOnBezierCurve(p0, p1, p2, tick * i);
            }

            return points;
        }
    }
}