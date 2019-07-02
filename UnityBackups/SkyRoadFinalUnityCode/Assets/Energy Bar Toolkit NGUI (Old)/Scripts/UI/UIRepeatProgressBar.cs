/*
* Energy Bar Toolkit by Mad Pixel Machine
* http://www.madpixelmachine.com
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnergyBarToolkit;

[ExecuteInEditMode]
public class UIRepeatProgressBar : EnergyBar {

    // ===========================================================
    // Constants
    // ===========================================================

    // ===========================================================
    // Fields
    // ===========================================================
    
    public Vector2 positionDelta = new Vector2(64, 0);
//    public Vector2 scaleStart = new Vector2(64, 64);
//    public Vector2 scaleEnd = new Vector2(64, 64);
    
    public Effect effect = Effect.Cut;
    public CutDirection effectCutDirection = CutDirection.LeftToRight;
    
    public Icon[] icons;
    
    [HideInInspector]
    public Vector3 baseScale;
    
    // effects
    public bool effectSmoothChange;
    public float effectSmoothChangeSpeed = 0.7f;
    
    private float actualDisplayValue; // value that is actually displayed
    
    // ===========================================================
    // Getters, Setters
    // ===========================================================
    
    // ===========================================================
    // Methods for/from SuperClass/Interfaces
    // ===========================================================

    // ===========================================================
    // Methods
    // ===========================================================
    
    new protected void Update() {
        base.Update();
        
        if (effectSmoothChange) {
            EnergyBarCommons.SmoothDisplayValue(ref actualDisplayValue, ValueF, effectSmoothChangeSpeed);
        } else {
            actualDisplayValue = ValueF;
        }
        
        float iconCount = actualDisplayValue * icons.Length;
        int filledIconCount = (int) Mathf.Floor(iconCount);     // icons painted at full visibility
     
        ForEachIcon((Icon icon, int i) => {
            if (i + 1 <= iconCount) {
                SetIconProgress(icon, 1);
            } else if (i > iconCount) {
                SetIconProgress(icon, 0);
            } else {
                SetIconProgress(icon, iconCount - filledIconCount);
            }
            
            // if not in grow effect then apply current scales
            // this allows user to scale icons on his will
            if (effect != Effect.GrowIn) {
                icon.ApplyScales();
            }
            
        });
    }
    
    private void SetIconProgress(Icon icon, float progress) {
        var sprite = icon.icon.GetComponent<UISprite>();
    
        switch (effect) {
            case Effect.Cut:
                sprite.fillAmount = progress;
                break;
            case Effect.FadeOut:
                sprite.alpha = progress;
                break;
            case Effect.GrowIn:
                if (progress > 0) {
                    sprite.alpha = 1;
                    sprite.transform.localScale = icon.iconScaleOrig * progress;
                } else {
                    sprite.alpha = 0;
                }
                break;
            case Effect.None:
                sprite.alpha = progress == 1 ? 1 : 0;
                break;
        }
    }
    
    public delegate void TransformOperation(Transform t, Vector2 scaleOrig, int index);
    
    public void ForEachTransform(TransformOperation op) {
        ForEachIcon((Icon icon, int index) => {
            op(icon.icon, icon.iconScaleOrig, index);
            if (icon.slot != null) {
                op(icon.slot, icon.slotScaleOrig, index);
            }
        });
    }
    
    public delegate void IconOperation(Icon icon, int index);
    
    public void ForEachIcon(IconOperation op) {
        for (int i = 0; i < icons.Length; ++i) {
            var icon = icons[i];
            op(icon, i);
        }
    }
    
    // ===========================================================
    // Static Methods
    // ===========================================================

    // ===========================================================
    // Inner and Anonymous Classes
    // ===========================================================
    
    public enum Effect {
        None,
        GrowIn,
        FadeOut,
        Cut
    }
    
    public enum CutDirection {
        LeftToRight,
        TopToBottom,
        RightToLeft,
        BottomToTop
    }
    
    [System.Serializable]
    public class Icon {
        public Transform anchor;
    
        public Transform icon;
        public Vector2 iconScaleOrig;
        
        public Transform slot;
        public Vector2 slotScaleOrig;
        
        public void ScaleToOriginal() {
            icon.localScale = iconScaleOrig;
            if (slot != null) {
                slot.localScale = slotScaleOrig;
            }
        }
        
        public void ApplyScales() {
            iconScaleOrig = new Vector2(icon.transform.localScale.x, icon.transform.localScale.y);
            if (slot != null) {
                slotScaleOrig = new Vector2(slot.transform.localScale.x, slot.transform.localScale.y);
            }
        }
        
    }

}