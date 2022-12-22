using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public Renderer MainRender;
    public Vector2Int Size = Vector2Int.one;


    public void SetTransperent(bool avaliable)
    {
        if (avaliable)
        {
            MainRender.material.color = Color.green;
        }
        else
        {
            MainRender.material.color = Color.red;
        }
    }

    public void SetNormal()
    {
        MainRender.material.color = Color.white;
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
}
