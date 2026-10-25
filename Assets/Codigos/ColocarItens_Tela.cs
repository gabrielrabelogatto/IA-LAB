using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Obrigatório para usar os eventos de UI

public class ColocarItens_Tela : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image imagemAlvo;
    private Color corBaseAlvo;

    [Header("Configurações")]
    public float corPassarMouse_Transparencia = 217f;

    void Start()
    {
        imagemAlvo = GetComponent<Image>();

        if (imagemAlvo != null)
        {
            corBaseAlvo = imagemAlvo.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (imagemAlvo != null)
        {
            Color cor = imagemAlvo.color;
            cor.a = corPassarMouse_Transparencia / 255f;
            imagemAlvo.color = cor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (imagemAlvo != null)
        {
            Color cor = imagemAlvo.color;
            cor.a = corBaseAlvo.a;
            imagemAlvo.color = cor;
        }
    }
}