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
    }

    // Update is called once per frame
    void Update()
    {
        AtualizarPosicaoCursor();
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

        Criar_CaixaCursorMouse();
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

    private void AtualizarPosicaoCursor()
    {
        if (caixaCursor == null || cam == null) return;

        // 1. Pega a posição do mouse no mundo 2D
        Vector3 mouseMundo = cam.ScreenToWorldPoint(Input.mousePosition);

        // 2. Calcula qual célula o mouse está (descontando o offset inicial e o transform do objeto)
        Vector2 origemReal = (Vector2)transform.position + posicaoInicial_Grid;

        int celulaX = Mathf.FloorToInt((mouseMundo.x - origemReal.x) / tamanhoGrid);
        int celulaY = Mathf.FloorToInt((mouseMundo.y - origemReal.y) / tamanhoGrid);

        // 3. Verifica se o mouse está dentro dos limites da grade
        if (celulaX >= 0 && celulaX < colunas && celulaY >= 0 && celulaY < linhas)
        {
            caixaCursor.enabled = true;

            // 4. Calcula a posição do canto do bloco atual ("snap")
            float cantoX = origemReal.x + (celulaX * tamanhoGrid);
            float cantoY = origemReal.y + (celulaY * tamanhoGrid);

            // 5. Posiciona os 4 cantos da caixa amarela presos na célula
            Vector3[] pontosCaixa = new Vector3[5];
            pontosCaixa[0] = new Vector3(cantoX, cantoY, 0);
            pontosCaixa[1] = new Vector3(cantoX + tamanhoGrid, cantoY, 0);
            pontosCaixa[2] = new Vector3(cantoX + tamanhoGrid, cantoY + tamanhoGrid, 0);
            pontosCaixa[3] = new Vector3(cantoX, cantoY + tamanhoGrid, 0);
            pontosCaixa[4] = pontosCaixa[0]; // Fecha o quadrado

            caixaCursor.SetPositions(pontosCaixa);
        }
        else
        {
            // Se o mouse estiver fora do grid, esconde o cursor
            caixaCursor.enabled = false;
        }
    }

    private void Criar_CaixaCursorMouse()
    {
        GameObject objCursor = new GameObject("CaixaCursor");
        objCursor.transform.SetParent(transform);

        caixaCursor = objCursor.AddComponent<LineRenderer>();
        caixaCursor.material = materialLinhas;
        caixaCursor.startColor = caixaCursor.endColor = corQuadrado_aoEncostar;
        caixaCursor.startWidth = caixaCursor.endWidth = espessuraLinha * 1.5f;
        caixaCursor.positionCount = 5;
        caixaCursor.loop = true;
        caixaCursor.sortingOrder = 10; // Fica acima das linhas da grade
        caixaCursor.enabled = false;
    }

    public Vector3 ObterPosicaoGridAtual()
    {
        if (cam == null) return Vector3.zero;

        Vector3 mouseMundo = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 origemReal = (Vector2)transform.position + posicaoInicial_Grid;

        int celulaX = Mathf.FloorToInt((mouseMundo.x - origemReal.x) / tamanhoGrid);
        int celulaY = Mathf.FloorToInt((mouseMundo.y - origemReal.y) / tamanhoGrid);

        // Verifica se o mouse está dentro dos limites da grade
        if (celulaX >= 0 && celulaX < colunas && celulaY >= 0 && celulaY < linhas)
        {
            float cantoX = origemReal.x + (celulaX * tamanhoGrid);
            float cantoY = origemReal.y + (celulaY * tamanhoGrid);

            // Devolve o centro exato do quadrado (se o teu prefab tiver o pivot no centro)
            // Se o pivot do prefab for no canto inferior esquerdo, retira a parte do "+ (tamanhoGrid / 2f)"
            return new Vector3(cantoX + (tamanhoGrid / 2f), cantoY + (tamanhoGrid / 2f), 0);
        }

        return Vector3.zero; // Retorna 0,0,0 caso clique fora do grid
    }
}
