using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    private float _popUpTimer;
    public bool _poped;
    private float _scaleX;
    private float _scaleY;
    private float _maxScale;
    private float _minScale;
    private bool _animated;
    private int animationDirection = 1;
    public SpriteRenderer _SR;
    private bool isTimed = false;
    public Sprite[] popUpSprites;
    private Transform parentTransform;
    private List<Transform> listOfParents = new List<Transform>();
    private int parentIndex;
    private Transform currentGameObject;
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
        getParents();
        parentIndex = listOfParents.Count-1;

        parentTransform = listOfParents[parentIndex];

        int i = 0;
        if (parentTransform.localScale.x == 1f && parentTransform.localScale.y == 1f)
        {
            while (parentTransform.localScale.x == 1f && parentTransform.localScale.y == 1f)
            {
                i++;
                
                parentIndex--;
                if (parentIndex <0)
                {
                    _scaleX = x/parentTransform.localScale.x;
       
                    _scaleY = y/parentTransform.localScale.y;
                    return;
                }

                if (i > 20)
                {
                    return;
                }
                
                parentTransform = listOfParents[parentIndex].transform;
            }
            _scaleX = x/parentTransform.localScale.x;

            _scaleY = y/parentTransform.localScale.y;
        
            transform.localScale = new Vector3(_scaleX,_scaleY );
        }
        else
        {
            _scaleX = x/parentTransform.localScale.x;
            _scaleY = y/parentTransform.localScale.y;
            transform.localScale = new Vector3(_scaleX,_scaleY );
        }
        
         
    }

    private void getParents()
    {
        int i = 0;
       
        while (currentGameObject.parent != null)
        {
            i++;
            currentGameObject = currentGameObject.parent;
            listOfParents.Add(currentGameObject);
            if (i > 30)
            {
                return;
            }
            
            
        }
    }
   
    protected virtual void Awake()
    {
        currentGameObject = gameObject.transform;
        AdjustSize();
        
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

    public void PopUpTimed(PopUpTypes popUpType,float time, float maxScale,float minScale)
    {
        
        _animated = true;
        _popUpTimer = time;
       
        isTimed = true;
        _poped = true;
       
        _maxScale = maxScale;
        _minScale = minScale;
        _SR.sprite = popUpSprites[popUpType.GetHashCode()];
        _SR.enabled = true;
       
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
           
        if (x < _scaleX*_minScale)
        {
            animationDirection = 1;
        }
        transform.localScale = new Vector3(x+Time.deltaTime*animationDirection*10f, y+Time.deltaTime*animationDirection*10f);
    }
}

