/*
* Energy Bar Toolkit by Mad Pixel Machine
* http://www.madpixelmachine.com
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace EnergyBarToolkit {

[CustomEditor(typeof(UIRepeatProgressBar))]
public class UIRepeatProgressBarInspector : EnergyBarInspectorBase {

    // ===========================================================
    // Constants
    // ===========================================================

    // ===========================================================
    // Fields
    // ===========================================================
    
    private SerializedProperty valueCurrent;
    private SerializedProperty valueMin;
    private SerializedProperty valueMax;
    
    private SerializedProperty positionDelta;
    
    private SerializedProperty effect;
    private SerializedProperty effectCutDirection;
    
    private UIRepeatProgressBar t;

    // ===========================================================
    // Methods for/from SuperClass/Interfaces
    // ===========================================================

    // ===========================================================
    // Methods
    // ===========================================================
    
    public override void OnEnable() {
        base.OnEnable();
    
        valueCurrent = serializedObject.FindProperty("valueCurrent");
        valueMin = serializedObject.FindProperty("valueMin");
        valueMax = serializedObject.FindProperty("valueMax");
        
        positionDelta = serializedObject.FindProperty("positionDelta");
        
        effect = serializedObject.FindProperty("effect");
        effectCutDirection = serializedObject.FindProperty("effectCutDirection");
        
        t = target as UIRepeatProgressBar;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
    
        valueCurrent.intValue = (int) EditorGUILayout.Slider("Current Value", valueCurrent.intValue, valueMin.intValue, valueMax.intValue);
        MadGUI.PropertyField(valueMin, "Min Value");
        MadGUI.PropertyField(valueMax, "Max Value");
        
//        EditorGUILayout.BeginHorizontal();
//        EditorGUILayout.IntField("Icon Count", t.icons.Length);
//        if (GUILayout.Button("+")) {
//            PushIcon();
//        }
//        if (GUILayout.Button("-")) {
//            PopIcon();
//        }
//        EditorGUILayout.EndHorizontal();
        
        Vector2 prev = positionDelta.vector2Value;
        MadGUI.PropertyFieldVector2(positionDelta, "Distance Between Icons");
    
            
            // TODO: code for scaling
//            if (Foldout("Scaling", false)) {
//                EditorGUI.indentLevel++;
//                var iconScaleFrom = FirstIconScale();
//                var iconScaleTo = LastIconScale();
//                
//                var newFirstScale = EditorGUILayout.Vector2Field("Icon From", iconScaleFrom);
//                var newLastScale = EditorGUILayout.Vector2Field("Icon To", iconScaleTo);
//                EditorGUI.indentLevel--;
//            }
        
        if (GUILayout.Button("Make Pixel-Perfect")) {
            positionDelta.vector2Value = MakePixelPerfect();
        }
        
        if (!TransformChangedManually(prev)) {
            Reposition();
        } else {
            if (MadGUI.MessageWithButton("Icons positions were set by hand.", "Override", MessageType.Warning)) {
                Reposition();
            }
        }
        
        int oldEffect = effect.enumValueIndex;
        int oldEffectCutDirection = effectCutDirection.enumValueIndex;
    
        MadGUI.PropertyField(effect, "Effect");
        MadGUI.PropertyField(effectCutDirection, "Cut Direction");
        MadGUI.PropertyField(effectSmoothChange, "Smooth Effect");
        MadGUI.PropertyField(effectSmoothChangeSpeed, "Smooth Speed");
        if (oldEffect != effect.enumValueIndex || oldEffectCutDirection != effectCutDirection.enumValueIndex) {
            ApplyEffect();
        }
        
        serializedObject.ApplyModifiedProperties();
    
//        base.OnInspectorGUI();
    }
    
    Vector2 MakePixelPerfect() {
        Vector2 firstPos = Vector2.zero;
        Vector2 newDelta = Vector2.zero;
    
        t.ForEachTransform((Transform icon, Vector2 scaleOrig, int index) => {
            icon.GetComponent<UISprite>().MakePixelPerfect();
            icon.localScale = scaleOrig;
        });
    
        t.ForEachIcon((UIRepeatProgressBar.Icon icon, int index) => {
            icon.anchor.localPosition = RountToInt(icon.anchor.localPosition);
        
            if (index == 0) {
                firstPos = icon.anchor.localPosition;
            } else if (index == 1) {
                newDelta = new Vector2(icon.anchor.localPosition.x, icon.anchor.localPosition.y) - firstPos;
            }
        });
        
        return newDelta;
    }
    
#region Position
    /// <returns>
    /// True if position was changed by user on individual transforms.
    /// </returns>
    bool TransformChangedManually(Vector2 expectedSpace) {
        var delta = expectedSpace;
        
        bool result = false;
        
        t.ForEachIcon((UIRepeatProgressBar.Icon icon, int index) => {
            var pos = delta * index;
            if (!CloseEnough(icon.anchor.localPosition, pos)) {
                result = true;
            }
        });
        
        // TODO: code for scaling
//        t.ForEachIcon((UIRepeatProgressBar.Icon icon, int index) => {
//            var iconScale = IconScaleForIndex(index);
//            if (icon.icon.localScale.x != iconScale.x || icon.icon.localScale.y != iconScale.y) {
//                result = true;
//            }
//            
//            var slotScale = SlotScaleForIndex(index);
//            if (icon.slot.localScale.x != slotScale.x || icon.slot.localScale.y != slotScale.y) {
//                result = true;
//            }
//        });
        
        return result;
    }
    
    // this is somewhat a hack to not rely on MakePixelPerfect function of NGUI.
    bool CloseEnough(Vector3 v3, Vector2 v2) {
        return Mathf.Abs(v3.x - v2.x) <= 1 && Mathf.Abs(v3.y - v2.y) <= 1;
    }
    
    void Reposition() {
        var delta = positionDelta.vector2Value;
        
        t.ForEachIcon((UIRepeatProgressBar.Icon icon, int index) => {
            icon.anchor.localPosition = delta * index;
        });
    }
#endregion

#region Scale
    Vector2 IconScaleForIndex(int index) {
        int iconCount = t.icons.Length;
        float scaleFactor = index / (iconCount - 1);
        return FirstIconScale() * (1 - scaleFactor) + LastIconScale() * scaleFactor;
    }
    
    Vector2 SlotScaleForIndex(int index) {
        int iconCount = t.icons.Length;
        
        float scaleFactor = index / (iconCount - 1);
        
        return FirstSlotScale() * (1 - scaleFactor) + LastSlotScale() * scaleFactor;
    }

    Vector2 FirstIconScale() {
        if (t.icons.Length == 0) {
            return Vector2.zero;
        }
        
        var icon = t.icons[0];
        return icon.iconScaleOrig;
    }
    
    Vector2 FirstSlotScale() {
        if (t.icons.Length == 0) {
            return Vector2.zero;
        }
        
        var icon = t.icons[0];
        return icon.slotScaleOrig;
    }
    
    Vector2 LastIconScale() {
        if (t.icons.Length == 0) {
            return Vector2.zero;
        }
        
        var icon = t.icons[t.icons.Length - 1];
        return icon.iconScaleOrig;
    }
    
    Vector2 LastSlotScale() {
        if (t.icons.Length == 0) {
            return Vector2.zero;
        }
        
        var icon = t.icons[t.icons.Length - 1];
        return icon.slotScaleOrig;
    }
#endregion
    
    void ApplyEffect() {
        t.ForEachIcon((UIRepeatProgressBar.Icon icon, int index) => {
            var sprite = icon.icon.GetComponent<UISprite>();
            
            sprite.fillAmount = 1;
            sprite.alpha = 1;
            icon.ScaleToOriginal();
            
            switch ((UIRepeatProgressBar.Effect) effect.enumValueIndex) {
                case UIRepeatProgressBar.Effect.Cut:
                    switch ((UIRepeatProgressBar.CutDirection) effectCutDirection.enumValueIndex) {
                        case UIRepeatProgressBar.CutDirection.LeftToRight:
                            sprite.fillDirection = UISprite.FillDirection.Horizontal;
                            sprite.invert = false;
                            break;
                            
                        case UIRepeatProgressBar.CutDirection.RightToLeft:
                            sprite.fillDirection = UISprite.FillDirection.Horizontal;
                            sprite.invert = true;
                            break;
                            
                        case UIRepeatProgressBar.CutDirection.TopToBottom:
                            sprite.fillDirection = UISprite.FillDirection.Vertical;
                            sprite.invert = true;
                            break;
                           
                        case UIRepeatProgressBar.CutDirection.BottomToTop:
                            sprite.fillDirection = UISprite.FillDirection.Vertical;
                            sprite.invert = false;
                            break;
                    }
                    break;
            }
        });
    }
    
    // ===========================================================
    // Static Methods
    // ===========================================================

    // ===========================================================
    // Inner and Anonymous Classes
    // ===========================================================

}

} // namespace
