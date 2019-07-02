/*
* Energy Bar Toolkit by Mad Pixel Machine
* http://www.madpixelmachine.com
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class UICreateEnergyBarWizard : EditorWindow {

    // ===========================================================
    // Constants
    // ===========================================================

    // ===========================================================
    // Fields
    // ===========================================================
    
    static bool loaded;
    
    private BarType template = BarType.Repeated;

    // ===========================================================
    // Methods for/from SuperClass/Interfaces
    // ===========================================================

    // ===========================================================
    // Methods
    // ===========================================================
    
    // Add menu named "My Window" to the Window menu
    [MenuItem ("NGUI/Energy Bar Tool")]
    static void Init () {
        // Get existing open window or if none, make a new one:
        EditorWindow.GetWindow(typeof(UICreateEnergyBarWizard));
    }
 
    void OnEnable() {
        title = "Bars Tool";
    }
          
    void Start() {
    }

    void Update() {
    }
    
    void OnSelectAtlas (Object obj) {
         NGUISettings.atlas = obj as UIAtlas;
         Repaint();
     }
    
    void OnGUI() {
        if (!loaded) { loaded = true; Load(); }
    
        EditorGUIUtility.LookLikeControls(80f);
        GameObject go = NGUIEditorTools.SelectedRoot();
    
        GUILayout.BeginHorizontal();
        ComponentSelector.Draw<UIAtlas>(NGUISettings.atlas, OnSelectAtlas, true, GUILayout.Width(140f));
        GUILayout.Label("Texture atlas used by widgets", GUILayout.MinWidth(10000f));
        GUILayout.EndHorizontal();
        
        GUILayout.Space(-2f);
        NGUIEditorTools.DrawSeparator();
        
        GUILayout.BeginHorizontal();
        template = (BarType)EditorGUILayout.EnumPopup("Template", template, GUILayout.Width(200f));
        GUILayout.Space(20f);
        GUILayout.Label("Select a widget template to use");
        GUILayout.EndHorizontal();
         
        switch (template) {
            case BarType.Filled:
                CreateFilled(go);
                break;
        
            case BarType.Repeated:
                CreateRepeated(go);
                break;
                
            case BarType.Sequence:
                CreateSequence(go);
                break;
        }
    }
    
    static string sprite0;
    static string sprite1;
    static string sprite2;
    static int int0;
    static bool bool0;
    static bool bool1;
    
    void OnSprite0(string val) { sprite0 = val; Save(); Repaint(); }
    void OnSprite1(string val) { sprite1 = val; Save(); Repaint(); }
    void OnSprite2(string val) { sprite2 = val; Save(); Repaint(); }
    void OnBool0(bool val) { bool0 = val; Save(); Repaint(); }
    void OnBool1(bool val) { bool1 = val; Save(); Repaint(); }
    
    private void CreateFilled(GameObject go) {
        if (NGUISettings.atlas != null) {
        
            bool withBackground = BoolField("With Background", "", bool0, OnBool0);
            GUI.enabled = withBackground;
            NGUIEditorTools.DrawSpriteField("Background", "Background image (optional)", NGUISettings.atlas, sprite0, OnSprite0);
            GUI.enabled = true;
            
            NGUIEditorTools.DrawSpriteField("Bar", "Bar image", NGUISettings.atlas, sprite1, OnSprite1);
            
            bool withForeground = BoolField("With Foreground", "", bool1, OnBool1);
            GUI.enabled = withForeground;
            NGUIEditorTools.DrawSpriteField("Foreground", "Foreground image (optional)", NGUISettings.atlas, sprite2, OnSprite2);
            GUI.enabled = true;
            
            if (ShouldCreate(go, !string.IsNullOrEmpty(sprite1))) {
                int depth = NGUITools.CalculateNextDepth(go);
                var root = NGUITools.AddChild(go);
                root.name = "FilledBar";
                var barScript = root.AddComponent<UIFillProgressBar>();
                
                if (withBackground && !string.IsNullOrEmpty(sprite0)) {
                    var bg = NGUITools.AddWidget<UISprite>(root);
                    bg.name = "background";
                    bg.depth = depth;
                    bg.type = UISprite.Type.Simple;
                    bg.atlas = NGUISettings.atlas;
                    bg.spriteName = sprite0;
                    bg.pivot = UIWidget.Pivot.TopLeft;
                    bg.transform.localPosition = Vector3.zero;
                    bg.MakePixelPerfect();
                    
                }
                
                var bar = NGUITools.AddWidget<UISprite>(root);
                bar.name = "bar";
                bar.depth = depth + 10;
                bar.atlas = NGUISettings.atlas;
                bar.spriteName = sprite1;
                bar.pivot = UIWidget.Pivot.TopLeft;
                bar.type = UISprite.Type.Filled;
                bar.fillDirection = UISprite.FillDirection.Horizontal;
                
                bar.transform.localPosition = Vector3.zero;
                bar.MakePixelPerfect();
                
                barScript.bar = bar;
                
                if (withForeground && !string.IsNullOrEmpty(sprite2)) {
                    var fg = NGUITools.AddWidget<UISprite>(root);
                    fg.name = "foreground";
                    fg.depth = depth + 20;
                    fg.type = UISprite.Type.Simple;
                    fg.atlas = NGUISettings.atlas;
                    fg.spriteName = sprite2;
                    fg.pivot = UIWidget.Pivot.TopLeft;
                    fg.transform.localPosition = Vector3.zero;
                    fg.MakePixelPerfect();
                    
                }
            }
        }
    }
    
    private void CreateRepeated(GameObject go) {
        if (NGUISettings.atlas != null) {
            NGUIEditorTools.DrawSpriteField("Icon", "Icon sprite to repeat", NGUISettings.atlas, sprite0, OnSprite0);

            bool withSlots = BoolField("With Slots", "", bool0, OnBool0);
            
            GUI.enabled = withSlots;
            NGUIEditorTools.DrawSpriteField("Slot", "Slot for icons", NGUISettings.atlas, sprite1, OnSprite1);
            GUI.enabled = true;
            
            GUILayout.BeginHorizontal();
            int repeat = EditorGUILayout.IntField("Repeat Count", int0, GUILayout.Width(200f));
            GUILayout.Space(20f);
            GUILayout.Label("How many times icon should repeated");
            GUILayout.EndHorizontal();
            
            if (repeat != int0) {
                int0 = repeat;
                Save();
                Repaint();
            }
        
            if (ShouldCreate(go, true)) {
                Vector3 delta = new Vector3(64, 0, 0);
            
                int depth = NGUITools.CalculateNextDepth(go);
                go = NGUITools.AddChild(go);
                go.name = "RepeatBar";
                
                var bar = go.AddComponent<UIRepeatProgressBar>();
                bar.icons = new UIRepeatProgressBar.Icon[repeat];
                
                for (int i = 0; i < repeat; ++i) {
                    var ic = new UIRepeatProgressBar.Icon();
                    bar.icons[i] = ic;
                    
                    var iconAnchor = NGUITools.AddChild(go);
                    iconAnchor.name = "iconAnchor" + (i + 1);
                    ic.anchor = iconAnchor.transform;
                    
                    ic.anchor.localPosition = delta * i;
                
                    if (withSlots && sprite1 != null) {
                        UISprite slot = NGUITools.AddWidget<UISprite>(iconAnchor);
                        slot.type = UISprite.Type.Simple;
                        slot.name = "slot";
                        slot.depth = depth + i * 2;
                        slot.atlas = NGUISettings.atlas;
                        slot.spriteName = sprite1;
                        slot.MakePixelPerfect();
                        
                        ic.slot = slot.transform;
                        ic.slotScaleOrig = slot.transform.localScale;
                    }
                    
                    UISprite icon = NGUITools.AddWidget<UISprite>(iconAnchor);
                    icon.type = UISprite.Type.Filled;
                    icon.fillDirection = UISprite.FillDirection.Horizontal;
                    icon.name = "icon";
                    icon.depth = depth + i * 2 + 1;
                    icon.atlas = NGUISettings.atlas;
                    icon.spriteName = sprite0;
                    icon.MakePixelPerfect();
                    
                    ic.icon = icon.transform;
                    ic.iconScaleOrig = icon.transform.localScale;
                }
                
                
             }
         }
    }
    
    private void CreateSequence(GameObject go) {
        if (NGUISettings.atlas != null) {
            if (ShouldCreate(go, true)) {
                int depth = NGUITools.CalculateNextDepth(go);
                go = NGUITools.AddChild(go);
                go.name = "SequenceBar";
                
                var bar = go.AddComponent<UISequenceProgressBar>();
                var sprite = NGUITools.AddWidget<UISprite>(go);
                sprite.type = UISprite.Type.Simple;
                sprite.name = "sprite";
                sprite.depth = depth;
                sprite.atlas = NGUISettings.atlas;
                
                bar.sprite = sprite;
             }
         }
    }
    
    /// <summary>
    /// Convenience function -- creates the "Add To" button and the parent object field to the right of it.
    /// </summary>
    
    bool ShouldCreate (GameObject go, bool isValid) {
        GUI.color = isValid ? Color.green : Color.grey;
    
        GUILayout.BeginHorizontal();
        bool retVal = GUILayout.Button("Add To", GUILayout.Width(76f));
        GUI.color = Color.white;
        GameObject sel = EditorGUILayout.ObjectField(go, typeof(GameObject), true, GUILayout.Width(140f)) as GameObject;
        GUILayout.Label("Select the parent in the Hierarchy View", GUILayout.MinWidth(10000f));
        GUILayout.EndHorizontal();
    
        if (sel != go) Selection.activeGameObject = sel;
    
        if (retVal && isValid)
        {
            NGUIEditorTools.RegisterUndo("Add a Widget");
            return true;
        }
        return false;
     }

    // ===========================================================
    // Static Methods
    // ===========================================================
    
    static void Save() {
        SaveString("EBT Sprite 0", sprite0);
        SaveString("EBT Sprite 1", sprite1);
        SaveString("EBT Sprite 2", sprite2);
        SaveInt("EBT Int 0", int0);
        SaveBool("EBT Bool 0", bool0);
        SaveBool("EBT Bool 1", bool1);
    }
    
    static void Load() {
        sprite0 = LoadString("EBT Sprite 0");
        sprite1 = LoadString("EBT Sprite 1");
        sprite2 = LoadString("EBT Sprite 2");
        int0 = LoadInt("EBT Int 0");
        bool0 = LoadBool("EBT Bool 0");
        bool1 = LoadBool("EBT Bool 1");
    }
    
    static void SaveString(string field, string val) {
        if (string.IsNullOrEmpty(val)) {
            EditorPrefs.DeleteKey(field);
        } else {
            EditorPrefs.SetString(field, val);
        }
    }
    
    static string LoadString(string field) {
        string s = EditorPrefs.GetString(field);
        return (string.IsNullOrEmpty(s)) ? "" : s;
    }
    
    static void SaveInt(string field, int val) {
        EditorPrefs.SetInt(field, val);
    }
    
    static int LoadInt(string field) {
        return EditorPrefs.GetInt(field, 0);
    }
    
    static void SaveBool(string field, bool val) {
        EditorPrefs.SetBool(field, val);
    }
    
    static bool LoadBool(string field) {
        return EditorPrefs.GetBool(field, false);
    }
    
    delegate void OnBool(bool val);
    
    bool BoolField(string label, string description, bool val, OnBool onBool) {
        GUILayout.BeginHorizontal();
        bool nval = EditorGUILayout.Toggle(label, val, GUILayout.Width(200f));
        GUILayout.Space(20f);
        GUILayout.Label(description);
        GUILayout.EndHorizontal();
        
        if (nval != val) {
            onBool(nval);
        }
        
        return nval;
    }

    // ===========================================================
    // Inner and Anonymous Classes
    // ===========================================================
    
    public enum BarType {
        Filled,
        Repeated,
        Sequence,
    }

}