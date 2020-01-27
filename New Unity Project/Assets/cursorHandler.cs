using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class cursorHandler : MonoBehaviour
{
    public SpriteRenderer SR;

    public Sprite[] CursorSprites;
    
    public enum CursorTypes
    {
        drag,
        point,
    }
    public CursorTypes currentCursor
    {
        get { return CurrentCursor; }
        
        set
        {
            ChangeCursorType(value);
            CurrentCursor = value;
        }
    }

    private CursorTypes CurrentCursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        currentCursor = CursorTypes.point;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 20;
        transform.position = cursorPos;
    }

    private void ChangeCursorType(CursorTypes type)
    {
        SR.sprite = CursorSprites[type.GetHashCode()];

    }
}
