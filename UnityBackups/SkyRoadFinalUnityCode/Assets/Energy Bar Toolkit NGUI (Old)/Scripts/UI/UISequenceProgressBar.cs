/*
* Energy Bar Toolkit by Mad Pixel Machine
* http://www.madpixelmachine.com
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[ExecuteInEditMode]
public class UISequenceProgressBar : EnergyBar {

    // ===========================================================
    // Constants
    // ===========================================================

    // ===========================================================
    // Fields
    // ===========================================================
    
    public UISprite sprite;
    public string spriteName = "heart";
    
    public string spriteNameTemplate = "sprite_XX";
    public int spriteNameNumberStart = 1;
    public int spriteNameNumberEnd = 10;

    // ===========================================================
    // Methods for/from SuperClass/Interfaces
    // ===========================================================

    // ===========================================================
    // Methods
    // ===========================================================

    void Start() {
    }

    new void Update() {
        base.Update();
        var spriteName = GetSpriteName(GetSpriteNumber(), spriteNameTemplate);
        sprite.spriteName = spriteName;
    }
    
    int GetSpriteNumber() {
        int size = spriteNameNumberEnd - spriteNameNumberStart + 1;
        int index = (int) Mathf.Min(Mathf.Floor(ValueF * size), size - 1);
        return spriteNameNumberStart + index;
    }
    
    public static string GetSpriteName(int number, string template) {
        StringBuilder spriteName = new StringBuilder(template);
    
        string numberStr = number.ToString();
        for (int i = numberStr.Length - 1; i >= 0; i--) {
            char c = numberStr[i];
            int lastIndex = spriteName.ToString().LastIndexOf('X');
            if (lastIndex == -1) {
                break;
            }
            
            spriteName[lastIndex] = c;
        }
        
        return spriteName.Replace('X', '0').ToString();
    }

    // ===========================================================
    // Static Methods
    // ===========================================================

    // ===========================================================
    // Inner and Anonymous Classes
    // ===========================================================

}