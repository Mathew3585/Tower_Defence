using UnityEngine;
using TMPro;

namespace CurvedUIUtility
{
    public partial class CurvedTextMeshPro : TextMeshProUGUI, ICurveable
    {
        /*
         * To reduce code clutter in the main class, I have moved the modified underline code to its own file.
         */
        protected override void DrawUnderlineMesh(Vector3 start, Vector3 end, ref int index, float startScale, float endScale, float maxScale, float sdfScale, Color32 underlineColor)
        {
#if TMP_MANUALLY_GET_UNDERLINE
            GetUnderlineSpecialCharacter(m_fontAsset);

            var m_cached_Underline_Character = m_Underline.character;
#endif

            if (m_cached_Underline_Character == null)
            {
                if (!TMP_Settings.warningsDisabled)
                    Debug.LogWarning("Unable to add underline since the Font Asset doesn't contain the underline character.", this);

                return;
            }

            int horizontalElements = CurvedUIHelper.GetNumberOfElementsForWidth(Mathf.Abs(start.x - end.x));

            int verticesCount = index + (horizontalElements * 4);
            // Check to make sure our current mesh buffer allocations can hold these new Quads.
            if (verticesCount > m_textInfo.meshInfo[0].vertices.Length)
            {
                // Resize Mesh Buffers
                m_textInfo.meshInfo[0].ResizeMeshInfo(verticesCount / 4);
            }

            // Adjust the position of the underline based on the lowest character. This matters for subscript character.
            start.y = Mathf.Min(start.y, end.y);
            end.y = Mathf.Min(start.y, end.y);

            float startPadding = m_padding * startScale / maxScale;
            float endPadding = m_padding * endScale / maxScale;

            float underlineThickness = m_fontAsset.faceInfo.underlineThickness;

            // Alpha is the lower of the vertex color or tag color alpha used.
            underlineColor.a = m_fontColor32.a < underlineColor.a ? (byte)(m_fontColor32.a) : (byte)(underlineColor.a);

            Vector3[] vertices = m_textInfo.meshInfo[0].vertices;
            Vector2[] uvs0 = m_textInfo.meshInfo[0].uvs0;
            Vector2[] uvs2 = m_textInfo.meshInfo[0].uvs2;
            Color32[] colors32 = m_textInfo.meshInfo[0].colors32;

            Vector2 uvBL = new Vector2((m_cached_Underline_Character.glyph.glyphRect.x - startPadding) / m_fontAsset.atlasWidth, (m_cached_Underline_Character.glyph.glyphRect.y - m_padding) / m_fontAsset.atlasHeight);  // bottom left
            Vector2 uvTL = new Vector2(uvBL.x, (m_cached_Underline_Character.glyph.glyphRect.y + m_cached_Underline_Character.glyph.glyphRect.height + m_padding) / m_fontAsset.atlasHeight);  // top left
            Vector2 uvBR = new Vector2((m_cached_Underline_Character.glyph.glyphRect.x + endPadding + m_cached_Underline_Character.glyph.glyphRect.width) / m_fontAsset.atlasWidth, uvTL.y); // End Part - Bottom Right
            Vector2 uvTR = new Vector2(uvBR.x, uvBL.y); // End Part - Top Right

            var xScale = Mathf.Abs(sdfScale);
            var width = end.x - start.x;

            for (int i = 0; i < horizontalElements; i++)
            {
                var face = i * 4;

                var faceWidth = width / horizontalElements * i;
                var nextFaceWidth = width / horizontalElements * (i + 1);

                var bl = index + face + 0;
                var tl = index + face + 1;
                var tr = index + face + 2;
                var br = index + face + 3;

                if (i == 0)
                {
                    vertices[bl] = start + new Vector3(0, 0 - (underlineThickness + m_padding) * maxScale, 0);
                    vertices[tl] = start + new Vector3(0, m_padding * maxScale, 0);
                    vertices[tr] = vertices[tl] + new Vector3(nextFaceWidth, 0, 0);
                    vertices[br] = vertices[bl] + new Vector3(nextFaceWidth, 0, 0);

                    uvs0[bl] = uvBL;
                    uvs0[tl] = uvTL;
                    uvs0[tr] = Vector2.Lerp(uvTL, uvTR, nextFaceWidth / width);
                    uvs0[br] = Vector2.Lerp(uvBL, uvBR, nextFaceWidth / width);
                }
                else if (i == horizontalElements - 1)
                {
                    vertices[bl] = start + new Vector3(faceWidth, -(underlineThickness + m_padding) * maxScale, 0);
                    vertices[tl] = start + new Vector3(faceWidth, m_padding * maxScale, 0);
                    vertices[tr] = end + new Vector3(0, m_padding * maxScale, 0);
                    vertices[br] = end + new Vector3(0, -(underlineThickness + m_padding) * maxScale, 0);

                    uvs0[bl] = Vector2.Lerp(uvBL, uvBR, faceWidth / width);
                    uvs0[tl] = Vector2.Lerp(uvTL, uvTR, faceWidth / width);
                    uvs0[tr] = uvTR;
                    uvs0[br] = uvBR;
                }
                else
                {
                    vertices[bl] = start + new Vector3(faceWidth, 0 - (underlineThickness + m_padding) * maxScale, 0);
                    vertices[tl] = start + new Vector3(faceWidth, m_padding * maxScale, 0);
                    vertices[tr] = vertices[tl] + new Vector3(nextFaceWidth - faceWidth, 0, 0);
                    vertices[br] = vertices[bl] + new Vector3(nextFaceWidth - faceWidth, 0, 0);

                    uvs0[bl] = Vector2.Lerp(uvBL, uvBR, faceWidth / width);
                    uvs0[tl] = Vector2.Lerp(uvTL, uvTR, faceWidth / width);
                    uvs0[tr] = Vector2.Lerp(uvTL, uvTR, nextFaceWidth / width);
                    uvs0[br] = Vector2.Lerp(uvBL, uvBR, nextFaceWidth / width);
                }

                uvs2[bl] = PackUV(faceWidth / width, 0, xScale);
                uvs2[tl] = PackUV(faceWidth / width, 1, xScale);
                uvs2[tr] = PackUV(nextFaceWidth / width, 1, xScale);
                uvs2[br] = PackUV(nextFaceWidth / width, 0, xScale);

                colors32[bl] = underlineColor;
                colors32[tl] = underlineColor;
                colors32[tr] = underlineColor;
                colors32[br] = underlineColor;
            }

            index = verticesCount;
        }
    }
}
