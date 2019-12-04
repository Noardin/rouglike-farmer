using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    private float _popUpTimer;
    private bool _poped;
    private float _scaleX;
    private float _scaleY;
    private float _maxScale;
    private float _minScale;
    private bool _animated;
    private int animationDirection = 1;
    private SpriteRenderer _SR;
    private bool isTimed;
    public Sprite[] popUpSprites;
    private Transform parentTransform;
    public enum PopUpTypes
    {
        Exclemation, 
        ENTER,
        ESC,
        SPACE,
    }

    public PopUpTypes popUpType;

    void AdjustSize()
    {
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        parentTransform = transform.root;
        _scaleX = x/parentTransform.localScale.x;
        _scaleY = y/parentTransform.localScale.y;
        transform.localScale = new Vector3(_scaleX,_scaleY );
    }
    protected virtual void Awake()
    {
        AdjustSize();
        _SR = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (parentTransform.localScale != transform.root.localScale)
        {
            AdjustSize();
        }
    }


    public void PopUp(PopUpTypes popUpType,float time)
    {
        _animated = false;
        
        _SR.sprite = popUpSprites[popUpType.GetHashCode()];
        _SR.enabled = true;
        _popUpTimer = time;
        _poped = true;
        isTimed = true;
    }

    public void ShowPopUp(PopUpTypes popUpType)
    {
        _animated = false;
        _SR.sprite = popUpSprites[popUpType.GetHashCode()];
        _SR.enabled = true;
        isTimed = false;
        _poped = true;
    }
    public void HidePopUp()
    {
        _poped = false;
        _SR.enabled = false;
        transform.localScale = new Vector3(_scaleX, _scaleY);
    }

    public void PopUp(PopUpTypes popUpType,float time, float maxScale,float minScale)
    {
        _animated = true;
        _maxScale = maxScale;
        _minScale = minScale;
        _SR.sprite = popUpSprites[popUpType.GetHashCode()];
        _SR.enabled = true;
        _popUpTimer = time;
        isTimed = true;
        _poped = true;
    }

    private void FixedUpdate()
    {
        if (_poped)
        {
            _popUpTimer -= Time.deltaTime;
            if (_popUpTimer > 0)
            {
                if (_animated)
                {
                    Animate();
                }
            }
            else if(isTimed)
            {
                _SR.enabled = false;
                _poped = false;
                transform.localScale = new Vector3(_scaleX, _scaleY);
            }
        }
        
    }

    private void Animate()
    {
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        if (x >= _maxScale*_scaleX)
        {
            animationDirection = -1;
        }

        if (x < _scaleX/_minScale)
        {
            animationDirection = 1;
        }
        transform.localScale = new Vector3(x+Time.deltaTime*animationDirection*10f, y+Time.deltaTime*animationDirection*10f);
    }
}

