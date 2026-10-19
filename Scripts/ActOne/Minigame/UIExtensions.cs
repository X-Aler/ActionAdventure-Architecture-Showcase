using UnityEngine;

namespace ActOne.Minigame
{
    public static class UIExtensions
    {
        public static bool IsOverlapping(RectTransform a, RectTransform b, Camera cam = null)
        {
            if (a == null || b == null) return false;

            Vector2[] A = GetScreenSpaceCorners(a, cam);
            Vector2[] B = GetScreenSpaceCorners(b, cam);

            return PolygonsIntersect(A, B);
        }

        private static Vector2[] GetScreenSpaceCorners(RectTransform rt, Camera cam)
        {
            Vector3[] worldCorners = new Vector3[4];
            rt.GetWorldCorners(worldCorners);

            Vector2[] screenCorners = new Vector2[4];
            for (int i = 0; i < 4; i++)
            {
                // Для Screen Space - Overlay, cam может быть null — RectTransformUtility корректно обработает это.
                screenCorners[i] = RectTransformUtility.WorldToScreenPoint(cam, worldCorners[i]);
            }

            return screenCorners;
        }

        // Separating Axis Theorem for two convex polygons (rectangles in our case)
        private static bool PolygonsIntersect(Vector2[] poly1, Vector2[] poly2)
        {
            // проверяем оси, образованные ребрами обеих фигур
            if (!IsOverlapOnAllAxes(poly1, poly2)) return false;
            if (!IsOverlapOnAllAxes(poly2, poly1)) return false;
            return true;
        }

        private static bool IsOverlapOnAllAxes(Vector2[] polyA, Vector2[] polyB)
        {
            for (int i = 0; i < polyA.Length; i++)
            {
                Vector2 p1 = polyA[i];
                Vector2 p2 = polyA[(i + 1) % polyA.Length];
                Vector2 edge = p2 - p1;

                // нормаль к ребру — это потенциальная ось разделения
                Vector2 axis = new Vector2(-edge.y, edge.x);
                if (axis.sqrMagnitude < Mathf.Epsilon) continue; // защита от вырожденного ребра

                axis.Normalize();

                if (!OverlapOnAxis(polyA, polyB, axis))
                    return false; // найдена разделяющая ось -> нет пересечения
            }

            return true;
        }

        private static bool OverlapOnAxis(Vector2[] polyA, Vector2[] polyB, Vector2 axis)
        {
            ProjectPolygon(axis, polyA, out float minA, out float maxA);
            ProjectPolygon(axis, polyB, out float minB, out float maxB);

            // нет пересечения между проекциями
            if (maxA < minB || maxB < minA) return false;
            return true;
        }

        private static void ProjectPolygon(Vector2 axis, Vector2[] poly, out float min, out float max)
        {
            min = max = Vector2.Dot(axis, poly[0]);
            for (int i = 1; i < poly.Length; i++)
            {
                float proj = Vector2.Dot(axis, poly[i]);
                if (proj < min) min = proj;
                if (proj > max) max = proj;
            }
        }
    }
}
