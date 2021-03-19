using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatReference))]
public class FloatReferenceDrawer : PropertyDrawer
{
    /// <summary>
    /// Options to display in the popup to select constant or variable.
    /// </summary>
    private readonly string[] popupOptions = { "Use Constant", "Use Variable" };

    /// <summary> 
    /// Style to use to draw the popup button. 
    /// </summary>
    private GUIStyle popupStyle;

    /// <summary>
    /// Used to draw the Property Drawer
    /// </summary>
    /// <param name="position">Position of the proerty</param>
    /// <param name="property">The Property to make the custom GUI for.</param>
    /// <param name="label">The label of this property</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Style to use when drawing the popup
        if (popupStyle == null)
        {
            popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
            popupStyle.imagePosition = ImagePosition.ImageOnly;
        }

        // Initialize Label
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        // Begin checking if there is a change in the GUI State 
        EditorGUI.BeginChangeCheck();

        // Get properties
        SerializedProperty useConstant = property.FindPropertyRelative("_useConstant");
        SerializedProperty constantValue = property.FindPropertyRelative("_constantValue");
        SerializedProperty variable = property.FindPropertyRelative("_variable");

        // Calculate rect for configuration button
        Rect buttonRect = new Rect(position);
        buttonRect.yMin += popupStyle.margin.top;
        buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
        position.xMin = buttonRect.xMax;

        // Store old indent level and set it to 0, the PrefixLabel takes care of it
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Make the pop up using the popup options provided
        int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);

        // Get the result of what ever the user has chosen on the pop up options
        useConstant.boolValue = result == 0;

        // Draw the property field depending on what the user chose on the pop up
        EditorGUI.PropertyField(position,
            useConstant.boolValue ? constantValue : variable,
            GUIContent.none);

        // End checking by Applying the newly made property
        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}
