/*
* Energy Bar Toolkit by Mad Pixel Machine
* http://www.madpixelmachine.com
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// abstract class for all energy
public abstract class UIProgressBarBase : EnergyBar {

    // ===========================================================
    // Constants
    // ===========================================================

    // ===========================================================
    // Fields
    // ===========================================================
    
    public GameObject anchorObject;
    public Camera anchorCamera; // camera on which anchor should be visible. if null then Camera.main
    public Vector2 anchorOffset = new Vector2(0.5f, 0.5f);
    public bool anchor3d; // if true places anchor in real 3d position
    
    Camera guiCamera;
    
    Bounds bounds; // total bounds for all sprites. Calculated on Start()

    // ===========================================================
    // Methods for/from SuperClass/Interfaces
    // ===========================================================

    // ===========================================================
    // Methods
    // ===========================================================
    

    protected void Start() {
        var uiRoot = NGUITools.GetRoot(gameObject);
        guiCamera = uiRoot.GetComponentInChildren<Camera>();
        
        RecalculateBounds();
    }
    
    void RecalculateBounds() {
        bounds = new Bounds();
        bool boundsSet = false;
        UISprite[] sprites = GetComponentsInChildren<UISprite>();
        foreach (UISprite sprite in sprites) {
            var size = new Vector2(sprite.width, sprite.height);
            size.Scale(sprite.cachedTransform.localScale);
            
            if (!boundsSet) {
                bounds = new Bounds(size / 2, size);
                boundsSet = true;
            } else {
                bounds.Encapsulate(size);
            }
        }
    }

    void LateUpdate() {
        if (Application.isEditor) {
            RecalculateBounds();
        }
    
        if (anchorObject != null && anchorCamera != null) {
        
            if (anchor3d) {
                transform.position = anchorObject.transform.position;
            } else {
                var pos = anchorCamera.WorldToScreenPoint(anchorObject.transform.position);
                pos = guiCamera.ScreenToWorldPoint(pos);
                pos.z = 0;
                transform.position = pos;
            }

            Vector3 loc = transform.localPosition;
            loc.x -= bounds.size.x * transform.localScale.x * anchorOffset.x;
            loc.y += bounds.size.y * transform.localScale.y * anchorOffset.y;
            transform.localPosition = loc;
        }
        
        UpdateVisible();
    }
    
    void UpdateVisible() {
        bool visible = IsVisible();
    
        if ((visible && !cachedVisible) || (!visible && cachedVisible)) {
            // change all child sprites states
            var sprites = transform.GetComponentsInChildren<UISprite>();
            foreach (var sprite in sprites) {
                SetEnabled(sprite, visible);
            }
            
            cachedVisible = visible;
        }
    }
    
    bool cachedVisible = true;
    
    protected bool IsVisible() {
        if (anchorObject != null && anchorCamera != null) {
            Vector3 heading = anchorObject.transform.position - anchorCamera.transform.position;
            float dot = Vector3.Dot(heading, anchorCamera.transform.forward);
            
            return dot >= 0;
        }
        
        return true;
    }
    
    protected void SetEnabled(UISprite sprite, bool enabled) {
        if (sprite != null) {
            sprite.enabled = enabled;
        }
    }

    // ===========================================================
    // Static Methods
    // ===========================================================

    // ===========================================================
    // Inner and Anonymous Classes
    // ===========================================================

}
