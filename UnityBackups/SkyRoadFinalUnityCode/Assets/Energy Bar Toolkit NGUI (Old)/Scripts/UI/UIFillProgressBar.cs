/*
* Energy Bar Toolkit by Mad Pixel Machine
* http://www.madpixelmachine.com
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnergyBarToolkit;

[ExecuteInEditMode]
public class UIFillProgressBar : UIProgressBarBase {

    // ===========================================================
    // Constants
    // ===========================================================

    // ===========================================================
    // Fields
    // ===========================================================
    
    public UISprite.FillDirection fillDirection = UISprite.FillDirection.Horizontal;
    public bool fillDirectionInvert = false;
    
    public ColorType colorType;
    public Gradient colorGradient;
    private Color colorVisible;
    
    public bool effectSmoothChange;
    public float effectSmoothChangeSpeed = 0.7f;
    
    public bool effectBurn;
    [HideInInspector]
    public UISprite effectBurnBar;
    
    public bool effectBlink;
    public float effectBlinkValue = 0.2f;
    public float effectBlinkRatePerSecond = 1f;
    public Color effectBlinkColor = new Color(1, 1, 1, 0);
    
    private float effectBlinkAccum;
    private Color effectBlinkBaseColorBurn;
    
    
    [HideInInspector]
    public UISprite bar;
    
    private float actualDisplayValue; // value that is actually displayed
    private float actualBurnValue;

    // ===========================================================
    // Methods for/from SuperClass/Interfaces
    // ===========================================================

    // ===========================================================
    // Methods
    // ===========================================================

    new protected void Start() {
        base.Start();
    
        colorVisible = bar.color;
        if (effectBurnBar != null) {
            effectBlinkBaseColorBurn = effectBurnBar.color;
        }
    }

    new protected void Update() {
        base.Update();
        
        switch (colorType) {
            case ColorType.Solid:
                // do nothing, color changed by inspector code
                break;
            case ColorType.Gradient:
                colorVisible = colorGradient.Evaluate(ValueF);
                break;
            default:
                Debug.LogError("unknown option: " + colorType);
                break;
        }
        
        if (effectBurn) {
            if (effectSmoothChange) {
                // in burn mode smooth primary bar only when it's increasing
                if (ValueF > actualDisplayValue) {
                    EnergyBarCommons.SmoothDisplayValue(ref actualDisplayValue, ValueF, effectSmoothChangeSpeed);
                } else {
                    actualDisplayValue = ValueF;
                }
            } else {
                actualDisplayValue = ValueF;
            }
            
            EnergyBarCommons.SmoothDisplayValue(ref actualBurnValue, actualDisplayValue, effectSmoothChangeSpeed);
            actualBurnValue = Mathf.Max(actualBurnValue, actualDisplayValue);
        } else {
            if (effectSmoothChange) {
                EnergyBarCommons.SmoothDisplayValue(ref actualDisplayValue, ValueF, effectSmoothChangeSpeed);
            } else {
                actualDisplayValue = ValueF;
            }
            
            actualBurnValue = actualDisplayValue;
        }
        
        bar.fillAmount = actualDisplayValue;
        if (effectBurnBar != null) {
            effectBurnBar.fillAmount = actualBurnValue;
        }
        
        if (Application.isPlaying) {
            if (effectBlink && EnergyBarCommons.Blink(ValueF, effectBlinkValue, effectBlinkRatePerSecond, ref effectBlinkAccum)) {
                bar.color = effectBlinkColor;
                if (effectBurnBar != null) {
                    effectBurnBar.alpha = 0;
                }
            } else {
                bar.color = colorVisible;
                if (effectBurnBar != null) {
                    effectBurnBar.alpha = 1;
                    effectBurnBar.color = effectBlinkBaseColorBurn;
                }
            }
        }
    }

    // ===========================================================
    // Static Methods
    // ===========================================================

    // ===========================================================
    // Inner and Anonymous Classes
    // ===========================================================
    
    public enum ColorType {
        Solid,
        Gradient,
    }

}