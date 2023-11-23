using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    [Range (2, 512)]
    [SerializeField] private int _textureSize = 128;
    [SerializeField] private TextureWrapMode _textureWrapMode; 
    [SerializeField] private FilterMode _filterMode;
    [SerializeField] private Texture2D _texture;
    [SerializeField] private Material _material;

    [SerializeField] private Camera _camera;
    [SerializeField] private Collider _collider;
    [SerializeField] private Color _color;
    [SerializeField] private int _brushSize = 8;

    private int _oldRayX, _oldRayY;

    private void OnValidate()
    {
        if (_texture == null)
        {
            _texture = new Texture2D(_textureSize, _textureSize);
        }

        if (_texture.width != _textureSize)
        {
            _texture.Reinitialize(_textureSize, _textureSize);
        }

        _texture.wrapMode = _textureWrapMode;
        _texture.filterMode = _filterMode;
        _material.mainTexture = _texture;
        _texture.Apply();
    }

    private void Update()
    {

        _brushSize += (int)Input.mouseScrollDelta.y;
        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (_collider.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.textureCoord.x);
                Debug.Log(hit.textureCoord.x * _textureSize);
                int rayX = (int)(hit.textureCoord.x * _textureSize);
                int rayY = (int)(hit.textureCoord.y * _textureSize);

              
                if (_oldRayX != rayX || _oldRayY != rayY)
                {
                    // DrawQuad(rayX,rayY);
                    // DrawCircle(rayX,rayY);
                    DrawCircleBlendColors(rayX, rayY);
                    
                    
                    _oldRayX = rayX;
                    _oldRayY = rayY;
                
                    _texture.Apply();
                }
                
             
              
            }


        }
    }

    private void DrawQuad(int rayX,int rayY)
    {
        for (int y = 0; y < _brushSize; y++)
        {
            for (int x = 0; x < _brushSize; x++)
            {
                _texture.SetPixel(rayX+x-_brushSize/2,rayY+y-_brushSize/2,_color);
            }
        }


    }
    
    private void DrawCircle(int rayX, int rayY) 
    {
        for (int y = 0; y < _brushSize; y++) {
            for (int x = 0; x < _brushSize; x++) {

                float x2 = Mathf.Pow(x - _brushSize / 2, 2);
                float y2 = Mathf.Pow(y - _brushSize / 2, 2);
                float r2 = Mathf.Pow(_brushSize / 2 - 0.5f, 2);

                if (x2 + y2 < r2) {
                    int pixelX = rayX + x - _brushSize / 2;
                    int pixelY = rayY + y - _brushSize / 2;

                    if (pixelX >= 0 && pixelX < _textureSize && pixelY >= 0 && pixelY < _textureSize) {
                        Color oldColor = _texture.GetPixel(pixelX, pixelY);
                        Color resultColor = Color.Lerp(oldColor, _color, _color.a);
                        _texture.SetPixel(pixelX, pixelY, resultColor);
                    }

                }

                
            }
        }
    }

    private void DrawCircleBlendColors(int rayX, int rayY)
    {
        for (int y = 0; y < _brushSize; y++) {
            for (int x = 0; x < _brushSize; x++) {

                float x2 = Mathf.Pow(x - _brushSize / 2, 2);
                float y2 = Mathf.Pow(y - _brushSize / 2, 2);
                float r2 = Mathf.Pow(_brushSize / 2 - 0.5f, 2);

                if (x2 + y2 < r2) {
                    int pixelX = rayX + x - _brushSize / 2;
                    int pixelY = rayY + y - _brushSize / 2;

                    if (pixelX >= 0 && pixelX < _textureSize && pixelY >= 0 && pixelY < _textureSize) {
                        Color oldColor = _texture.GetPixel(pixelX, pixelY);
                        
                        //Lerp Blend
                        Color resultColor = Color.Lerp(oldColor, _color, _color.a);
                        
                        // Additive Blend
                        // Color resultColor = oldColor + _color; 

                        // Multiplicative Blend
                        // Color resultColor = new Color(oldColor.r * _color.r, oldColor.g * _color.g, oldColor.b * _color.b, oldColor.a * _color.a);

                        
                        _texture.SetPixel(pixelX, pixelY, resultColor);
                    }

                }

                
            }
        }
    }
    
    
}
