using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/UIGradient")]
public class UIGradient : BaseMeshEffect
{
    [SerializeField] private Color32 _topColor = Color.white;
    [SerializeField] private Color32 _bottomColor = Color.black;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive())
            return;

        var vertexList = new System.Collections.Generic.List<UIVertex>();
        vh.GetUIVertexStream(vertexList);

        int count = vertexList.Count;
        if (count == 0)
            return;

        float topY = vertexList[0].position.y;
        float bottomY = vertexList[0].position.y;

        // Находим верхнюю и нижнюю границы
        for (int i = 1; i < count; i++)
        {
            float y = vertexList[i].position.y;
            if (y > topY) topY = y;
            else if (y < bottomY) bottomY = y;
        }

        float height = topY - bottomY;
        if (height <= 0) return;

        // Применяем градиент
        for (int i = 0; i < count; i++)
        {
            UIVertex vertex = vertexList[i];
            float ratio = (vertex.position.y - bottomY) / height;
            vertex.color = Color32.Lerp(_bottomColor, _topColor, ratio);
            vertexList[i] = vertex;
        }

        vh.Clear();
        vh.AddUIVertexTriangleStream(vertexList);
    }
}