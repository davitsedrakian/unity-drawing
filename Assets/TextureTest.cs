using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureTest : MonoBehaviour
{
    [SerializeField]  private Texture2D _texture;
    [Range(2,512)]
    [SerializeField]  private int _resolution;

    [SerializeField]  private FilterMode _filtermode;
  
    
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
            
        }



        _texture.filterMode = _filtermode;
        
        // _texture.SetPixel(5,5,Color.red);
        // _texture.SetPixel(6,6,Color.yellow);

        for (int y = 0; y < _resolution; y++)
        {
            for (int x = 0; x < _resolution; x++)
            {
                _texture.SetPixel(x,y,new Color(1,0.5f,0,1));
            }
        }

        _texture.Apply();
    }
}
