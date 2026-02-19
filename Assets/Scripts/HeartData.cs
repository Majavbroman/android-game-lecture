using System.Collections.Generic;
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
    Green
}

public static class HeartData
{
    public static Dictionary<HeartState, Dictionary<HeartType, Texture2D>> HeartTextures;

    public static void LoadHearts()
    {
        HeartTextures = new Dictionary<HeartState, Dictionary<HeartType, Texture2D>>();
        Debug.Log("HeartData: Loading heart textures...");

        foreach (HeartState state in System.Enum.GetValues(typeof(HeartState)))
        {
            if (state == HeartState.Empty)
            {
                HeartTextures[state] = new Dictionary<HeartType, Texture2D>
                {
                    { HeartType.Red, Resources.Load<Texture2D>("Hearts/Empty") }
                };

                continue;
            }

            HeartTextures[state] = new Dictionary<HeartType, Texture2D>();

            foreach (HeartType type in System.Enum.GetValues(typeof(HeartType)))
            {
                string textureName = $"{type}_{state}";
                Texture2D texture = Resources.Load<Texture2D>($"Hearts/{textureName}");
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
    }

    public static Texture2D GetHeartTexture(HeartState state, HeartType type = HeartType.Red)
    {
        if (HeartTextures == null)
        {
            LoadHearts();
        }

        if (state == HeartState.Empty)
        {
            return HeartTextures[HeartState.Empty][HeartType.Red];
        }

        return HeartTextures.ContainsKey(state) && HeartTextures[state].ContainsKey(type) ? HeartTextures[state][type] : null;
    }
}
