using UnityEngine;

public class Grid : MonoBehaviour
{
    [Header("Configurações do GRID")]
    public int colunas = 30;
    public int linhas = 16;
    public float tamanhoGrid = 1.0f;
    public Vector2 posicaoInicial_Grid = new Vector2(0.0f, 0.0f);

    [Header("Aparencia")]
    public Color corGrid = new Color(1f, 1f, 1f, 0.2f); // Branco transparente
    public Color corQuadrado_aoEncostar = new Color(1f, 0.9f, 0.2f, 0.85f); // Amarelo
    public float espessuraLinha = 0.03f;

    private GameObject pastaLinhas;
    private LineRenderer caixaCursor; //Esta caixa que vamos criar vai seguir o cursor
    private Material materialLinhas;
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        materialLinhas = new Material(Shader.Find("Sprites/Default"));
        //Criar_CaixaCursorMouse();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Outras funções

    public void CriarGrid()
    {
        pastaLinhas = new GameObject("LinhasGrid");
        pastaLinhas.transform.SetParent(transform);

        float larguraTotal = colunas * tamanhoGrid;
        float alturaTotal = linhas * tamanhoGrid;

        for(int x = 0; x <= colunas; x++)
        {
            float posX = posicaoInicial_Grid.x + (x * tamanhoGrid);
            DesenharLinha(new Vector3(posX, posicaoInicial_Grid.y, 0), new Vector3(posX, posicaoInicial_Grid.y + alturaTotal, 0));
        }

        for(int y = 0; y <= linhas; y++)
        {
            float posY = posicaoInicial_Grid.y + (y * tamanhoGrid);
            DesenharLinha(new Vector3(posicaoInicial_Grid.x, posY, 0), new Vector3(posicaoInicial_Grid.x + larguraTotal, posY, 0));
        }
    }

    private void DesenharLinha(Vector3 inicio, Vector3 fim)
    {
        GameObject obj = new GameObject("Linha");
        obj.transform.SetParent(pastaLinhas.transform);
        LineRenderer lineRenderer = obj.AddComponent<LineRenderer>();
        lineRenderer.material = materialLinhas;
        lineRenderer.startColor = lineRenderer.endColor = corGrid;
        lineRenderer.startWidth = lineRenderer.endWidth = espessuraLinha;
        lineRenderer.positionCount = 2;
        lineRenderer.sortingOrder = 5;
        lineRenderer.SetPosition(0, inicio);
        lineRenderer.SetPosition(1, fim);
    }
}
