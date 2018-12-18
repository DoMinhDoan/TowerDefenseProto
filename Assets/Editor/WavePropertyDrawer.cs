using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof (Wave))]
public class WavePropertyDrawer : PropertyDrawer
{
    const int spriteHeight = 50;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);
        if(property.isExpanded)
        {
            SerializedProperty enemyPrefabProperty = property.FindPropertyRelative("enemyPrefab");
            GameObject enermyPrefab = (GameObject)enemyPrefabProperty.objectReferenceValue;
            if(enermyPrefab != null)
            {
                SpriteRenderer enemySprite = enermyPrefab.GetComponentInChildren<SpriteRenderer>();
                
                Rect indentedRect = EditorGUI.IndentedRect(position);
                float fieldHeight = base.GetPropertyHeight(property, label) + 2;
                Vector3 enemySize = enemySprite.bounds.size;
                Rect texturePosition = new Rect(indentedRect.x + indentedRect.width / 4, indentedRect.y + fieldHeight * 4, enemySize.x / enemySize.y * spriteHeight, spriteHeight);

                EditorGUI.DropShadowLabel(texturePosition, new GUIContent(enemySprite.sprite.texture));
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty enemyPrefabProperty = property.FindPropertyRelative("enemyPrefab");
        GameObject enermyPrefab = (GameObject)enemyPrefabProperty.objectReferenceValue;
        if (property.isExpanded && enermyPrefab != null)
        {
            return EditorGUI.GetPropertyHeight(property) + spriteHeight;
        }
        return EditorGUI.GetPropertyHeight(property);
    }
}
