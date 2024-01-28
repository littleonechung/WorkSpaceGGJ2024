using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Image imageToChange; // 用於引用要改變顏色的Image元件
    public float colorChangeSpeed = 1.0f; // 顏色變化速度

    public float initialHue; // 初始色相
    private float currentHue;

    private float hue;

    void Awake()
    {
        imageToChange = GetComponent<Image>();
    }

    private void Start()
    {

        initialHue = Random.Range(0f, 1f);

        Debug.Log("Initiel Hue=" + initialHue);
        currentHue = initialHue;
    }

    void Update()
    {
        if (imageToChange != null)
        {
            // 隨時間改變色相值
            currentHue += Time.deltaTime * colorChangeSpeed;
            if (currentHue > 1) currentHue -= 1;

            // 設置Image的顏色
            imageToChange.color = Color.HSVToRGB(currentHue, 1f, 1f);
        }
    }
}
