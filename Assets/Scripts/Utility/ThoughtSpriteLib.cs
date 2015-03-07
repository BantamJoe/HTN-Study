using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ThoughtSpriteLib
{
    private Dictionary<Thought, Sprite> spriteMap;
    private string[] textureLoc = { "social_icon", "explore_icon", "aggro_icon"};
    private List<Texture2D> textures;
    
    public ThoughtSpriteLib()
    {
        spriteMap = new Dictionary<Thought, Sprite>();
        loadTextures();
        initializeThoughtSpriteMap();
    }

    public Sprite GetThoughtSprite(Thought thought)
    {
        Sprite thoughtSprite;
        spriteMap.TryGetValue(thought, out thoughtSprite);
        return thoughtSprite;
    }

    private void loadTextures()
    {
        textures = new List<Texture2D>();
        foreach (string loc in textureLoc)
        {
            /**Texture location = enumeration **/
            Texture2D texture = (Texture2D)Resources.Load(loc);
            textures.Add(texture);
        }
    }

    private void initializeThoughtSpriteMap()
    {
        //add more behaviors here
        spriteMap.Add(Thought.Social, Sprite.Create(textures[0], new Rect(0, 0, 0, 0), Vector2.zero));
        spriteMap.Add(Thought.Explore, Sprite.Create(textures[1], new Rect(0, 0, 0, 0), Vector2.zero));
        spriteMap.Add(Thought.Aggro, Sprite.Create(textures[2], new Rect(0, 0, 0, 0), Vector2.zero));

    }
}