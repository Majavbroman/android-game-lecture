using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum HeartState
{
    Full,
    Half,
    Empty
}

public enum HeartType
{
    Red,
    Blue,
    Green,
    Orange
}

public static class HeartData
{
    public static Dictionary<HeartState, Dictionary<HeartType, Texture2D>> HeartTextures;

    public static readonly HeartType[] HEART_ORDER = { HeartType.Red, HeartType.Green, HeartType.Orange, HeartType.Blue };

    public static Task LoadHearts()
    {
        HeartTextures = new Dictionary<HeartState, Dictionary<HeartType, Texture2D>>();
        Debug.Log("HeartData: Loading heart textures...");

        foreach (HeartState state in System.Enum.GetValues(typeof(HeartState)))
        {
            if (state == HeartState.Empty)
            {
                HeartTextures[state] = new Dictionary<HeartType, Texture2D>
                {
                    { HeartType.Red, Resources.Load<Texture2D>("UI/Hearts/Empty") }
                };

                continue;
            }

            HeartTextures[state] = new Dictionary<HeartType, Texture2D>();

            foreach (HeartType type in System.Enum.GetValues(typeof(HeartType)))
            {
                string textureName = $"{type}_{state}";
                Texture2D texture = Resources.Load<Texture2D>($"UI/Hearts/{textureName}");
                if (texture != null)
                {
                    HeartTextures[state][type] = texture;
                }
                else
                {
                    Debug.LogWarning($"HeartData: Texture not found for {textureName}");
                }
            }
        }

        Debug.Log("HeartData: Heart textures loaded.");
        return Task.CompletedTask;
    }

    public async static Task<Texture2D> GetHeartTexture(HeartState state, HeartType type = HeartType.Red)
    {
        if (HeartTextures == null)
        {
            await LoadHearts();
        }

        if (state == HeartState.Empty)
        {
            return HeartTextures[HeartState.Empty][HeartType.Red];
        }

        return HeartTextures.ContainsKey(state) && HeartTextures[state].ContainsKey(type) ? HeartTextures[state][type] : null;
    }
}
