using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class HeartUI : MonoBehaviour
{
    private HeartState _state;
    private HeartType _type;

    RawImage _image;

    private void Awake() {
        _image = GetComponent<RawImage>();
    }

    public async Task SetTexture(HeartState newState, HeartType type = HeartType.Red)
    {
        _state = newState;
        _type = type;
        
        Texture2D texture = await HeartData.GetHeartTexture(_state, _type);

        if (texture != null)        
        {
            _image.texture = texture;
        }
        else
        {
            Debug.LogError($"HealthHeart: Texture not found for state {_state} and type {_type}");
        }
    }
}
