using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureTest : MonoBehaviour
{
    [SerializeField] private Texture2D _texture;
    [Range(2,512)]
    [SerializeField] private int _resolution;

    [SerializeField] private FilterMode _filtermode;
    [SerializeField] private TextureWrapMode _textureWrapMode;
    [SerializeField] private float _radiusOut;  
    [SerializeField] private float _radiusIn;
    [SerializeField] private Vector2 offset;
    
    
    
    private void OnValidate()
    {
        if (_texture == null)
        {
            _texture = new Texture2D(_resolution,_resolution);
            GetComponent<Renderer>().material.mainTexture = _texture;
        }

        if (_texture.width != _resolution)
        {
            _texture.Reinitialize(_resolution,_resolution);
            _texture.Reinitialize(_resolution, _resolution);
        }

        

        _texture.filterMode = _filtermode;
        _texture.wrapMode = _textureWrapMode;
        
        // _texture.SetPixel(5,5,Color.red);
        // _texture.SetPixel(6,6,Color.yellow);


        float step = 1f / _resolution;
        
        for (int y = 0; y < _resolution; y++)
        {
            for (int x = 0; x < _resolution; x++)
            {
                float x2 = Mathf.Pow((x+0.5f) * step - offset.x, 2);
                float y2 = Mathf.Pow((y+0.5f) * step - offset.y, 2);
                float rOut2 = Mathf.Pow(_radiusOut, 2);   
                float rIn2 = Mathf.Pow(_radiusIn, 2);
                float result = x2 + y2;
                
                if(result < rOut2 && result > rIn2)
                {
                    _texture.SetPixel(x,y,Color.black);
                }
                else
                {
                    _texture.SetPixel(x,y,Color.white);
                }
            }
        }

        _texture.Apply();
    }
}
