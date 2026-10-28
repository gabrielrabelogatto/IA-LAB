using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Obrigatório para usar os eventos de UI

public class ColocarItens_Tela : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image imagemAlvo;
    private Color corBaseAlvo;
    private GameObject prefab;

    [Header("Configurações")]
    public float corPassarMouse_Transparencia = 217f;
    public GameObject objetoSpawnar;
    public Transform destino;
    public Vector3 tamanhoObjetoSpawnar;

    void Start()
    {
        imagemAlvo = GetComponent<Image>();

        if (imagemAlvo != null)
        {
            corBaseAlvo = imagemAlvo.color;
        }
    }

    private void Update()
    {
        Grid grid = FindFirstObjectByType<Grid>();

        if(grid != null && prefab != null)
        {
            Vector3 posicaoAlvo = grid.ObterPosicaoGridAtual();

            prefab.transform.position = grid.ObterPosicaoGridAtual();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Grid grid = FindFirstObjectByType<Grid>();

            if(grid != null)
            {
                grid.CriarGrid();

                criarObjetoColocar();
            }
        }
    }

    //Outras funções

    private void criarObjetoColocar()
    {
        if(objetoSpawnar != null)
        {
            prefab = Instantiate(objetoSpawnar, Vector3.zero, Quaternion.identity, destino);

            if(prefab != null)
            {
                prefab.transform.localScale = tamanhoObjetoSpawnar;
            }
        }
    }
}