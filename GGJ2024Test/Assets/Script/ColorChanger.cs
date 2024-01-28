using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Image imageToChange; // �Ω�ޥέn�����C�⪺Image����
    public float colorChangeSpeed = 1.0f; // �C���ܤƳt��

    public float initialHue; // ��l���
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
            // �H�ɶ����ܦ�ۭ�
            currentHue += Time.deltaTime * colorChangeSpeed;
            if (currentHue > 1) currentHue -= 1;

            // �]�mImage���C��
            imageToChange.color = Color.HSVToRGB(currentHue, 1f, 1f);
        }
    }
}
