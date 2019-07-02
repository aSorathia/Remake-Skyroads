/*
* Energy Bar Toolkit by Mad Pixel Machine
* http://www.madpixelmachine.com
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace EnergyBarToolkit {

[CustomEditor(typeof(UISequenceProgressBar))]
public class UISequenceProgressBarInspector : EnergyBarInspectorBase {

    // ===========================================================
    // Constants
    // ===========================================================

    // ===========================================================
    // Fields
    // ===========================================================
    
    SerializedProperty valueCurrent;
    SerializedProperty valueMin;
    SerializedProperty valueMax;
    
    SerializedProperty spriteNameTemplate;
    SerializedProperty spriteNameNumberStart;
    SerializedProperty spriteNameNumberEnd;

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
        
        spriteNameTemplate = serializedObject.FindProperty("spriteNameTemplate");
        spriteNameNumberStart = serializedObject.FindProperty("spriteNameNumberStart");
        spriteNameNumberEnd = serializedObject.FindProperty("spriteNameNumberEnd");
        
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        
        valueCurrent.intValue = (int) EditorGUILayout.Slider("Current Value", valueCurrent.intValue, valueMin.intValue, valueMax.intValue);
        MadGUI.PropertyField(valueMin, "Min Value");
        MadGUI.PropertyField(valueMax, "Max Value");
        
        MadGUI.PropertyField(spriteNameTemplate, "Sprite Name Template");
        MadGUI.Info("Format: Use \"X\" to mark digit placeholder. For instance: sprite_XX will resolve to sprite_01, sprite_02 etc.");
        MadGUI.PropertyField(spriteNameNumberStart, "Sprite Number Start");
        MadGUI.PropertyField(spriteNameNumberEnd, "Sprite Number End");
        
        MadGUI.Info(string.Format("Will search for sprites {0} to {1}",
            UISequenceProgressBar.GetSpriteName(spriteNameNumberStart.intValue, spriteNameTemplate.stringValue),
            UISequenceProgressBar.GetSpriteName(spriteNameNumberEnd.intValue, spriteNameTemplate.stringValue)));
        
        if (GUILayout.Button("Make Pixel-Perfect")) {
            (target as UISequenceProgressBar).sprite.MakePixelPerfect();
        }
        
        
        serializedObject.ApplyModifiedProperties();
    }

    // ===========================================================
    // Static Methods
    // ===========================================================

    // ===========================================================
    // Inner and Anonymous Classes
    // ===========================================================

}

} // namespace
