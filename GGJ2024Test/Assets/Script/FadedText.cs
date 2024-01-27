using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadedText : MonoBehaviour
{
    [SerializeField] public float TextAppearSpeed = 1.0f;

    private TextMeshProUGUI m_TextMeshPro;
    private void Awake()
    {
        m_TextMeshPro = this.gameObject.GetComponent<TextMeshProUGUI>();
    }
    public void GenerateText(string src)
    {
        StartCoroutine(generateText(src, m_TextMeshPro));
    }
    private IEnumerator generateText(string src, TextMeshProUGUI m_TextMeshPro)
    {
        string text = "";
        for (int i = 0; i < src.Length; i++)
        {
            text += src[i];
            m_TextMeshPro.SetText(text);
            yield return new WaitForSeconds(TextAppearSpeed);
        }
    }
}
