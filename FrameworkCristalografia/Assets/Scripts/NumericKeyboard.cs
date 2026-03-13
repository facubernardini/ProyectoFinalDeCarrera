using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class NumericKeyboard : MonoBehaviour
{
    private TMP_InputField _currentInput;
    private string _originalValue;
    
    [Header("UI References")]
    public TextMeshProUGUI previewText; // Arrastra aquí el texto de preview

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetTargetInput(TMP_InputField input)
    {
        _currentInput = input;
        
        // Vaciamos el texto del input original
        if (_currentInput != null)
        {
            _originalValue = _currentInput.text;
            _currentInput.text = "0"; 
            // Opcional: _currentInput.text = "0"; si prefieres que empiece en cero
        }

        gameObject.SetActive(true);
        UpdatePreview(); // Esto pondrá la preview también en blanco
    }

    public void TypeKey(string character)
    {
        if (_currentInput == null) return;

        int cursorPosition = _currentInput.caretPosition;
        if (cursorPosition == 0)
        {
            _currentInput.text = "";
        }
        _currentInput.text = _currentInput.text.Insert(cursorPosition, character);
        _currentInput.caretPosition = cursorPosition + 1;
        
        UpdatePreview(); // Actualizar preview tras escribir
    }

    public void Backspace()
    {
        // 1. Verificación básica: que haya un input y que no esté vacío
        if (_currentInput == null || _currentInput.text.Length == 0) return;

        // 2. Obtenemos la posición actual del cursor (caret)
        int cursorPosition = _currentInput.caretPosition;

        // 3. Solo borramos si el cursor NO está al inicio del texto
        if (cursorPosition > 0)
        {
            // Remove(índice de inicio, cantidad de caracteres a borrar)
            // Borramos 1 solo carácter en la posición anterior al cursor
            _currentInput.text = _currentInput.text.Remove(cursorPosition - 1, 1);

            // 4. Reposicionamos el cursor una posición atrás
            _currentInput.caretPosition = cursorPosition - 1;
        }

        // 5. Sincronizamos la preview
        UpdatePreview();
    }

    // Función clave para la Preview
    private void UpdatePreview()
    {
        if (_currentInput != null && previewText != null)
        {
            previewText.text = _currentInput.text;
        }
    }

    public void SubmitAndClose()
    {
        if (_currentInput != null)
        {
            _currentInput.onEndEdit.Invoke(_currentInput.text);
            EventSystem.current.SetSelectedGameObject(null);
            _currentInput = null;
        }
        
        if(previewText != null) previewText.text = "";
        gameObject.SetActive(false);
    }

    public void Cancel()
    {
        if (_currentInput != null)
        {
            // 1. Restauramos el valor original que guardamos al abrir
            _currentInput.text = _originalValue;
            
            // 2. Forzamos la actualización de cualquier script externo que lea el input
            _currentInput.onEndEdit.Invoke(_currentInput.text);

            // 3. Quitamos el foco
            EventSystem.current.SetSelectedGameObject(null);
            _currentInput = null;
        }

        // 4. Cerramos el teclado
        gameObject.SetActive(false);
    }
}