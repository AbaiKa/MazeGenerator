using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelStart : MonoBehaviour
{
    [SerializeField] private Button _startGameBtn;

    [SerializeField] private Slider _mazeWidthSlider;
    [SerializeField] private Slider _mazeHeightSlider;

    private TextMeshProUGUI _widthText;
    private TextMeshProUGUI _heightText;
    private void Start()
    {
        _widthText = _mazeWidthSlider.GetComponentInChildren<TextMeshProUGUI>();
        _heightText = _mazeHeightSlider.GetComponentInChildren<TextMeshProUGUI>();

        // defoult values
        _mazeWidthSlider.value = 0.15f;
        _mazeHeightSlider.value = 0.15f;

        OnWidthSliderValueChanged();
        OnHeightSliderValueChanged();

        _startGameBtn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        int width = (int)getMazeConfig(_mazeWidthSlider);
        int height = (int)getMazeConfig(_mazeHeightSlider);

        if (width >= 3 && height >= 3)
        {
            MazeAssistant.main.CreateMaze(width, height);
        }
        else
        {
            if (width < 3) _widthText.text = "Minimum 3";
            if (height < 3) _heightText.text = "Minimum 3";
        }
    }
    public void OnWidthSliderValueChanged() => _widthText.text = $"Width: {(int)getMazeConfig(_mazeWidthSlider)}";
    public void OnHeightSliderValueChanged() => _heightText.text = $"Height: {(int)getMazeConfig(_mazeHeightSlider)}";
    private float getMazeConfig(Slider slider) => slider.value * 100;
}
