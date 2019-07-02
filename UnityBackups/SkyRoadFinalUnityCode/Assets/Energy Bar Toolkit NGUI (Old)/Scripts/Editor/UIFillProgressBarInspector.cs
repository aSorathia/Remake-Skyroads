/*
* Energy Bar Toolkit by Mad Pixel Machine
* http://www.madpixelmachine.com
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace EnergyBarToolkit {

[CustomEditor(typeof(UIFillProgressBar))]
public class UIFillProgressBarInspector : EnergyBarInspectorBase {

    // ===========================================================
    // Constants
    // ===========================================================

    // ===========================================================
    // Fields
    // ===========================================================
    
    SerializedProperty valueCurrent;
    SerializedProperty valueMin;
    SerializedProperty valueMax;
    
    SerializedProperty fillDirection;
    SerializedProperty fillDirectionInvert;
    
    SerializedProperty colorType;
    SerializedProperty colorGradient;
    
    SerializedProperty effectBurn;
    SerializedProperty effectBurnBar;
    
    SerializedProperty effectBlink;
    SerializedProperty effectBlinkValue;
    SerializedProperty effectBlinkRatePerSecond;
    SerializedProperty effectBlinkColor;
    
    private UIFillProgressBar t;

    // ===========================================================
    // Methods for/from SuperClass/Interfaces
    // ===========================================================

    // ===========================================================
    // Methods
    // ===========================================================

    public override void OnEnable() {
        base.OnEnable();
    
        t = target as UIFillProgressBar;
    
        valueCurrent = serializedObject.FindProperty("valueCurrent");
        valueMin = serializedObject.FindProperty("valueMin");
        valueMax = serializedObject.FindProperty("valueMax");
        
        fillDirection = serializedObject.FindProperty("fillDirection");
        fillDirectionInvert = serializedObject.FindProperty("fillDirectionInvert");
        
        colorType = serializedObject.FindProperty("colorType");
        colorGradient = serializedObject.FindProperty("colorGradient");
        
        effectBurn = serializedObject.FindProperty("effectBurn");
        effectBurnBar = serializedObject.FindProperty("effectBurnBar");
        
        effectBlink = serializedObject.FindProperty("effectBlink");
        effectBlinkValue = serializedObject.FindProperty("effectBlinkValue");
        effectBlinkRatePerSecond = serializedObject.FindProperty("effectBlinkRatePerSecond");
        effectBlinkColor = serializedObject.FindProperty("effectBlinkColor");
    }
    
    override public void OnInspectorGUI() {
        serializedObject.Update();
        
        valueCurrent.intValue = (int) EditorGUILayout.Slider("Current Value", valueCurrent.intValue, valueMin.intValue, valueMax.intValue);
        MadGUI.PropertyField(valueMin, "Min Value");
        MadGUI.PropertyField(valueMax, "Max Value");
        
        int fillDirectionPrev = fillDirection.enumValueIndex;
        MadGUI.PropertyField(fillDirection, "Fill Direction");
        if (fillDirectionPrev != fillDirection.enumValueIndex) {
            SetFillDirection((UISprite.FillDirection) fillDirection.enumValueIndex, fillDirectionInvert.boolValue);
        }
        
        bool invertFillPrev = fillDirectionInvert.boolValue;
        MadGUI.PropertyField(fillDirectionInvert, "Invert Fill");
        if (invertFillPrev != fillDirectionInvert.boolValue) {
            SetFillDirection((UISprite.FillDirection) fillDirection.enumValueIndex, fillDirectionInvert.boolValue);
        }
        
        MadGUI.PropertyField(colorType, "Bar Color Type");
        
        switch ((UIFillProgressBar.ColorType) colorType.enumValueIndex) {
            case UIFillProgressBar.ColorType.Solid:
                t.bar.color = EditorGUILayout.ColorField("Bar Color", t.bar.color);
                EditorUtility.SetDirty(t.bar);
                break;
            case UIFillProgressBar.ColorType.Gradient:
                MadGUI.PropertyField(colorGradient, "Bar Color Gradient");
                break;
            default:
                Debug.LogError("unknown option: " + colorType.enumValueIndex);
                break;
        }
        
        if (MadGUI.Foldout("Position", true)) {
            MadGUI.BeginBox(); {
                MadGUI.PropertyField(anchorObject, "Anchor", "Object to attach to");
                MadGUI.PropertyField(anchorCamera, "Anchor Camera", "Camera on which anchored object is visible");
                MadGUI.PropertyField(anchor3d, "Anchor 3D",
                    "If enabled bar will be positioned in real 3D position. Useful when Using 3D interface in NGUI.");
                MadGUI.PropertyFieldVector2(anchorOffset, "Anchor Offset");
            } MadGUI.EndBox();
        }
        
        if (MadGUI.Foldout("Effects", true)) {
            MadGUI.BeginBox(); {
                FieldSmoothEffect();
                
                bool burnEnabledPrev = effectBurn.boolValue;
                MadGUI.PropertyFieldToggleGroup2(effectBurn, "Burn Out Effect", () => {
                    MadGUI.Indent(() => {
                        if (burnEnabledPrev != effectBurn.boolValue) {
                            SetBurnEffect(effectBurn.boolValue);
                        }
                        
                        if (effectBurn.boolValue) {
                            ((UISprite)effectBurnBar.objectReferenceValue).color =
                                EditorGUILayout.ColorField("Burn Color", ((UISprite)effectBurnBar.objectReferenceValue).color);
                        }    
                    });
                });
                
                MadGUI.PropertyFieldToggleGroup2(effectBlink, "Blink Effect", () => {
                    MadGUI.Indent(() => {
                        MadGUI.PropertyField(effectBlinkValue, "Value");
                        MadGUI.PropertyField(effectBlinkRatePerSecond, "Rate (per second)");
                        MadGUI.PropertyField(effectBlinkColor, "Color");
                    });
                });
                
                
            } MadGUI.EndBox();
        }
        
        serializedObject.ApplyModifiedProperties();
    }
    
    void SetBurnEffect(bool enabled) {
        if (enabled) {
            var burnBar = NGUITools.AddWidget<UISprite>(t.gameObject);
            burnBar.name = "burnBar";
            burnBar.depth = t.bar.depth - 5;
            burnBar.type = UISprite.Type.Filled;
            burnBar.atlas = t.bar.atlas;
            burnBar.spriteName = t.bar.spriteName;
            burnBar.pivot = UIWidget.Pivot.TopLeft;
            burnBar.fillDirection = t.bar.fillDirection;
            
            var sprite = burnBar.atlas.GetSprite(burnBar.spriteName);
                burnBar.transform.localPosition =
                    new Vector3(sprite.paddingLeft * sprite.width, -sprite.paddingTop * sprite.height, 0);
            burnBar.MakePixelPerfect();
            
            effectBurnBar.objectReferenceValue = burnBar;
        } else {
            GameObject.DestroyImmediate((effectBurnBar.objectReferenceValue as MonoBehaviour).gameObject);
            effectBurnBar.objectReferenceValue = null;
        }
    }
    
    void SetFillDirection(UISprite.FillDirection fillDirection, bool invertFill) {
        t.bar.fillDirection = fillDirection;
        t.bar.invert = invertFill;
        if (t.effectBurnBar != null) {
            t.effectBurnBar.fillDirection = fillDirection;
            t.effectBurnBar.invert = invertFill;
        }
    }

    // ===========================================================
    // Static Methods
    // ===========================================================

    // ===========================================================
    // Inner and Anonymous Classes
    // ===========================================================

}

} // namespace
