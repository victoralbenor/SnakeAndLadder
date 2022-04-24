using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int Number;
    public RectTransform RectTransform;
    public Tile snakeTo;
    public Tile ladderTo;

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        if (ladderTo != null)
        {
            LineRenderer line = gameObject.AddComponent<LineRenderer>();
            line.startWidth = 0.2f;
            line.endWidth = 0.2f;

            line.material = new Material(Shader.Find("Sprites/Default"))
            {
                color = Color.green
            };

            line.positionCount = 2;
            line.SetPosition(0, new Vector3(RectTransform.position.x, RectTransform.position.y, 0));
            line.SetPosition(1, new Vector3(ladderTo.RectTransform.position.x, ladderTo.RectTransform.position.y, 0));
        }

        if (snakeTo != null)
        {
            LineRenderer line = gameObject.AddComponent<LineRenderer>();
            line.startWidth = 0.2f;
            line.endWidth = 0.2f;

            line.material = new Material(Shader.Find("Sprites/Default"))
            {
                color = Color.red
            };


            line.positionCount = 2;
            line.SetPosition(0, new Vector3(RectTransform.position.x, RectTransform.position.y, 0));
            line.SetPosition(1, new Vector3(snakeTo.RectTransform.position.x, snakeTo.RectTransform.position.y, 0));
        }
    }

}
