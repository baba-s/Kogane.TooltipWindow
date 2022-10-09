using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    internal sealed class TooltipWindowContent : PopupWindowContent
    {
        private static readonly Vector2 WINDOW_SIZE_OFFSET = new( 6, 4 );

        private readonly string               m_label;
        private readonly GUIStyle             m_labelStyle;
        private readonly Vector2              m_windowSize;
        private readonly GUILayoutOption      m_labelHeight;
        private readonly EditorWaitForSeconds m_editorWaitForSeconds;

        private EditorCoroutine m_editorCoroutine;

        public TooltipWindowContent
        (
            string text,
            float  time,
            int    fontSize
        )
        {
            m_labelStyle = new( EditorStyles.label )
            {
                alignment = TextAnchor.UpperLeft,
                fontSize  = fontSize,
            };

            var guiContent = new GUIContent( text );
            var labelSize  = m_labelStyle.CalcSize( guiContent );

            m_label                = text;
            m_windowSize           = labelSize + WINDOW_SIZE_OFFSET;
            m_labelHeight          = GUILayout.Height( labelSize.y );
            m_editorWaitForSeconds = new EditorWaitForSeconds( time );
        }

        public override void OnOpen()
        {
            m_editorCoroutine = EditorCoroutineUtility.StartCoroutine
            (
                routine: OnUpdate(),
                owner: editorWindow
            );
        }

        public override void OnClose()
        {
            if ( m_editorCoroutine == null ) return;
            EditorCoroutineUtility.StopCoroutine( m_editorCoroutine );
            m_editorCoroutine = null;
        }

        private IEnumerator OnUpdate()
        {
            yield return m_editorWaitForSeconds;
            m_editorCoroutine = null;
            if ( editorWindow == null ) yield break;
            editorWindow.Close();
        }

        public override Vector2 GetWindowSize()
        {
            return m_windowSize;
        }

        public override void OnGUI( Rect rect )
        {
            EditorGUILayout.LabelField( m_label, m_labelStyle, m_labelHeight );
        }
    }
}